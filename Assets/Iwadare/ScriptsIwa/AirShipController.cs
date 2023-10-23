using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [Tooltip("右を向いていたら+1、左を向いていたら-1")]
    float minas = 1;
    float h;
    float v;
    [Tooltip("移動の向き")]
    Vector2 _move;
    [Tooltip("プレイヤーのスピード")]
    [SerializeField] float _speed = 3f;
    [Tooltip("プレイヤーの低速スピード")]
    [SerializeField] float _lowSpeed = 1.5f;
    [Tooltip("プレイヤーの現在のスピード")]
    float _nowSpeed;
    public float _minas => minas;
    [Tooltip("飛行機が再生成される時間")]
    [SerializeField] float _time = 3f;
    [Tooltip("GameManagerのインスタンス")]
    GameManager _gm;
    [Tooltip("リトライするための変数。")]
    SceneLoader _activeLoad;
    [Tooltip("PowerUpのインスタンス")]
    private PowerUp _power;
    [Tooltip("プール")]
    [SerializeField] BulletPoolActive _pool;
    [Header("trueにすれば、当たり判定なくなるから実質無敵")]
    [Tooltip("無敵時間")]
    [SerializeField] bool _starTime;
    [Header("通常時の色")]
    [Tooltip("通常時の色")]
    [SerializeField] Color _normalColor;
    [Header("無敵時の色")]
    [Tooltip("無敵時の色")]
    [SerializeField] Color _starColor;
    [Tooltip("弾を出すフラグ")]
    private bool _fire;
    [Tooltip("移動を遅くするフラグ")]
    private bool _slowMove;
    [Tooltip("ポーズフラグ")]
    private bool _pause;

    /// InputSystem処理  
    /// <summary>移動処理</summary>
    public void OnMove(InputValue value) => _move = value.Get<Vector2>();
    
    /// <summary>弾を出す処理</summary>
    public void OnFire(InputValue value) => _fire = value.Get<float>() > 0;

    /// <summary>移動を遅くする処理</summary>
    public void OnSlowMove(InputValue value) => _slowMove = value.Get<float>() > 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PauseManager.OnPauseResume += OnStartPause;
    }   // ポーズ

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // ポーズ解除

    void Start()
    {
        _gm = GameManager.Instance;
        _power = PowerUp.Instance;
        _nowSpeed = _speed;
    }

    void Update()
    {
        if (!_pause)
        {

            if (_fire)
            {
                if (!_cooltime)
                {
                    _gm.AddScore(-100);
                    StartCoroutine(BulletCoolTime());
                }
            }   // 攻撃ボタンを押した場合、クールタイムを確認し、攻撃を行う

            if (_slowMove)
            {
                _nowSpeed = _lowSpeed;
            }   // 低速ボタンを押した場合、速度を低速モードに切り替える
            else
            {
                _nowSpeed = _speed;
            }   // 低速ボタンを離した場合、速度を元に戻す
        }
        else
        {
            _move = new Vector2(0,0);
        }
    }

    private void FixedUpdate()
    {
        if (!_pause)
        {
            Vector2 dir = _move.normalized;
            _rb.velocity = dir * _nowSpeed;
        }// プレイヤーの移動処理
    }

    // 他のオブジェクトとの衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_starTime && (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossFire"))
        {
            _gm.AddScore(-500);
            _starTime = true;
            StartCoroutine(AllAtackTime());
        }   // 無敵状態でない場合、敵の弾や敵、ボスの攻撃に当たった場合、お金を減らし、無敵状態に移行する

        if (collision.gameObject.tag == "GetMoneyFloor")
        {
            var moneys = GameObject.FindGameObjectsWithTag("Money");

            if (moneys.Length != 0)
            {
                foreach (var i in moneys)
                {
                    var moneyscript = i.GetComponent<PointMoneyScripts>();
                    moneyscript.MoneyGet(gameObject);
                }
            }
        }   // プレイヤーが「GetMoneyFloor」に衝突した場合、周囲にあるお金をプレイヤーに引き寄せる。
    }

    // 全体攻撃の無敵時間の処理
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
        }   //特定のポイントに生成ポイントを設置して、画面全体を爆発で埋め尽くす。

        yield return new WaitForSeconds(4f);
        _starTime = false;
        col.color = _normalColor;
        _airShipOnOff.SetActive(true);
        _cooltime = false;
    }

    // 弾のクールタイムの処理
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

    /// <summary>
    /// ポーズの状態が変更されたときの処理
    /// </summary>
    /// <param name="pause">ポーズの状態</param>
    public virtual void OnStartPause(bool pause)
    {
        if (pause)
        {
            _starTime = true;
            _pause = true;
            _rb.Sleep();
        }   // ポーズになった場合、無敵状態に移行し、プレイヤーの動きを停止する
        else
        {
            _starTime = false;
            _pause = false;
            _rb.WakeUp();
        }   // ポーズが解除された場合、無敵状態を解除し、プレイヤーの動きを再開する。
    }

}
