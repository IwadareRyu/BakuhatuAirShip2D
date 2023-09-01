using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mimikku : MonoBehaviour
{
    [Tooltip("プレイヤー")]
    [SerializeField] GameObject _player;
    [Tooltip("プレイヤーの位置")]
    [SerializeField] Vector2 _playerTrams;
    [Tooltip("球を飛ばす初期位置")]
    [SerializeField] GameObject _muzzleE;
    [Tooltip("！(危険信号)を出す位置")]
    [SerializeField] GameObject _muzzle2;
    [Tooltip("!(危険信号)")]
    [SerializeField] GameObject _bikkuri;
    [Tooltip("まっすぐ飛ぶ球")]
    [SerializeField] GameObject _bullet;
    [Tooltip("プレイヤーに向かって飛ぶ球")]
    [SerializeField] GameObject _playerBullet;
    [Tooltip("自身の向き")]
    float minas;
    public float _minasmimikku => minas;
    [Tooltip("ワープする際に出るエフェクト")]
    [SerializeField] GameObject _warp;
    [Tooltip("子分を生成する位置(上)")]
    [SerializeField] GameObject[] _muzzleU;
    [Tooltip("子分を生成する位置(左)")]
    [SerializeField] GameObject[] _muzzleL;
    [Tooltip("子分を生成する位置(右)")]
    [SerializeField] GameObject[] _muzzleR;
    [Tooltip("ワープする位置")]
    [SerializeField] Transform[] _warpMuzzle;
    [Tooltip("上から降ってくるミミック子分")]
    [SerializeField] GameObject _parentsmimikku;
    [Tooltip("左右から降ってくるミミック子分")]
    [SerializeField] GameObject _parentsmimikkuLR;
    [Tooltip("自身(this.gameobjectだと箱の方が操作できない。)")]
    [SerializeField] GameObject _myMimikku;
    [Tooltip("自身(箱のほう)")]
    [SerializeField] GameObject _hakomimikku;
    [Tooltip("ランダム変数")]
    int _ram2;
    [Tooltip("攻撃している時間のbool型")]
    public bool _attackTime;
    [Tooltip("自身のアニメーション")]
    Animator _anim;
    [Tooltip("dotweenで自身が移動する時間")]
    float _time = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackTime)
        {
            //falseにしてコルーチンが被らないようにする
            _attackTime = false;
            Instantiate(_bikkuri, _muzzle2.transform.position, Quaternion.identity);
            _anim.Play("mumukku2");
            //コルーチンスタート
            StartCoroutine(EnemyTime());
        }
    }
    private void FixedUpdate()
    {
        //自身の向きをプレイヤーの位置に合わせて変更する。
        _playerTrams = (_player.transform.position - this.transform.position).normalized;
        FlipX(_playerTrams);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        StartCoroutine("Dammage");
    //    }

    //}
    void FlipX(Vector2 horizontal)
    {
        //向き
        if (horizontal.x < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            minas = 1;
        }
        else if (horizontal.x > 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            minas = -1;
        }

    }
    IEnumerator EnemyTime()
    {
        //球生成*3
        yield return new WaitForSeconds(0.2f);
        for (var i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(_bullet, _muzzleE.transform.position, Quaternion.identity);
        }
        //一定の位置へ移動
        yield return new WaitForSeconds(3f);
        _myMimikku.transform.DOMove(_warpMuzzle[0].position, _time).SetEase(Ease.Linear);
        _anim.Play("Syokanmae");
        
        //10回ミミック子分(下向き)の召喚
        for(var i = 0;i<10;i++)
        {
            yield return new WaitForSeconds(2f);
            int ram1 = Random.Range(0, 3);
            _ram2 = ram1;
            Instantiate(_parentsmimikku, _muzzleU[_ram2].transform.position,Quaternion.identity);
        }

        //左に行くか右に行くかをランダム変数でランダムにする。
        yield return new WaitForSeconds(5f);
        int ram = Random.Range(1, 3);
        Debug.Log(ram);
        _anim.Play("BeforeAttack");

        if(ram == 1)
        {
            /*左のパターン*/
            //自身を移動させる。
            _myMimikku.transform.DOMove(_warpMuzzle[ram].position, _time).SetEase(Ease.Linear);

            for (var i = 0;i < 10;i++)
            {
                yield return new WaitForSeconds(2f);
                SyokanTime(_muzzleL);
            }

        }
        else
        {
            /*右のパターン*/
            //自身を移動させる。
            _myMimikku.transform.DOMove(_warpMuzzle[ram].position, _time).SetEase(Ease.Linear);

            for (var i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(2f);
                SyokanTime(_muzzleR);
            }

        }
        //左か右の待機場所にワープさせて箱になる。
        yield return new WaitForSeconds(5f);
        ram = Random.Range(3, 5);
        Instantiate(_warp, transform.position, Quaternion.identity);
        _myMimikku.transform.position = _warpMuzzle[ram].position;
        Instantiate(_warp, transform.position, Quaternion.identity);
        _hakomimikku.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void SyokanTime(GameObject[] _muzzleLR)
    {
        //球とミミック(左右)を召喚させる。
        Instantiate(_bikkuri, _muzzle2.transform.position, Quaternion.identity);
        int ram1 = Random.Range(0, 3);
        Instantiate(_parentsmimikkuLR, _muzzleLR[ram1].transform.position, Quaternion.identity);
        Instantiate(_playerBullet, _muzzleE.transform.position, Quaternion.identity);
    }

}
