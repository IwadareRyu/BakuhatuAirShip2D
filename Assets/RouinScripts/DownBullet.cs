using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Rigidbody2D rb;
    [SerializeField] GameObject _hit;
    [SerializeField] float _minas = 1f;
    [Tooltip("プレイヤーが自身に当たるか当たらないか。")]
    [SerializeField] bool _isPlayer;
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
        RouinGameManager GM = GameObject.FindGameObjectWithTag("GM").GetComponent<RouinGameManager>();
        //壁か地面に当たったら破壊。
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        //プレイヤーへのダメージ(!GM.starは無敵時間じゃないとき)
        if (collision.gameObject.tag == "Player" && !GM.star && !_isPlayer)
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            FindObjectOfType<RouinGameManager>().AddLife(-5f);
            GM.StartCoroutine("StarTime");
        }
    }
}
