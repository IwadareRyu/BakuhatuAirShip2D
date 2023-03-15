using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMove : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _stopsec = 10f;
    [SerializeField] GameObject _bulletpoint;
    Rigidbody2D _rb;
    float _y;
    [Tooltip("’âŽ~”»’è")]
    bool _stopBool;
    [SerializeField] bool _takatori;

    private void OnEnable()
    {
        _y = Random.Range(0f, 4f);
        _stopBool = false;
    }

    // Start is called before the first frame updat
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.down * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= _y && _stopBool == false)
        {
            _stopBool = true;
            StartCoroutine(StopTime());
        }
        if (this.transform.position.y <= -7f)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator StopTime()
    {
        _rb.velocity *= 0;
        yield return new WaitForSeconds(_stopsec);
        _rb.velocity = Vector2.down * _speed;
        if(_takatori)
        {
            _bulletpoint.SetActive(false);
        }
    }

}
