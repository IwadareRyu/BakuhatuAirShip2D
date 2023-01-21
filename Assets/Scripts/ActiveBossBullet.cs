using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossBullet : MonoBehaviour
{
    [Tooltip("跳ぶ向き")]
    [SerializeField] float _angle;
    [Tooltip("速さ")]
    [SerializeField] float _speed = 3f;
    float dist = 0;
    Vector3 _velocity;
    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    Vector3 _transA;
    [Header("生成時一度止まる球")]
    [SerializeField] bool _stop;
    [Tooltip("0:red直線、1:右回り、2:左回り,3:blue直線")]
    int _type = 0;
    [Header("炎の色をアタッチ")]
    [SerializeField] Sprite _redFire;
    [SerializeField] Sprite _blueFire;
    [Header("スフィアをアタッチ")]
    [SerializeField] Sprite _bluesphere;
    [SerializeField] Sprite _redsphere;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        //Move();
    }

    private void Update()
    {
        if (transform.position.x > 5 || transform.position.x < -5
            || transform.position.y > 7 || transform.position.y < -7)
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        if (_type == 1 || _type == 2)
        {
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad) * dist;
            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad) * dist;
            Vector3 pos = transform.position;
            _rb.velocity = _velocity;

            if(_type == 1)
            {
                _angle += 0.3f;
            }
            else if(_type == 2)
            {
                _angle -= 0.3f;
            }

            dist += 0.1f;
        }
        
    }


    private void Type0Move()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        _rb.velocity = _velocity;
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }
    }


    private void Type3Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            Vector2 v = player.transform.position - transform.position;
            v = v.normalized * _speed;
            float zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
            _rb.velocity = v;
        }
        //if (_stop)
        //{
        //    StartCoroutine(StopBullet());
        //}
    }

    IEnumerator StopBullet()
    {
        yield return new WaitForSeconds(0.5f);
        _rb.simulated = false;
        yield return new WaitForSeconds(3f);
        _rb.simulated = true;
    }

    public void Reset()
    {
        _transA = transform.position;
        _transA.y = 10f;
        transform.position = _transA;
        gameObject.SetActive(false);
    }

    public void BulletAdd(float angle, float speed, int type = 0)
    {
        _angle = angle;
        _speed = speed;
        _type = type;
        dist = 0;
        if(_type == 2)
        {
            _sprite.sprite = _bluesphere;
        }
        else if(_type == 1)
        {
            _sprite.sprite = _redsphere;
        }
        else if(_type == 0)
        {
            _sprite.sprite = _redFire;
            Type0Move();
        }
        else
        {
            _sprite.sprite = _blueFire;
            Type3Move();
        }
    }
}
