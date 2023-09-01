using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItazuraBulletPlayer : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] GameObject _hit;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            //プレイヤーと球の位置を計算して、プレイヤーの方向へ力を加える。
            Vector2 v = player.transform.position - transform.position;
            v = v.normalized * _speed;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = v;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RouinGameManager GM = GameObject.FindGameObjectWithTag("GM").GetComponent<RouinGameManager>();
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player" && !GM.star)
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            FindObjectOfType<RouinGameManager>().AddLife(-5);
            GM.StartCoroutine("StarTime");
        }
    }
}
