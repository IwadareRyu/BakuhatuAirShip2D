using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossBullet : MonoBehaviour
{
    [SerializeField] float _angle;
    [SerializeField] float _speed = 3f;
    [SerializeField] bool _stop;
    Vector3 _velocity;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);

        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);

        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        _rb.velocity = _velocity;
        if(_stop)
        {
            StartCoroutine(StopBullet());
        }
    }

    IEnumerator StopBullet()
    {
        _rb.simulated = false;
        yield return new WaitForSeconds(3f);
        _rb.simulated = true;
    }


}
