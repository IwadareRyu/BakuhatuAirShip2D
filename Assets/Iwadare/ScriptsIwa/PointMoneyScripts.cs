using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMoneyScripts : MonoBehaviour
{
    GameManager _gm;
    Rigidbody2D _rb;
    GameObject _player;
    [SerializeField]int _point = 100;
    [SerializeField] float _speed = 2f;
    [SerializeField] float _getSpeed = 5f;
    bool _moneyget;
    [SerializeField] float _y = -5.3f;
    AudioSource _se;
    [SerializeField] AudioClip _coin;
    // Start is called before the first frame update


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.down * _speed;
        _se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_moneyget)
        {
            if(_player)
            {
                Vector3 dir = (_player.transform.position - transform.position) * _getSpeed;
                transform.Translate(dir * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if(transform.position.y < _y)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            _gm.AddScore(_point);
            SEManager.Instance?.SEPlay(SEManager.SE.Coin);
            Destroy(gameObject);
        }
    }

    public void MoneyGet(GameObject player)
    {
        _player = player;
        _moneyget = true;
    }
}
