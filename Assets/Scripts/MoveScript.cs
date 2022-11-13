using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [Tooltip("左右の移動")]
    [SerializeField][Range(1.0f,-1.0f)] float _rLMove = 1.0f;
    [Tooltip("上下の移動")]
    [SerializeField][Range(1.0f, -1.0f)] float _uDMove = -1.0f;
    [Tooltip("スピード")]
    [SerializeField] float _speed = 3f;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動方向
        Vector2 dir = new Vector2(_rLMove, _uDMove).normalized;
        //移動
        _rb.velocity = dir * _speed;
        if(transform.position.x > 15 || transform.position.x < -15
            || transform.position.y > 15 || transform.position.y < -15)
        {
            Destroy(this.gameObject);
        }
    }
}
