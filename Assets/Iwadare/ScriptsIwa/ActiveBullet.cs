using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBullet : MonoBehaviour
{
    [Tooltip("�e�̔�Ԍ���")]
    [SerializeField] float _angle;
    [Tooltip("�e�̑���")]
    [SerializeField] float _speed = 3f;
    [Tooltip("RotateBullet�̎��A�O�ɍL���邽�߂̕ϐ��B")]
    float dist = 0;
    [Tooltip("Rigidbody2D��velocity�ɑ�����邽�߂̕ϐ�")]
    Vector3 _velocity;
    [Tooltip("Rigidbody2D")]
    Rigidbody2D _rb;
    [Tooltip("SpriteRenderer")]
    SpriteRenderer _sprite;
    [Tooltip("�e�̈ʒu���ړ������邽�߂̕ϐ�")]
    Vector3 _transA;
    [Header("�������Ɉ�x�~�܂邩�ǂ����������t���O")]
    bool _stop;
    [Header("���̐F���A�^�b�`")]
    [Tooltip("���̐Ԃ��F")]
    [SerializeField] Sprite _redFire;
    [Tooltip("���̐��F")]
    [SerializeField] Sprite _blueFire;
    [Header("�X�t�B�A�̐F���A�^�b�`")]
    [Tooltip("���X�t�B�A")]
    [SerializeField] Sprite _bluesphere;
    [Tooltip("�Ԃ��X�t�B�A")]
    [SerializeField] Sprite _redsphere;
    [Tooltip("���̐F��`�ɂ���ē����𕪂��邽�߂�enum")]
    BulletTypeClass.BulletSpriteState _state;
    [Tooltip("�e���ꎞ��~���邩�ǂ����������t���O")]
    bool _stopbool;
    [Tooltip("�e�̈��ڂ̓����̕ω����s�����ǂ����������t���O")]
    bool _change1;
    [Tooltip("�e�̈��ڂ̓����̕ω��̎��Ƀv���C���[�Ɍ������ċ�����Ԃ��ǂ����������t���O")]
    bool _change1ziki;
    [Tooltip("�e�̓��ڂ̓����̕ω����s�����ǂ����������t���O")]
    bool _change2;
    [Header("�e��������ʒu"),Tooltip("�e��������ʒu�͈̔�")]
    [SerializeField] float yup = 8, ydown = -7, xup = 7, xdown = -7;
    [Tooltip("���ˉ�")]
    [SerializeField] AudioClip _fireOrShot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // ��ʂ̊O�ɏo���SetActive��false�ɂȂ�Reset�֐������s����B
        if (transform.position.x > xup || transform.position.x < xdown
            || transform.position.y > yup || transform.position.y < ydown)
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        // RotateBullet�̏ꍇ�̏����BState��LeftSphere,RightSphere���Ƃ��̏����ɂȂ�B
        if (_state == BulletTypeClass.BulletSpriteState.LeftSphere || _state == BulletTypeClass.BulletSpriteState.RightSphere)
        {
            // �������邮��񂷏����B�����X�|�[�������ꏊ�𒆐S�ɉ���Ă���B
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad) * dist;
            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad) * dist;
            _rb.velocity = _velocity;

            // �X�t�B�A�̐F�ɂ���ĉ�������ς���B
            if (_state == BulletTypeClass.BulletSpriteState.LeftSphere)
            {
                _angle += 0.3f;
            }
            else if (_state == BulletTypeClass.BulletSpriteState.RightSphere)
            {
                _angle -= 0.3f;
            }

            // dist�̃C���N�������g���čL���鋅������Ă���B
            dist += 0.1f;
        }

    }

    /// <summary>
    /// ���܂��������Ɍ������Ēe��^��������΂�����
    /// </summary>
    private void Type0Move()
    {
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        _rb.velocity = _velocity;
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }   //��x�e���~�߂鏈��
        if (_change1)
        {
            StartCoroutine(ChangeBullet());
        }   //�e�̓�����ς��鏈��
    }

    /// <summary>�v���C���[�Ɍ������Ĕ�Ԓe</summary>
    private void Type1Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Vector2 v = player.transform.position - transform.position;
            v = v.normalized * _speed;
            float zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
            _rb.velocity = v;
        }   //Player�Ɍ������Ă܂�������ԁB
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }   //��x�~�܂�e���~�߂����̏���
    }

    /// <summary>��x�e���~�߂Ă����莞�Ԃ�������ɓ����o�������B</summary>
    IEnumerator StopBullet()
    {
        _rb.simulated = false;
        _stopbool = false;
        yield return new WaitWhile(() => _stopbool == false);
        _rb.simulated = true;
    }

    /// <summary>�e�ɓ����̕ω������鏈���B</summary>
    IEnumerator ChangeBullet()
    {
        yield return new WaitForSeconds(1f);
        _rb.simulated = false;
        yield return new WaitForSeconds(1f);

        if (!_change1ziki)
        {
            _sprite.sprite = _bluesphere;
            _state = BulletTypeClass.BulletSpriteState.LeftSphere;
            _rb.simulated = true;
            _speed = 0.2f;
        }   //�E��肩�獶����
        else
        {
            _sprite.sprite = _blueFire;
            _rb.simulated = true;
            _speed = 5f;
            Type1Move();
        }   //���@�ɔ�΂�
        if (_change2)
        {
            yield return new WaitForSeconds(2f);
            _rb.simulated = false;
            yield return new WaitForSeconds(1f);
            _sprite.sprite = _redsphere;
            _state = BulletTypeClass.BulletSpriteState.RightSphere;
            _speed = -0.2f;
            _rb.simulated = true;
        }   //�Q��ڂ̕ω����s���Ƃ��̏����A�E���ɖ߂��B
    }

    /// <summary>�e��pool�ɕԂ��ۂɎ��s���鏈��</summary>
    public void Reset()
    {
        _transA = transform.position;
        _transA.y = 10f;
        transform.position = _transA;
        _rb.simulated = true;
        gameObject.SetActive(false);
    }

    /// <summary>Bullet�̓�����ݒ肷�鏈��</summary>
    /// <param name="angle">�e�̌���</param>
    /// <param name="speed">����</param>
    /// <param name="type">�e�̐F�A���������߂�state
    /// (�N���X��state�����߂Ă���̂ő��X�N���v�g��state�̒��g�͓���)</param>
    /// <param name="stop">�e���~�߂邩�~�߂Ȃ���
    /// (���͂��Ȃ��Ă������Ŏ~�߂Ȃ��ݒ�ɂ��Ă���)</param>
    public void BulletAdd(float angle, float speed, BulletTypeClass.BulletSpriteState type, bool stop = false, bool change1 = false, bool change2 = false, bool change1ziki = false)
    {
        //���͂���Ă����ϐ������ꂼ�����B
        _angle = angle;
        _speed = speed;
        _state = type;
        _stop = stop;
        _change1 = change1;
        _change2 = change2;
        _change1ziki = change1ziki;
        dist = 0;
        //state�ɂ���Ēe�̐F��ς�����A�����̏���������switch���B
        switch (_state)
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

    /// <summary>�e���ꎞ��~���邽�߂̊֐�</summary>
    public void StopTrue()
    {
        _stopbool = true;
    }

}
