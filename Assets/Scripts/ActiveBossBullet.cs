using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossBullet : MonoBehaviour
{
    [Tooltip("跳ぶ向き")]
    [SerializeField] float _angle;
    [Tooltip("速さ")]
    [SerializeField] float _speed = 3f;
    Vector3 _velocity;
    Rigidbody2D _rb;
    Vector3 _transA;
    [Header("生成時一度止まる球")]
    [SerializeField] bool _stop;
    [Header("まっすぐ飛ぶ球")]
    [SerializeField] bool _typeA;
    [Header("右回りする球")]
    [SerializeField] bool _typeB;
    [Header("左回りする球")]
    [SerializeField] bool _typeC;

    void OnEnable()
    {
        //Move();
    }

    private void Move()
    {
        if (_typeA)
        {
            _rb = GetComponent<Rigidbody2D>();
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);

            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
            float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
            _rb.velocity = _velocity;
        }
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }
    }

    private void Update()
    {
        if (transform.position.x > 5 || transform.position.x < -5
            || transform.position.y > 7 || transform.position.y < -7)
        {
            Reset();
        }
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

    public void BulletAdd(float angle,float speed)
    {
        _angle = angle;
        _speed = speed;
        Move();
    }
}
