using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipController : MonoBehaviour
{
    Rigidbody2D _rb;
    [Tooltip("攻撃のクールタイム")]
    bool _cooltime;
    [Tooltip("飛ばす飛行機")]
    [SerializeField] GameObject _airShip;
    [Tooltip("飛ばす飛行機の初期位置")]
    [SerializeField] GameObject _airShipMazzle;
    [Tooltip("プレイヤーに付いてくる飛行機のオンオフ")]
    [SerializeField] GameObject _airShipOnOff;
    [Tooltip("右を向いていたら+1、左を向いていたら-1。")]
    float minas = 1;
    float h;
    float v;
    [Tooltip("プレイヤーのスピード")]
    [SerializeField] float Speed = 3f;
    public float _minas => minas;
    [Tooltip("飛行機が再生成される時間")]
    [SerializeField] float _time = 3f;
    GameManager _gm;
    [Tooltip("リトライするための変数。")]
    SceneLoader _activeLoad;

    // Start is called before the first frame update
    void Start()
    {
        //それぞれの要素をGetConponentする。
        _rb = GetComponent<Rigidbody2D>();
        //AttackAni =GetComponent<Animator>();
        _gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        //_activeLoad = GameObject.FindGameObjectWithTag("GM").GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_gm._gameover)
        //{
        //プレイヤーの左右の動き。
        h = Input.GetAxisRaw("Horizontal");
        //プレイヤーの上下の動き。
        v = Input.GetAxisRaw("Vertical");

        //飛行機を飛ばす。
        if (Input.GetButton("Fire1") && !_cooltime)
        {
            Instantiate(_airShip, _airShipMazzle.transform.position, Quaternion.identity);
            StartCoroutine(CoolTime());
        }

        ////リトライ
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    _activeLoad.ActiveSceneLoad();
        //}
        //else
        //{
        //    h = 0;
        //    v = 0;
        //}
    }
    private void FixedUpdate()
    {
        //上下左右に入力されたときの動きの計算。
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * Speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            if (!_cooltime)
            {
                StartCoroutine(CoolTime());
            }
        }

    }
    IEnumerator CoolTime()
    {
        _gm.AddScore(-100);
        _airShipOnOff.SetActive(false);
        _cooltime = true;
        yield return new WaitForSeconds(_time);
        _airShipOnOff.SetActive(true);
        _cooltime = false;
    }
}
