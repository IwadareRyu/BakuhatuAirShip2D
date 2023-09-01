using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimikkuParents : CopyBase
{
    private GameObject Mazzle;
    private Transform _atari;
    [SerializeField] GameObject _copyTumeBullet;
    Rigidbody2D _rb;
    float _speed = 3f;
    [Tooltip("プレイヤーのposition")]
    private GameObject _playerpos;
    [Tooltip("カーブする場所を決める。")]
    [SerializeField] float _playerOffest = 2f;
    [Tooltip("カーブの速さ。")]
    [SerializeField] float _cavePower = 2f;
    float _thisX;
    bool rotation0;
    /// <summary>自分のコピーを生成する関数</summary>
    public override void CopyTech()
    {
        Mazzle = GameObject.FindGameObjectWithTag("Mazzle");
        _atari = Mazzle.GetComponent<Transform>();
        Instantiate(_copyTumeBullet, _atari.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerpos = GameObject.FindGameObjectWithTag("Player");
        //下に動かす。
        _rb.velocity = Vector2.down * _speed;
        //向きを変える。
        transform.Rotate(new Vector3(0, 0, -90f));
    }
    private void Update()
    {

        //null判定
        if(_playerpos)
        {

            //自身とプレイヤーのyの距離がOffestの値より小さかったら
            if(transform.position.y - _playerpos.transform.position.y < _playerOffest)
            {

                if(_thisX == 0)
                {
                    //自身とプレイヤーのxの位置がプラス(プレイヤーが左にいる)なら1、マイナス(プレイヤーが右にいる)なら-1を代入する。
                    _thisX = ( _playerpos.transform.position.x > transform.position.x) ? 1 : -1;
                }

                if (!rotation0)
                {
                    //自身のrotationを元に戻し、また回転しないようbool型をtrueにする(しないと大回転する)。
                    transform.Rotate(new Vector3(0, 0, 90f));
                    rotation0 = true;
                }
                //向き判定。
                FlipX(_thisX);
                //左か右に力を加える。
                _rb.AddForce(_thisX * Vector2.right * _cavePower);

            }

        }

    }

    void FlipX(float horizontal)
    {
        //向き
        if(horizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(horizontal< 0)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
