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
    bool _stop;
    [Header("炎の色をアタッチ")]
    [SerializeField] Sprite _redFire;
    [SerializeField] Sprite _blueFire;
    [Header("スフィアをアタッチ")]
    [SerializeField] Sprite _bluesphere;
    [SerializeField] Sprite _redsphere;
    BulletTypeClass.BulletSpriteState _state;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {

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
        if (_state == BulletTypeClass.BulletSpriteState.LeftSphere || _state == BulletTypeClass.BulletSpriteState.RightSphere)
        {
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad) * dist;
            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad) * dist;
            Vector3 pos = transform.position;
            _rb.velocity = _velocity;

            if(_state == BulletTypeClass.BulletSpriteState.LeftSphere)
            {
                _angle += 0.3f;
            }
            else if(_state == BulletTypeClass.BulletSpriteState.RightSphere)
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
        //if (_stop)
        //{
        //    StartCoroutine(StopBullet());
        //}
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
        yield return new WaitForSeconds(0.2f);
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

    public void BulletAdd(float angle, float speed, BulletTypeClass.BulletSpriteState type = BulletTypeClass.BulletSpriteState.RedFire ,bool stop = false)
    {
        _angle = angle;
        _speed = speed;
        _state = type;
        _stop = stop;
        dist = 0;
        switch(_state)
        {
            case BulletTypeClass.BulletSpriteState.LeftSphere:
                _sprite.sprite = _bluesphere;
                break;
            case BulletTypeClass.BulletSpriteState.RightSphere:
                _sprite.sprite = _redsphere;
                break;
            case BulletTypeClass.BulletSpriteState.RedFire:
                _sprite.sprite = _redFire;
                Type0Move();
                break;
            case BulletTypeClass.BulletSpriteState.BlueFire:
                _sprite.sprite = _blueFire;
                Type3Move();
                break;
        }
    }
}
