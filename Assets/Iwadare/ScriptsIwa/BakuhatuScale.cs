using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class BakuhatuScale : MonoBehaviour
{
    [Tooltip("PowerUp�N���X�̎Q��")]
    PowerUp _power;
    [SerializeField, Tooltip("�S�̍U���̔����͈͂̍L��")]
    float _allPowerScale = 5;
    [Tooltip("�U���͈͂̉~�̔��a")]
    Vector2 _circleRadius;
    [Tooltip("�U���͈͂̉~�̒��S���W")]
    Vector2 _cirleCenter;
    [Tooltip("�U���t���O")]
    bool _attackbool;
    [SerializeField,Tooltip("�U���p�^�[���̑I���iNormal: �ʏ�U��, AllAttack: �S�̍U���j"),Header("�U���p�^�[���̑I���iNormal: �ʏ�U��, AllAttack: �S�̍U��)")]
    Pattern _pattern;

    [SerializeField,Tooltip("�J�����̐U���ݒ�̃N���X")]
    CinemachineImpulseSource _impulse;
    [Tooltip("�J�����U���̋���")]
    [SerializeField] float _impulsePower = 1;


    void Start()
    {
        // ���������Đ����A�J������U��������
        BGMManager.Instance.SEPlay(SE.Explosion);
        _impulse.GenerateImpulseAt(new Vector2(0, 0), new Vector2(0, _impulsePower));

        // �U���p�^�[���̕���
        if (_pattern == Pattern.Normal)
        {
            // �ʏ�U�����̑傫����ݒ肷��B
            _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
            transform.localScale = new Vector2(1.0f + _power._bakuhatuPower * 0.15f, 1.0f + _power._bakuhatuPower * 0.15f);
            _circleRadius = new Vector3(1.0f + _power._bakuhatuPower * 0.15f, 0);
            _cirleCenter = transform.position;
        }
        else
        {
            // �S�̍U�����̑傫����ݒ肷��B
            _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
            transform.localScale = new Vector2(_allPowerScale, _allPowerScale);
            _circleRadius = new Vector3(_allPowerScale, 0);
            _cirleCenter = transform.position;
        }
    }


    void Update()
    {
        // �U���͈͂��f�o�b�O�ŕ\��
        Debug.DrawLine(_cirleCenter, _cirleCenter + _circleRadius / 2);

        // �U���͈͂ɓ������ΏۑS����ۑ�����z��
        Collider2D[] attacklange;
        if (_pattern == Pattern.Normal)
        {
            // �ʏ�U�����̍U���͈�
            attacklange = Physics2D.OverlapCircleAll(_cirleCenter, (1.0f + _power._bakuhatuPower * 0.15f) / 2);
        }
        else
        {
            // �S�̍U�����̍U���͈�
            attacklange = Physics2D.OverlapCircleAll(_cirleCenter, _allPowerScale / 2);
        }

        if (!_attackbool)
        {
            foreach (var a in attacklange)
            {
                if (a.gameObject.tag == "Enemy")
                {
                    var dead = a.GetComponent<DestroyEnamy>();
                    dead.Damage();
                }   //�U���Ώۂ�Enemy�Ȃ�_���[�W(���S)
                else if (a.gameObject.tag == "Boss" && !_attackbool && _pattern == Pattern.Normal)
                {
                    var boss = a.GetComponent<BossGanerator>();
                    boss.AddBossDamage(-1.0f);
                }//�ʏ�U���ōU���Ώۂ�Enemy�Ȃ�1�_���[�W
                else if (a.gameObject.tag == "EnemyBullet" && _pattern == Pattern.AllAttack)
                {
                    var rb = a.GetComponent<Rigidbody2D>();
                    if (rb)
                    {
                        rb.simulated = true;
                    }
                    a.gameObject.SetActive(false);
                }   //�S�̍U���ŁA�G�̒e�Ȃ�e���g���Ă��Ȃ���Ԃɂ���B
            }
            _attackbool = true;
        }
    }

    /// <summary>�U���p�^�[����enum�^</summary>
    enum Pattern
    {
        /// <summary>�ʏ�U��</summary>
        Normal, 
        /// <summary>�S�̍U��</summary>
        AllAttack,
    }
}