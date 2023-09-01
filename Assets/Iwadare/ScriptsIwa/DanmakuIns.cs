using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmakuIns : MonoBehaviour
{
    [Tooltip(" �e�̔��ˊԊu")]
    [SerializeField] 
    float _count = 5;

    [Tooltip("�e�̔��ˊԊu(_count�ƕʂ̎g��������΂悩�����ȂƂ�������)")]
    [SerializeField] 
    float _angleCount = 1f;

    [Tooltip("�g�p����e�̃v�[��")]
    [SerializeField]
    BulletPoolActive _pool;

    [Tooltip("�e���̔��˃t���O")]
    bool _bulletTime;

    [Tooltip("�e�̃X�N���v�g")]
    ActiveBullet bulletCs;

    [Header("���Ƌ��̊Ԋu����́B"),Tooltip("�����m�̊Ԋu")]
    [SerializeField]
    int _num = 20;

    [Tooltip("�e�̑��x")]
    [SerializeField]
    float _bulletspeed = 1f;

    [Tooltip("�g�e���̐؂�ւ��t���O")]
    bool _interval;

    [Tooltip("�e���̔��ˊp�x")]
    [SerializeField] 
    float _angle = 0f;

    [Tooltip("�e�̃��X�g")]
    List<GameObject> _bulletList = new List<GameObject>();

    [Header("DanmakuState��Rotate�̏ꍇ�AColorState����͂��Ă��������B"),Tooltip("�e���̃^�C�v")]
    [SerializeField] 
    BulletTypeClass.BulletState _danmakuState;

    [Tooltip("�e�̃^�C�v")]
    [SerializeField]
    BulletTypeClass.BulletSpriteState _colorState;

    [Tooltip("�e�̋O������x�ς��e���̃^�C�v�̎��Ɏ��@�_���̋��ɂ��邩�ǂ����̃t���O")]
    [SerializeField] 
    bool _zikiMode;

    [Tooltip("���O�����ǂ����̃t���O")]
    [SerializeField]
    bool _mogura;

    [Tooltip("�|�[�Y")]
    bool _pause;

    private void Start()
    {
        if (!_pool)
        {
            _pool = GameObject.FindGameObjectWithTag("MidBulletPool").GetComponent<BulletPoolActive>();
        }   //�v�[���Q��
    }

    private void OnEnable()
    {
        _bulletTime = false;
        PauseManager.OnPauseResume += OnStartPause;
    }   // �|�[�Y

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // �|�[�Y����

    void Update()
    {
        // �|�[�Y���Ă��Ȃ��ꍇ�ɂ̂ݒe�������s
        if (!_pause)
        {
            if (!_bulletTime)
            {
                _bulletTime = true;
                StartCoroutine(BulletTime());
            }   //�t���O��false�ɂȂ�����e���̎��s�B
        }
    }

    /// <summary>���̓����␔�A�p�^�[�������߂郁�\�b�h</summary>
    /// <returns></returns>
    IEnumerator BulletTime()
    {
        // �e���̃^�C�v��Nami�̏ꍇ
        if (_danmakuState == BulletTypeClass.BulletState.Nami)
        {
            StartCoroutine(NamiBullet());
        }
        // �e���̃^�C�v��Ziki�i�P�����@�_���j�̏ꍇ
        else if (_danmakuState == BulletTypeClass.BulletState.Ziki)
        {
            StartCoroutine(SingleZikiBullet());
        }
        // �e���̃^�C�v��AllZiki�i�������@�_���j�̏ꍇ
        else if (_danmakuState == BulletTypeClass.BulletState.AllZiki)
        {
            StartCoroutine(MaltiZikiBullet());
        }
        // �e���̃^�C�v��StopBullet�̏ꍇ
        else if (_danmakuState == BulletTypeClass.BulletState.StopBullet)
        {
            StartCoroutine(StopBullet());
        }
        // �e���̃^�C�v��IllusionBullet�i�����e�j�̏ꍇ
        else if (_danmakuState == BulletTypeClass.BulletState.IllusionBullet)
        {
            StartCoroutine(IllusionBullet());
        }
        // ���̑��̃^�C�v�̏ꍇ
        else
        {
            //�����̒e�̐����̎��s
            AllBulletIns(0);
            if (_mogura)
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
            }
            else
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
            }   //�e�̉��̍Đ�
            yield return new WaitForSeconds(_count);
            _bulletTime = false;
        }
        yield return null;
    }

    /// <summary>NamiBullet�̏ꍇ�̏���</summary>
    IEnumerator NamiBullet()
    {
        //�e�̃^�C�v�̕ύX
        _colorState = BulletTypeClass.BulletSpriteState.RedFire;

        //�C���^�[�o���̐؂�ւ��Ō��݂ɔg�̒e���𐶐��B
        if (!_interval)
        {
            for (var i = 0; i <= 15; i += 5)
            {
                //�����̒e�̐���
                AllBulletIns(i); 
                if (_mogura)
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                }
                else
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                }   //SE�̍Đ�����(���O���ƃh���S���ŉ����𕪂���B)
                yield return new WaitForSeconds(_count);
            }
            _interval = true;
        }
        else
        {
            for (var i = 15; i >= 0; i -= 5)
            {
                //�����̒e�̐���
                AllBulletIns(i);
                if (_mogura)
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                }
                else
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                }   //SE�̍Đ�
                yield return new WaitForSeconds(_count);
            }
            yield return new WaitForSeconds(0.5f);
            _interval = false;
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>�P���̎��@�_���e�̏ꍇ</summary>
    IEnumerator SingleZikiBullet()
    {

        _colorState = BulletTypeClass.BulletSpriteState.BlueFire;
        //�P���̒e����
        OnePointIns();
        if (_mogura)
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
        }
        else
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }


    /// <summary>�����̎��@�_���e�̏ꍇ</summary>
    IEnumerator MaltiZikiBullet()
    {
        _colorState = BulletTypeClass.BulletSpriteState.BlueFire;
        for (var j = 0; j < 5; j++)
        {
            yield return new WaitForSeconds(0.1f);
            //for���Ń��[�v���Ď��@�_���̋�����������o���B
            for (var k = 0; k < 5; k++)
            {
                yield return new WaitForSeconds(0.01f);
                //�P���̒e�̐���
                OnePointIns(); 
                if (_mogura)
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                }
                else
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                }
            }
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>StopBullet(�ꎞ��~����e)�̏���</summary>
    IEnumerator StopBullet()
    {
        //�r���Ŏ~�܂�e���΂��āA�ēx�e�𓮂�����悤�ɒe�̃��X�g������Ă����B
        for (var i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(_angleCount);
            //�P���e����(���X�g���g���Đ��������e�̃��X�g�����A)
            OnePointIns(true, _angle, true);
            if (_mogura)
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
            }
            else
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
            }   //SE�Đ�
        }
        yield return new WaitForSeconds(_angleCount);
        var num = _bulletList.Count;

        //ActiveBullet�̃X�N���v�g�ōēx�e�𓮂����āA���X�g����ɂ���B
        for (var j = 0; j < num; j++)
        {
            bulletCs = _bulletList[0].GetComponent<ActiveBullet>();
            bulletCs.StopTrue();
            _bulletList.RemoveAt(0);
            if (_mogura)
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
            }
            else
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
            }   //SE�Đ�
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>IllusionBullet(�����e)�̏���</summary>
    IEnumerator IllusionBullet()
    {
        //180�x�ɒP���e����
        OnePointIns(false, 180, true);
        //0�x�ɒP���e����
        OnePointIns(false, 0, true);
        if (_mogura)
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
        }
        else
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
        }   //SE����
        yield return new WaitForSeconds(_angleCount);
        if (_bulletList.Count != 0)
        {
            //�����e(����)
            for (var i = _bulletList.Count; i > 0; i--)
            {
                if (_bulletList[i - 1])
                {
                    if (_bulletList[i - 1].active)
                    {
                        AllBulletIns(0, true);
                        if (_mogura)
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                        }
                        else
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                        }
                        _bulletList.RemoveAt(i - 1);
                    }
                }
                else
                {
                    _bulletList.RemoveAt(i - 1);
                }
            }
        }
        yield return new WaitForSeconds(_angleCount);
        if (_bulletList.Count != 0)
        {
            //�����e(2���)
            for (var i = _bulletList.Count; i > 0; i--)
            {
                if (_bulletList[i - 1])
                {
                    if (_bulletList[i - 1].active)
                    {
                        AllBulletIns(0, true);
                        if (_mogura)
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                        }
                        else
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                        }
                        _bulletList.RemoveAt(i - 1);
                    }
                }
                else
                {
                    _bulletList.RemoveAt(i - 1);
                }
            }
        }
        _bulletList.Clear();
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>365�x�ϓ��ɋ����o�����\�b�h�B</summary>
    /// <param name="rad">���̊Ԋu</param>
    /// <param name="illusionPosBool">�����e�̍ہA���􂷂�Ƃ��̈ʒu��ς���ۂ̃t���O</param>
    private void AllBulletIns(int rad, bool illusionPosBool = false)
    {
        //������ɏ�����������B
        for (var i = rad; i < 365; i += _num)
        {
            //����pool������o���B
            var bullet = _pool.GetBullet();
            bulletCs = bullet.GetComponent<ActiveBullet>();
            //���̈ʒu���X�|�[���|�C���g�Ɉړ�������B
            if (!illusionPosBool)
            {
                bullet.transform.position = transform.position;
            }
            else if (_bulletList.Count != 0)
            {
                bullet.transform.position = _bulletList[0].transform.position;
            }
            //���e�̓�����ς���Ƃ��̏���
            if (_danmakuState == BulletTypeClass.BulletState.ChangeBulletOne)
            {
                if (_zikiMode)
                {
                    bulletCs.BulletAdd(i, _bulletspeed, _colorState, false, true, false, true);
                }
                else
                {
                    bulletCs.BulletAdd(i, _bulletspeed, _colorState, false, true);
                }
            }
            //�Q�񓮂���ς���Ƃ��̏���
            else if (_danmakuState == BulletTypeClass.BulletState.ChangeBulletTwo)
            {
                bulletCs.BulletAdd(i, _bulletspeed, _colorState, false, true, true);
            }
            //����ȊO�̎��̏���
            else
            {
                bulletCs.BulletAdd(i, _bulletspeed, _colorState);
            }
            //illusionbullet�̎��A���X�g�ɒǉ��B
            if (illusionPosBool)
            {
                _bulletList.Add(bullet);
            }
        }

        //�������I����������e�����X�g����폜�B
        if (illusionPosBool)
        {
            _bulletList.RemoveAt(0);
        }
    }

    /// <summary>�P���Œe���o������</summary>
    /// <param name="stop">�e���~�߂邩�ǂ����̃t���O</param>
    /// <param name="angle">�e���΂��p�x</param>
    /// <param name="listbool">���X�g�ɒǉ����邩�̃t���O</param>
    private void OnePointIns(bool stop = false, float angle = 0, bool listbool = false)
    {
        var bullet = _pool.GetBullet();
        bulletCs = bullet.GetComponent<ActiveBullet>();
        bullet.transform.position = transform.position;
        if (listbool)
        {
            _bulletList.Add(bullet);
        }
        bulletCs.BulletAdd(angle, _bulletspeed, _colorState, stop);
    }

    /// <summary>�|�[�Y����</summary>
    /// <param name="pause">�|�[�Y�t���O</param>
    public virtual void OnStartPause(bool pause)
    {
        _pause = pause;
    }
}