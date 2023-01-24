using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDanmakuIns : MonoBehaviour
{
    [SerializeField] float _count = 5;
    [SerializeField] BulletPoolActive _pool;
    bool _bulletime;
    ActiveBossBullet bulletcs;
    float _rad;
    [Header("ãÖÇ∆ãÖÇÃä‘äuÇì¸óÕÅB")]
    [SerializeField]int num = 20;
    [SerializeField] float _bulletspeed = 1f;
    bool _interval;
    [Header("DanmakuStateÇ™RotateÇÃèÍçáÅAColorStateÇì¸óÕÇµÇƒÇ≠ÇæÇ≥Ç¢ÅB")]
    [SerializeField] BulletTypeClass.BulletState _danmakuState;
    [SerializeField] BulletTypeClass.BulletSpriteState _colorState;

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
        if(_danmakuState == BulletTypeClass.BulletState.Nami)
        {
            _colorState = BulletTypeClass.BulletSpriteState.RedFire;
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
                yield return new WaitForSeconds(0.5f);
                _interval = false;
            }
        }
        else if(_danmakuState == BulletTypeClass.BulletState.Ziki)
        {
            _colorState = BulletTypeClass.BulletSpriteState.BlueFire;
            OnePointIns();
        }
        else if(_danmakuState == BulletTypeClass.BulletState.AllZiki)
        {
            _colorState = BulletTypeClass.BulletSpriteState.BlueFire;
            for(var j = 0;j < 5;j++)
            {
                yield return new WaitForSeconds(0.1f);
                for (var k = 0; k < 5; k++)
                {
                    yield return new WaitForSeconds(0.01f);
                    OnePointIns();
                }
            }
        }
        else
        {
            AllBulletIns(0);
        }
        yield return new WaitForSeconds(_count);
        _bulletime = false;
    }

    private void AllBulletIns(int rad)
    {
        for (var i = rad; i < 365; i += num)
        {
            var bullet = _pool.GetBullet();
            bulletcs = bullet.GetComponent<ActiveBossBullet>();
            bullet.transform.position = transform.position;
            bulletcs.BulletAdd(i, _bulletspeed, _colorState);
        }
    }
    private void OnePointIns(bool stop = false)
    {
        var bullet = _pool.GetBullet();
        bulletcs = bullet.GetComponent<ActiveBossBullet>();
        bullet.transform.position = transform.position;
        bulletcs.BulletAdd(0, _bulletspeed, _colorState,stop);
    }
}
