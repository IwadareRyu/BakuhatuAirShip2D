using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Rigidbody2D rb;
    [SerializeField] GameObject _hit;
    [SerializeField,Range(-1,1)] float _minas = 1f;
    [SerializeField] bool _isplayerBullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //下に球を出す。
        rb.velocity = Vector2.down * _speed * _minas;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        //壁か地面に当たったら破壊。
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        ////プレイヤーへのダメージ(!GM.starは無敵時間じゃないとき)
        //if (collision.gameObject.tag == "Player" && !GM.star && !_isPlayer)
        //{
        //    Instantiate(_hit, collision.transform.position, Quaternion.identity);
        //    GM.StartCoroutine("StarTime");
        //}
    }
}
