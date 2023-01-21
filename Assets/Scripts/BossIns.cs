using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIns : MonoBehaviour
{
    [SerializeField] float _count = 5;
    [SerializeField] BulletPoolActive _pool;
    bool _bulletime;
    ActiveBossBullet bulletcs;
    float _rad;
    int num = 20;
    [SerializeField] float _bulletspeed = 1f;
    [Header("0:直線redFire、1:右回り、2:左回り、3:直線blueFire")]
    [SerializeField,Range(0,3)] int _bulletType = 0;
    [Header("0:弾幕パターン1(波)、1:パターン2(回転)、2:パターン3(自機攻撃)、3:パターン4")]
    [SerializeField, Range(0, 3)] int _danmakuPattern = 0;
    bool _interval;
    int _type = 0;

    // Update is called once per frame
    void Update()
    {
        if (!_bulletime)
        {
            _bulletime = true;
            StartCoroutine(BulletTime());
        }
    }

    IEnumerator BulletTime()
    {
        yield return new WaitForSeconds(_count);
        if(_danmakuPattern == 0)
        {
            _bulletType = 0;
            if(!_interval)
            {
                for (var i = 0; i <= 15; i += 5)
                {
                    AllBulletIns(i);
                    yield return new WaitForSeconds(_count);
                }
                _interval = true;
            }
            else
            {
                for (var i = 15; i >= 0; i -= 5)
                {
                    AllBulletIns(i);
                    yield return new WaitForSeconds(_count);
                }
                _interval = false;
            }
        }
        else if(_danmakuPattern == 2)
        {
            _bulletType = 3;
            OnePointIns();
        }
        else
        {
            AllBulletIns(0);
        }
        _bulletime = false;
    }

    private void AllBulletIns(int rad)
    {
        for (var i = rad; i < 365; i += num)
        {
            var bullet = _pool.GetBullet();
            bulletcs = bullet.GetComponent<ActiveBossBullet>();
            bullet.transform.position = transform.position;
            bulletcs.BulletAdd(i, _bulletspeed, _bulletType);
        }
    }
    private void OnePointIns()
    {
        var bullet = _pool.GetBullet();
        bulletcs = bullet.GetComponent<ActiveBossBullet>();
        bullet.transform.position = transform.position;
        bulletcs.BulletAdd(0, _bulletspeed, _bulletType);
    }
}
