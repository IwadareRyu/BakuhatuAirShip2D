using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGanerator : MonoBehaviour
{
    [SerializeField] HPBar _hpbar;
    [SerializeField] BossAttack _bossAttack;
    [SerializeField] Transform[] _targets;
    //int _current;
    [SerializeField]float _bossMaxHP = 30;
    float _bossHP;
    //[SerializeField]float _stopdis = 0.5f;
    //[SerializeField] float _speed = 3f;
    [Header("使う弾幕パターンを入れていく。")]
    [SerializeField] GameObject[] _danmakuPattern;
    int _youso = 0;
    BossState _stateboss = BossState.Ten;
    bool _oneShot;
    Animator _bossAni;
    [Header("体力が4割の時にオーバーモードに突入。")]
    [Tooltip("体力が4割の時にオーバーモードに突入。")]
    [SerializeField]bool _overMode;
    [Header("体力を４割にしたいとき活用してください。")]
    [SerializeField] bool _testMode;
    bool _coroutinebreak;
    [Header("落とすお金")]
    [SerializeField]GameObject[] _moneys;
    [Tooltip("落とすお金の確率")]
    [Header("落とすお金の確率")]
    [SerializeField][Range(0f,100f)] float[] _moneycount;
    [Tooltip("落とすお金の回数")]
    [Header("落とすお金の回数")]
    [SerializeField] int _dropCount = 10;
    bool _downbool;
    // Start is called before the first frame update
    void Start()
    {
        _bossAni = GetComponent<Animator>();
        foreach(var i in _danmakuPattern)
        {
            i.SetActive(false);
        }
        _bossHP = _bossMaxHP;
        if(_testMode)
        {
            _bossHP = _bossMaxHP * 0.4f;
        }
        if(_hpbar)
        {
            _hpbar.HPSlider(_bossHP / _bossMaxHP);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector2.Distance(transform.position,_targets[_current].position);
        //if(distance > _stopdis)
        //{
        //    Vector3 dir = (_targets[_current].position - transform.position).normalized * _speed;
        //    transform.Translate(dir * Time.deltaTime);
        //}
        //else
        //{
        //    _current++;
        //    _current = _current % _targets.Length;
        //}

        if(_stateboss == BossState.Ten)
        {
            if(!_oneShot)
            {
                _oneShot = true;
                _danmakuPattern[_youso].SetActive(true);
            }

            if(_bossHP / _bossMaxHP < 0.8f)
            {
                BulletReset();
                _youso = 1;
                _stateboss = BossState.Eight;
            }
        }
        else if(_stateboss == BossState.Eight)
        {
            if(!_oneShot)
            {
                _oneShot = true;
                StartCoroutine(StopTime());
            }

            if(_bossHP / _bossMaxHP < 0.6f)
            {
                BulletReset();
                _youso = 2;
                _stateboss = BossState.Six;
            }
        }
        else if(_stateboss == BossState.Six)
        {
            if (!_oneShot)
            {
                _oneShot = true;
                StartCoroutine(StopTime());
            }

            if (_bossHP / _bossMaxHP < 0.4f)
            {
                BulletReset();
                _youso = 3;
                _stateboss = BossState.Four;
            }
        }
        else if (_stateboss == BossState.Four)
        {
            if (!_oneShot && _overMode)
            {
                _oneShot = true;
                _coroutinebreak = true;
                _bossAttack.OverMode();
                Debug.Log("ここには入るね");
            }
            else if (!_oneShot)
            {
                _oneShot = true;
                StartCoroutine(StopTime());
            }

            if (_bossHP / _bossMaxHP < 0.2f)
            {
                BulletReset();
                _youso = 4;
                _coroutinebreak = false;
                if(_overMode)
                {
                   _bossAttack.OverMode();
                }
                _stateboss = BossState.Two;
            }
        }
        else if (_stateboss == BossState.Two)
        {
            if (!_oneShot)
            {
                _oneShot = true;
                StartCoroutine(StopTime());
            }

            if (_bossHP <= 0f && !_downbool)
            {
                BulletReset();
                _oneShot = true;
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

    private void BulletReset()
    {
        _oneShot = false;
        _danmakuPattern[_youso].SetActive(false);
        var bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        DropMoney();
        foreach (var i in bullets)
        {
            var bullet = i.GetComponent<ActiveBullet>();
            bullet.Reset();
        }
    }

    public void AddBossDamage(float damage)
    {
        _bossHP += damage;
        Debug.Log(_bossHP);
        if(_hpbar)
        {
            _hpbar.HPSlider(_bossHP / _bossMaxHP);
        }
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(3f);
        if(_coroutinebreak)
        {
            yield break;
        }
        _danmakuPattern[_youso].SetActive(true);
        //int ram = (int)Random.Range(0f, 1.9f);
    }

    enum BossState
    {
        Ten,

        Eight,

        Six,

        Four,

        Two,
    }

    /// <summary>
    /// お金を落とすメソッド
    /// </summary>
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

    public void DownAudioPlay()
    {
        SEManager.Instance.SEPlay(SEManager.SE.Bakuhatu);
    }

    public void DownEnemy()
    {
        GameManager.Instance.SetDeadEnemy(-1);
        Destroy(gameObject);
    }

    void InsMoney(int i)
    {
        var ram1 = Random.Range(-1f,1f);
        var ram2 = Random.Range(-1f, 1f);
        Vector2 vec = new Vector2(transform.position.x + ram1,transform.position.y + ram2);
        Instantiate(_moneys[i], vec, Quaternion.identity);
    }
}
