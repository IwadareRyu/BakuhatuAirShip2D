using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGanerator : MonoBehaviour
{
    [Tooltip("ボスのHPバー")]
    [SerializeField]
    HPBar _hpbar;
    
    [Tooltip("BossAttackスクリプト")]
    [SerializeField]
    BossAttack _bossAttack;
    
    [Tooltip("ボスの最大体力")]
    [SerializeField]
    float _bossMaxHP = 30;
    
    [Tooltip("ボスの現在の体力")]
    float _bossHP;
    
    [Header("使う弾幕パターンを入れていく。"),Tooltip("使う弾幕のパターン")]
    [SerializeField]
    GameObject[] _danmakuPattern;
    
    [Tooltip("現在の弾幕パターンのインデックス")]
    int _danmakuIndex = 0;
    
    [Tooltip("ボスの状態を示す列挙型")]
    BossState _stateBoss = BossState.Ten;
    
    [Tooltip("ボスの状態が変化するときに一度だけ処理を動かすフラグ")]
    bool _stateOneShot;
    
    [Tooltip("ボスのアニメーター")]
    Animator _bossAni;

    [Header("体力が4割の時にオーバーモードに突入するフラグ"),
        Tooltip("体力が4割の時にオーバーモードに突入するフラグ")]
    [SerializeField]
    bool _overMode;
    
    [Header("体力を４割にしたいとき活用してください"),
        Tooltip("体力を4割にするときのフラグ")]
    [SerializeField]
    bool _testMode;

    [Tooltip("弾幕を出すのを中断するフラグ")]
    bool _coroutineBreak;

    [Header("落とすお金"),
        Tooltip("落とすお金の配列")]
    [SerializeField]
    GameObject[] _moneys;

    [Tooltip("落とすお金の確率"),
        Header("落とすお金の確率")]
    [SerializeField,Range(0f, 100f)] 
    float[] _moneycount;

    [Tooltip("ボスの状態が切り替わるときに落とすお金の数"),
        Header("ボスの状態変化で落とすお金の数")] 
    [SerializeField]
    int _dropCount = 10;

    [Tooltip("ボスが倒された時のフラグ")]
    bool _downbool;

    [Tooltip("ゲームが一時停止中かどうかのフラグ")]
    bool _pause;

    // 一時停止のイベント登録
    private void OnEnable()
    {
        PauseManager.OnPauseResume += StartPause;
    }

    // 一時停止のイベント解除
    private void OnDisable()
    {
        PauseManager.OnPauseResume -= StartPause;
    }

    void Start()
    {
        _bossAni = GetComponent<Animator>();

        // 弾幕パターンを非アクティブにする
        foreach (var i in _danmakuPattern)
        {
            i.SetActive(false);
        }
        _bossHP = _bossMaxHP;

        // テストモードなら体力を4割に設定
        if (_testMode)
        {
            _bossHP = _bossMaxHP * 0.4f;
        }

        // 体力バーの初期化
        if (_hpbar)
        {
            _hpbar.HPSlider(_bossHP / _bossMaxHP);
        }
    }

    void Update()
    {
        //ポーズ処理
        if (!_pause)
        {
            // Bossの体力が10割の時の処理。
            if (_stateBoss == BossState.Ten)
            {
                if (!_stateOneShot)
                {
                    _stateOneShot = true;
                    _danmakuPattern[_danmakuIndex].SetActive(true);
                }   //登録されている弾幕パターン1つ目の発動

                if (_bossHP / _bossMaxHP < 0.8f)
                {
                    BulletResetNextBossState();
                    _danmakuIndex = 1;
                    _stateBoss = BossState.Eight;
                }
            }
            // Bossの体力が8割の時の処理。
            else if (_stateBoss == BossState.Eight)
            {
                if (!_stateOneShot)
                {
                    _stateOneShot = true;
                    StartCoroutine(StopTime());
                }   //登録されている弾幕パターン2つ目の発動

                if (_bossHP / _bossMaxHP < 0.6f)
                {
                    BulletResetNextBossState();
                    _danmakuIndex = 2;
                    _stateBoss = BossState.Six;
                }
            }
            // Bossの体力が6割の時の処理。
            else if (_stateBoss == BossState.Six)
            {
                if (!_stateOneShot)
                {
                    _stateOneShot = true;
                    StartCoroutine(StopTime());
                }   //登録されている弾幕パターン3つ目の発動

                if (_bossHP / _bossMaxHP < 0.4f)
                {
                    BulletResetNextBossState();
                    _danmakuIndex = 3;
                    _stateBoss = BossState.Four;
                }
            }
            // Bossの体力が4割の時の処理。
            else if (_stateBoss == BossState.Four)
            {
                if (!_stateOneShot && _overMode)
                {
                    _stateOneShot = true;
                    _coroutineBreak = true;
                    _bossAttack.OverMode();
                    Debug.Log("ここには入るね");
                }   //オーバーモードフラグがtrueの時、オーバーモードの発動。
                else if (!_stateOneShot)
                {
                    _stateOneShot = true;
                    StartCoroutine(StopTime());
                }   //登録されている弾幕パターン4つ目の発動

                if (_bossHP / _bossMaxHP < 0.2f)
                {
                    BulletResetNextBossState();
                    _danmakuIndex = 4;
                    _coroutineBreak = false;
                    if (_overMode)
                    {
                        _bossAttack.OverMode();
                    }
                    _stateBoss = BossState.Two;
                }
            }
            // Bossの体力が2割の時の処理。
            else if (_stateBoss == BossState.Two)
            {
                if (!_stateOneShot)
                {
                    _stateOneShot = true;
                    StartCoroutine(StopTime());
                }   //登録されている弾幕パターン5つ目の発動

                if (_bossHP <= 0f && !_downbool)
                {
                    BulletResetNextBossState();
                    _stateOneShot = true;
                    _downbool = true;
                    if (_bossAni)
                    {
                        _bossAni.Play("DownAni");
                    }
                    else
                    {
                        GameManager.Instance.SetDeadEnemy(-1);
                    }

                }
            }
        }
    }

    /// <summary>画面内の弾を全て消して、ボスの状態を次の段階に移行する処理。</summary>
    private void BulletResetNextBossState()
    {
        _stateOneShot = false;
        _danmakuPattern[_danmakuIndex].SetActive(false);
        var bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        DropMoney();
        foreach (var i in bullets)
        {
            var bullet = i.GetComponent<ActiveBullet>();
            bullet.Reset();
        }   // 画面内の弾をpoolに返す。
    }

    /// <summary>ボスにダメージを与える処理</summary>
    /// <param name="damage">ダメージの値</param>
    public void AddBossDamage(float damage)
    {
        _bossHP += damage;
        Debug.Log(_bossHP);
        if (_hpbar)
        {
            _hpbar.HPSlider(_bossHP / _bossMaxHP);
        }   // 体力バーを更新する
    }

    /// <summary>弾幕パターンの停止時間のコルーチン</summary>
    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(3f);
        if (_coroutineBreak)
        {
            yield break;
        }
        _danmakuPattern[_danmakuIndex].SetActive(true);
    }

    /// <summary>お金を落とすメソッド</summary>
    void DropMoney()
    {
        for (var i = 0; i < _dropCount; i++)
        {
            int ram = Random.Range(0, 100);
            if (ram > _moneycount[1])
            {
                InsMoney(2);
            }
            else if (ram > _moneycount[0])
            {
                InsMoney(1);
            }
            else
            {
                InsMoney(0);
            }
        }
    }

    /// <summary>お金を生成するメソッド</summary>
    /// <param name="i">配列</param>
    void InsMoney(int i)
    {
        var ram1 = Random.Range(-1f, 1f);
        var ram2 = Random.Range(-1f, 1f);
        Vector2 vec = new Vector2(transform.position.x + ram1, transform.position.y + ram2);
        Instantiate(_moneys[i], vec, Quaternion.identity);
    }

    /// <summary>ボスが倒された時の音再生</summary>
    public void DownAudioPlay()
    {
        BGMManager.Instance.SEPlay(SE.Explosion);
    }

    /// <summary>ボスを倒す処理</summary>
    public void DownEnemy()
    {
        GameManager.Instance.SetDeadEnemy(-1);
        Destroy(gameObject);
    }


    /// <summary>一時停止の処理</summary>
    /// <param name="pause">ポーズ処理</param>
    public void StartPause(bool pause)
    {
        _pause = pause;
    }

    /// <summary>ボスの状態を示す列挙型</summary>
    enum BossState
    {
        //体力の割合
        Ten,

        Eight,

        Six,

        Four,

        Two,
    }
}