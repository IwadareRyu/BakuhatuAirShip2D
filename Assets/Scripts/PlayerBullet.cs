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
    [SerializeField] GameObject _bakuhatu;
    private PowerUp _power;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
        //球を出す。
        rb.velocity = Vector2.down * (_speed +_power._speedUp * 0.1f) * _minas;
        StartCoroutine(BakuhatuTime());
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
            Bakuhatu();
        }
        ////プレイヤーへのダメージ(!GM.starは無敵時間じゃないとき)
        //if (collision.gameObject.tag == "Player" && !GM.star && !_isPlayer)
        //{
        //    Instantiate(_hit, collision.transform.position, Quaternion.identity);
        //    GM.StartCoroutine("StarTime");
        //}
    }
    IEnumerator BakuhatuTime()
    {
        yield return new WaitForSeconds(2f);
        Bakuhatu();
    }
    void Bakuhatu()
    {
        if (_isplayerBullet && _bakuhatu)
        {
            Instantiate(_bakuhatu, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
