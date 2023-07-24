using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Tooltip("攻撃フラグ")]
    bool _attackBool;
    [Tooltip("一度だけの処理を行うフラグ")]
    bool _oneShot;
    [SerializeField,Tooltip("オーバーモード攻撃のオブジェクト")]
    GameObject _overAttack; 
    [SerializeField,Tooltip("プレイヤーに向かって発射するオブジェクト")] 
    GameObject _ownFire;
    [SerializeField,Tooltip("弾のオブジェクト一覧の配列")] 
    GameObject[] _bulletSpawns;
    [SerializeField,Tooltip("移動ポイントのTransform一覧の配列")] 
    Transform[] _movePoint;
    [SerializeField,Tooltip("移動停止距離")] 
    float _stopDistance = 0.5f;
    [SerializeField,Tooltip("移動速度")] 
    float _moveSpeed = 3f;
    [SerializeField,Tooltip("攻撃時の移動速度")]
    float _attackSpeed = 5f;
    [SerializeField,Tooltip("攻撃中の時間")]
    float _attackTime = 2f;
    [SerializeField,Tooltip("ボスの状態の列挙型")] 
    BossState _bossState;
    [Tooltip("移動を停止するフラグ")]
    bool _stopBool;
    [Tooltip("モグラの攻撃フラグ")]
    bool _moleAttackBool;
    [Tooltip("ランダムな変数")]
    int _random;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        _ownFire.SetActive(false);
        _overAttack.SetActive(false);
        if (_bulletSpawns.Length != 0)
        {
            foreach (var i in _bulletSpawns)
            {
                i.SetActive(false);
            }
        }
    }

    void Update()
    {
        //攻撃するとき
        if (_attackBool)
        {
            if (!_oneShot)
            {
                _random = (int)Random.Range(0, 2.9f);
                _stopBool = false;
                StartCoroutine(AttackTime()); 
                _oneShot = true;
            }   //移動させて、攻撃処理のコルーチンを呼び出し、Updateで回らないようフラグをtrueにする。
            float distance = Vector2.Distance(transform.position, _movePoint[_random].position);
            if (distance > _stopDistance && !_stopBool)
            {
                Vector3 dir = (_movePoint[_random].position - transform.position).normalized * _moveSpeed;
                transform.Translate(dir * Time.deltaTime);
            }   //特定の地点にたどり着くまでその地点に向かって移動
            else
            {
                _stopBool = true;
            }    //特定の地点にたどり着いたら、移動を停止
            if (_moleAttackBool)
            {
                Vector3 dir = Vector3.down.normalized * _attackSpeed;
                transform.Translate(dir * Time.deltaTime);
            }   //もぐらの攻撃時に下方向をさせる。
        }
    }

    /// <summary>攻撃のコルーチン</summary>
    IEnumerator AttackTime()
    {
        //移動が停止するまで待機
        yield return new WaitWhile(() => _stopBool == false);
        yield return new WaitForSeconds(2f);
        if (_attackBool)
        {
            //ボスがドラゴンの時の処理
            if (_bossState == BossState.Drgon)
            {
                //オーバー攻撃をアクティブにする。
                _overAttack.SetActive(true);
                BGMManager.Instance.SEPlay(BGMManager.SE.FireBreath);
                yield return new WaitForSeconds(0.2f);
                if (_bulletSpawns.Length > 2)
                {
                    var ram = Random.Range(0, 2);
                    for (var i = ram; i < _bulletSpawns.Length; i += 2)
                    {
                        _bulletSpawns[i].SetActive(true);
                    }
                }   //2パターンの攻撃からランダムに弾のスポーンポイントを出す。
                yield return new WaitForSeconds(3.5f);
                if (_bulletSpawns.Length != 0)
                {
                    foreach (var i in _bulletSpawns)
                    {
                        i.SetActive(false);
                    }
                }   //全てのスポーンポイントを非アクティブにする
                _overAttack.SetActive(false);
            }   // オーバー攻撃を非アクティブにする

            //モグラのボスの時の処理
            else if (_bossState == BossState.MoguraBoss)
            {
                _overAttack.SetActive(true);
                //モグラのの攻撃フラグをtrueにする
                _moleAttackBool = true;
                //もぐらの攻撃を停止する時間だけ待機
                yield return new WaitForSeconds(_attackTime);
                //もぐらの攻撃フラグをfalseにする
                _moleAttackBool = false;
                yield return new WaitForSeconds(0.1f);
                //オーバー攻撃を非アクティブにする
                _overAttack.SetActive(false);
                yield return new WaitForSeconds(2f);
            }
        }

        _oneShot = false;
    }

    public void OverMode()
    {
        if (_movePoint.Length > 0)
        {
            transform.position = _movePoint[0].position;
        }   //最初のポイントに位置を設定
        //攻撃フラグを切り替え
        _attackBool = !_attackBool;
        //プレイヤーに向けた攻撃を切り替え
        _ownFire.SetActive(_attackBool);
        //オーバー攻撃を非アクティブにする
        _overAttack.SetActive(false); 
    }

    enum BossState
    {
        /// <summary>ドラゴン</summary>
        Drgon,
        /// <summary>モグラ</summary>
        MoguraBoss,
    }
}
