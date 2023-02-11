using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDanmakuIns : MonoBehaviour
{
    [SerializeField] float _count = 5;
    [SerializeField] float _angleCount = 1f;
    [SerializeField] BulletPoolActive _pool;
    bool _bulletime;
    ActiveBullet bulletcs;
    float _rad;
    [Header("球と球の間隔を入力。")]
    [SerializeField]int num = 20;
    [SerializeField] float _bulletspeed = 1f;
    bool _interval;
    [SerializeField] float _angle = 0f;
    List<GameObject> _bulletList = new List<GameObject>();
    [Header("DanmakuStateがRotateの場合、ColorStateを入力してください。")]
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

    /// <summary>球の動きや数、パターンを決めるメソッド</summary>
    /// <returns></returns>
    IEnumerator BulletTime()
    {
        //波の場合は右方向に向かって波、左方向に向かって波を出す。
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
        //
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
        else if(_danmakuState == BulletTypeClass.BulletState.StopBullet)
        {
            for(var i = 0;i < 11;i++)
            {
                yield return new WaitForSeconds(_angleCount);
                OnePointIns(true,_angle,true);
            }
            yield return new WaitForSeconds(_angleCount);
            foreach(var j in _bulletList)
            {
                bulletcs = j.GetComponent<ActiveBullet>();
                bulletcs.StopTrue();
            }
        }
        else
        {
            AllBulletIns(0);
        }
        yield return new WaitForSeconds(_count);
        _bulletime = false;
    }

    /// <summary>365度均等に球を出すメソッド。</summary>
    /// <param name="rad">球の間隔</param>
    private void AllBulletIns(int rad)
    {
        //球一つずつに処理を加える。
        for (var i = rad; i < 365; i += num)
        {
            //球をpoolから取り出す。
            var bullet = _pool.GetBullet();
            bulletcs = bullet.GetComponent<ActiveBullet>();
            //球の位置をスポーンポイントに移動する。
            bullet.transform.position = transform.position;
            //球の動きを設定するメソッドに代入。
            bulletcs.BulletAdd(i, _bulletspeed, _colorState);
        }
    }
    private void OnePointIns(bool stop = false, float angle = 0, bool listbool = false)
    {
        var bullet = _pool.GetBullet();
        bulletcs = bullet.GetComponent<ActiveBullet>();
        bullet.transform.position = transform.position;
        if(listbool)
        {
            _bulletList.Add(bullet);
        }
        bulletcs.BulletAdd(angle, _bulletspeed, _colorState,stop);
    }
}
