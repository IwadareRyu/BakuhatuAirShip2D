using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItazuraBullet : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Rigidbody2D rb;
    Mimikku _flip;
    [SerializeField] GameObject _hit;
    // Start is called before the first frame update
    void Start()
    {
        //“G‚ÌŒü‚«‚ğQÆ‚µ‚ÄA‚»‚ÌŒü‚«‚É—Í‚ğ‰Á‚¦‚éB
        rb = GetComponent<Rigidbody2D>();
        _flip = GameObject.FindWithTag("HomingEnemy").GetComponent<Mimikku>();
        rb.velocity = Vector2.left * _speed * _flip._minasmimikku;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RouinGameManager GM = GameObject.FindGameObjectWithTag("GM").GetComponent<RouinGameManager>();

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Player" && !GM.star)
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            FindObjectOfType<RouinGameManager>().AddLife(-5f);
            GM.StartCoroutine("StarTime");
        }

    }
}
