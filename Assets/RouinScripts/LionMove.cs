using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionMove : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _power;
    [SerializeField] float _movepower;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //左にAddForceでライオンを吹っ飛ばす。
        _rb.AddForce(Vector2.left * _power, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //左にライオンを動かす。
        _rb.velocity = new Vector2(-1 * _movepower, _rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
