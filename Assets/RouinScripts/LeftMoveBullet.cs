using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveBullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Rigidbody2D rb;
    [SerializeField] GameObject _hit;
    [Tooltip("左(+1)か右(-1)に球を飛ばす")]
    [SerializeField] float _minas = 1f;
    [Tooltip("プレイヤーに自身が当たるか当たらないか。")]
    [SerializeField] bool _isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //球をvelocityで飛ばす。
        rb.velocity = Vector2.left * _speed * _minas;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RouinGameManager GM = GameObject.FindGameObjectWithTag("GM").GetComponent<RouinGameManager>();
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player" && !GM.star && !_isPlayer)
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            FindObjectOfType<RouinGameManager>().AddLife(-5f);
            GM.StartCoroutine("StarTime");
        }
    }
}
