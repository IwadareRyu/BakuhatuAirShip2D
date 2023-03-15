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
    [Header("���Ƌ��̊Ԋu����́B")]
    [SerializeField]int num = 20;
    [SerializeField] float _bulletspeed = 1f;
    bool _interval;
    [SerializeField] float _angle = 0f;
    List<GameObject> _bulletList = new List<GameObject>();
    [Header("DanmakuState��Rotate�̏ꍇ�AColorState����͂��Ă��������B")]
    [SerializeField] BulletTypeClass.BulletState _danmakuState;
    [SerializeField] BulletTypeClass.BulletSpriteState _colorState;

    private void Start()
    {
        if(!_pool)
        {
            _pool = GameObject.FindGameObjectWithTag("MidBulletPool").GetComponent<BulletPoolActive>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_bulletime)
        {
            _bulletime = true;
            StartCoroutine(BulletTime());
        }
    }

    /// <summary>���̓����␔�A�p�^�[�������߂郁�\�b�h</summary>
    /// <returns></returns>
    IEnumerator BulletTime()
    {
        //�g�̏ꍇ�͉E�����Ɍ������Ĕg�A�������Ɍ������Ĕg���o���B
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
            for(var i = 0;i < 10;i++)
            {
                yield return new WaitForSeconds(_angleCount);
                OnePointIns(true,_angle,true);
            }
            yield return new WaitForSeconds(_angleCount);
            var num = _bulletList.Count;
            for(var j = 0; j < num;j++)
            {
                bulletcs = _bulletList[0].GetComponent<ActiveBullet>();
                bulletcs.StopTrue();
                _bulletList.RemoveAt(0);
            }
        }
        else
        {
            AllBulletIns(0);
        }
        yield return new WaitForSeconds(_count);
        _bulletime = false;
    }

    /// <summary>365�x�ϓ��ɋ����o�����\�b�h�B</summary>
    /// <param name="rad">���̊Ԋu</param>
    private void AllBulletIns(int rad)
    {
        //������ɏ�����������B
        for (var i = rad; i < 365; i += num)
        {
            //����pool������o���B
            var bullet = _pool.GetBullet();
            bulletcs = bullet.GetComponent<ActiveBullet>();
            //���̈ʒu���X�|�[���|�C���g�Ɉړ�����B
            bullet.transform.position = transform.position;
            //���̓�����ݒ肷�郁�\�b�h�ɑ���B
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
