using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMoneyScripts : MonoBehaviour
{
    GameManager _gm;
    Rigidbody2D _rb;
    [SerializeField]int _point = 100;
    [SerializeField] float _speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.down * _speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            _gm.AddScore(_point);
            Destroy(gameObject);
        }
    }
}
