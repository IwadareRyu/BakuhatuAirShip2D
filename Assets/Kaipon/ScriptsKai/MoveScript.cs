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
    [SerializeField] bool _reset;
    [SerializeField] bool _pattern;
    bool _lock;
    Transform _playerpos;
    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        
        _rb = this?.GetComponent<Rigidbody2D>();
        //移動方向
        Vector2 dir = new Vector2(_rLMove, _uDMove).normalized;
        //移動
        _rb.velocity = dir * _speed;
        if(_pattern)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb.simulated == false)
        {
            return;
        }

        if (transform.position.x > 13 || transform.position.x < -10
            || transform.position.y > 13 || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        if(_pattern && !_lock)
        {
            _playerpos = _player.GetComponent<Transform>();
            if(_rLMove == 0 && _playerpos.position.y > transform.position.y )
            {
                _rLMove = _playerpos.position.x > transform.position.x ? 1 : -1;
                _uDMove = 0;
                Vector2 dir = new Vector2(_rLMove,_uDMove ).normalized;
                //移動
                _rb.velocity = dir * _speed;
                _lock = true;
            }
        }
    }
}
