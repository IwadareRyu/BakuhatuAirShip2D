using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimikkuLR : CopyBase
{
    float _minas;
    Rigidbody2D _rb;
    private GameObject _playerpos;
    float _speed = 5f;
    Vector2 _playerTrans;
    [SerializeField] GameObject _copyBullet;
    private GameObject Mazzle;
    private Transform _atari;

    // Start is called before the first frame update
    void Start()
    {
        _playerpos = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        //プレイヤーの位置と自分の位置を計算して、向きを変える。
        _playerTrans = (_playerpos.transform.position - transform.position).normalized;
        FlipXBullet(_playerTrans);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FlipXBullet(Vector2 horizontal)
    {
        //プレイヤーが自分の左にいたら左に、右にいたら右にvelocityで動かす。
        if (horizontal.x > 0f)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            _rb.velocity = Vector2.right * _speed * 1;
        }
        else if (horizontal.x < 0f)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            _rb.velocity =  Vector2.right * _speed * -1;
        }
    }
    //当たり判定に当たったら、自分のコピーを生成する。
    public override void CopyTech()
    {
        Mazzle = GameObject.FindGameObjectWithTag("Mazzle");
        _atari = Mazzle.GetComponent<Transform>();
        Instantiate(_copyBullet, _atari.position, Quaternion.identity);
    }
}
