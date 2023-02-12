using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGanerator : MonoBehaviour
{
    [SerializeField] Transform[] _targets;
    int _current;
    [SerializeField]float _bossMaxHP = 30;
    float _bossHP;
    [SerializeField]float _stopdis = 0.5f;
    [SerializeField] float _speed = 3f;
    [Header("使う弾幕パターンを入れていく。")]
    [SerializeField] GameObject[] _danmakuPattern;
    int _youso = 0;
    BossState _stateboss = BossState.Ten;
    bool _oneShot;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var i in _danmakuPattern)
        {
            i.SetActive(false);
        }
        _bossHP = _bossMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position,_targets[_current].position);
        if(distance > _stopdis)
        {
            Vector3 dir = (_targets[_current].position - transform.position).normalized * _speed;
            transform.Translate(dir * Time.deltaTime);
        }
        else
        {
            _current++;
            _current = _current % _targets.Length;
        }

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

            if(_bossHP / _bossMaxHP < 0.4f)
            {
                BulletReset();
                _youso = 3;
                _stateboss = BossState.Four;
            }
        }
        else if (_stateboss == BossState.Four)
        {
            if (!_oneShot)
            {
                _oneShot = true;
                StartCoroutine(StopTime());
            }

            if (_bossHP / _bossMaxHP < 0.2f)
            {
                BulletReset();
                _youso = 4;
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

            if (_bossHP <= 0f)
            {
                BulletReset();
                Destroy(gameObject);
            }
        }
    }

    private void BulletReset()
    {
        _oneShot = false;
        _danmakuPattern[_youso].SetActive(false);
        var bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
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
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(3f);
        _danmakuPattern[_youso].SetActive(true);
        int ram = (int)Random.Range(0f, 1.9f);
    }

    enum BossState
    {
        Ten,

        Eight,

        Six,

        Four,

        Two,
    }
}
