using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossBullet : MonoBehaviour
{
    [Tooltip("’µ‚ÔŒü‚«")]
    [SerializeField] float _angle;
    [Tooltip("‘¬‚³")]
    [SerializeField] float _speed = 3f;
    Vector3 _velocity;
    Rigidbody2D _rb;
    Vector3 _transA;
    [Header("¶¬ˆê“x~‚Ü‚é‹…")]
    [SerializeField] bool _stop;
    [Header("‚Ü‚Á‚·‚®”ò‚Ô‹…")]
    [SerializeField] bool _typeA;
    [Header("‰E‰ñ‚è‚·‚é‹…")]
    [SerializeField] bool _typeB;
    [Header("¶‰ñ‚è‚·‚é‹…")]
    [SerializeField] bool _typeC;

    void OnEnable()
    {
        //Move();
    }

    private void Move()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);

        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);

        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
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
        if (_typeA)
        {
            Move();
        }
    }


}
