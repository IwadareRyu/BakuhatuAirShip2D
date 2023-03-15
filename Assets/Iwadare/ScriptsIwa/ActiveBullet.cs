using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBullet : MonoBehaviour
{
    [Tooltip("���Ԍ���")]
    [SerializeField] float _angle;
    [Tooltip("����")]
    [SerializeField] float _speed = 3f;
    [Tooltip("RotateBullet�̎��A�O�ɍL���邽�߂̕ϐ��B")]
    float dist = 0;
    [Tooltip("_rb.velocity�ɑ�����邽�߂̕ϐ��B")]
    Vector3 _velocity;
    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    [Tooltip("����Position���ړ�������ϐ��B")]
    Vector3 _transA;
    [Header("��������x�~�܂鋅")]
    bool _stop;
    [Header("���̐F���A�^�b�`")]
    [SerializeField] Sprite _redFire;
    [SerializeField] Sprite _blueFire;
    [Header("�X�t�B�A�̐F���A�^�b�`")]
    [SerializeField] Sprite _bluesphere;
    [SerializeField] Sprite _redsphere;
    [Tooltip("���̐F�A�`�ɂ���ē����𕪂��邽�߂�enum")]
    BulletTypeClass.BulletSpriteState _state;
    bool _stopbool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //��ʂ̊O�ɏo��ƁASetActive��false�ɂȂ�Reset�֐��B
        if (transform.position.x > 7 || transform.position.x < -7
            || transform.position.y > 8 || transform.position.y < -7)
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        //RotateBullet�̏ꍇ�̏����BState��Sphere���Ƃ��̏����ɂȂ�B
        if (_state == BulletTypeClass.BulletSpriteState.LeftSphere || _state == BulletTypeClass.BulletSpriteState.RightSphere)
        {
            //�������邮��񂷏����B�����X�|�[�������ꏊ�𒆐S�ɉ���Ă���B
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad) * dist;
            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad) * dist;
            _rb.velocity = _velocity;

            //�X�t�B�A�̐F�ɂ���ĉ�������ς���B
            if(_state == BulletTypeClass.BulletSpriteState.LeftSphere)
            {
                _angle += 0.3f;
            }
            else if(_state == BulletTypeClass.BulletSpriteState.RightSphere)
            {
                _angle -= 0.3f;
            }

            //dist�̃C���N�������g���čL���鋅������Ă���B
            dist += 0.1f;
        }
        
    }

    /// <summary>���̌����ɉ����Ă܂�������ԋ��B</summary>
    private void Type0Move()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        _rb.velocity = _velocity;
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }
    }

    /// <summary>�v���C���[�Ɍ������Ĕ�ԋ�</summary>
    private void Type1Move()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            Vector2 v = player.transform.position - transform.position;
            v = v.normalized * _speed;
            float zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
            _rb.velocity = v;
        }
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }
    }

    /// <summary>��x�����~�߂Ă����莞�Ԃ�������ɓ����o�������B</summary>
    IEnumerator StopBullet()
    {
        _rb.simulated = false;
        _stopbool = false;
        yield return new WaitWhile(() => _stopbool == false);
        _rb.simulated = true;
    }

    /// <summary>����pool�ɕԂ��ۂɎ��s���鏈��</summary>
    public void Reset()
    {
        _transA = transform.position;
        _transA.y = 10f;
        transform.position = _transA;
        _rb.simulated = true;
        gameObject.SetActive(false);
    }

    /// <summary>Bullet�̓�����ݒ肷�鏈��</summary>
    /// <param name="angle">���̌���</param>
    /// <param name="speed">����</param>
    /// <param name="type">���̐F�A���������߂�state
    /// (�N���X��state�����߂Ă���̂ő��X�N���v�g��state�̒��g�͓���)</param>
    /// <param name="stop">�����~�߂邩�~�߂Ȃ���
    /// (���͂��Ȃ��Ă������Ŏ~�߂Ȃ��ݒ�ɂ��Ă���)</param>
    public void BulletAdd(float angle, float speed, BulletTypeClass.BulletSpriteState type,bool stop = false,bool change = false)
    {
        //���͂���Ă����ϐ������ꂼ�����B
        _angle = angle;
        _speed = speed;
        _state = type;
        _stop = stop;
        dist = 0;
        //state�ɂ���ċ��̐F��ς�����A�����̏���������switch���B
        switch(_state)
        {
            case BulletTypeClass.BulletSpriteState.LeftSphere:
                _sprite.sprite = _bluesphere;
                break;
            case BulletTypeClass.BulletSpriteState.RightSphere:
                _sprite.sprite = _redsphere;
                break;
            case BulletTypeClass.BulletSpriteState.RedFire:
                _sprite.sprite = _redFire;
                Type0Move();
                break;
            case BulletTypeClass.BulletSpriteState.BlueFire:
                _sprite.sprite = _blueFire;
                Type1Move();
                break;
        }
    }
    public void StopTrue()
    {
        _stopbool = true;
    }
}
