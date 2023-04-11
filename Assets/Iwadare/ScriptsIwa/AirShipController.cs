using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipController : MonoBehaviour
{
    Rigidbody2D _rb;
    [Tooltip("攻撃のクールタイム")]
    bool _cooltime;
    [Tooltip("オールアタックポイント")]
    [SerializeField] GameObject[] _allAttackPoints;
    [Tooltip("全てを消す爆発,オールアタック")]
    [SerializeField] GameObject _allAttack;
    [Tooltip("爆風")]
    [SerializeField] GameObject _bakuhatu;
    [Tooltip("飛ばす飛行機の初期位置")]
    [SerializeField] GameObject _airShipMazzle;
    [Tooltip("プレイヤーに付いてくる飛行機のオンオフ")]
    [SerializeField] GameObject _airShipOnOff;
    [Tooltip("右を向いていたら+1、左を向いていたら-1。")]
    float minas = 1;
    float h;
    float v;
    [Tooltip("プレイヤーのスピード")]
    [SerializeField] float _speed = 3f;
    [Tooltip("プレイヤーの低速スピード")]
    [SerializeField] float _lowSpeed = 1.5f;
    [Tooltip("プレイヤーの現在のスピード")]
    float _nowSpeed;
    public float _minas => minas;
    [Tooltip("飛行機が再生成される時間")]
    [SerializeField] float _time = 3f;
    GameManager _gm;
    [Tooltip("リトライするための変数。")]
    SceneLoader _activeLoad;
    [Tooltip("PowerUpのスクリプト")]
    private PowerUp _power;
    [Tooltip("プール")]
    [SerializeField] BulletPoolActive _pool;
    [Header("trueにすれば、当たり判定なくなるから実質無敵。")]
    [Tooltip("無敵時間")]
    [SerializeField]bool _starTime;
    [Header("通常時の色")]
    [Tooltip("通常時の色")]
    [SerializeField] Color _normalColor;
    [Header("無敵時の色")]
    [Tooltip("無敵時の色")]
    [SerializeField] Color _starColor;

    // Start is called before the first frame update
    void Start()
    {
        //それぞれの要素をGetConponentする。
        _rb = GetComponent<Rigidbody2D>();
        //AttackAni =GetComponent<Animator>();
        _gm = GameManager.Instance;
        //_activeLoad = GameObject.FindGameObjectWithTag("GM").GetComponent<SceneLoader>();
        _power = PowerUp.Instance;
        _nowSpeed = _speed;
    }


    void Update()
    {
        //if (!_gm._gameover)
        //{
        //プレイヤーの左右の動き。
        h = Input.GetAxisRaw("Horizontal");
        //プレイヤーの上下の動き。
        v = Input.GetAxisRaw("Vertical");

        //飛行機を飛ばす。
        if (Input.GetButton("Fire1"))
        {
            if (!_cooltime)
            {
                _gm.AddScore(-100);
                StartCoroutine(BulletCoolTime());
            }
        }

        if(Input.GetButtonDown("Fire3"))
        {
            _nowSpeed = _lowSpeed;
        }

        if(Input.GetButtonUp("Fire3"))
        {
            _nowSpeed = _speed;
        }
    }
    private void FixedUpdate()
    {
        //上下左右に入力されたときの動きの計算。
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * _nowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_starTime && collision.gameObject.tag == "EnemyBullet" || !_starTime && collision.gameObject.tag == "Enemy" ||!_starTime && collision.gameObject.tag == "BossFire")
        {
            _gm.AddScore(-500);
            _starTime = true;
            StartCoroutine(AllAtackTime());
        }

        //上のフロアに入ったらお金が自機に吸い寄せられる仕組み
        if(collision.gameObject.tag == "GetMoneyFloor")
        {
            var moneys = GameObject.FindGameObjectsWithTag("Money");

            if(moneys.Length != 0)
            {
                foreach(var i in moneys)
                {
                    var moneyscript = i.GetComponent<PointMoneyScripts>();
                    moneyscript.MoneyGet(gameObject);
                }
            }
        }
    }

    IEnumerator AllAtackTime()
    {
        _cooltime = true;
        var col = GetComponent<SpriteRenderer>();
        col.color = _starColor;
        _airShipOnOff.SetActive(false);
        Instantiate(_bakuhatu, transform.position, Quaternion.identity);

        foreach (var i in _allAttackPoints)
        {
            yield return new WaitForSeconds(0.3f);
            Instantiate(_allAttack, i.transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(4f);
        _starTime = false;
        col.color = _normalColor;
        _airShipOnOff.SetActive(true);
        _cooltime = false;
    }

    IEnumerator BulletCoolTime()
    {
        _cooltime = true;

        for (var i = 0; i < _power._airnum; i++)
        {
            yield return new WaitForSeconds(0.1f);
            var bullet = _pool.GetBullet();
            bullet.transform.position = _airShipMazzle.transform.position;
        }

        _airShipOnOff.SetActive(false);
        yield return new WaitForSeconds(_time);

        if (!_starTime)
        {
            _airShipOnOff.SetActive(true);
            _cooltime = false;
        }
    }
}
