using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Tooltip("�U���t���O")]
    bool _attackBool;
    [Tooltip("��x�����̏������s���t���O")]
    bool _oneShot;
    [SerializeField,Tooltip("�I�[�o�[���[�h�U���̃I�u�W�F�N�g")]
    GameObject _overAttack; 
    [SerializeField,Tooltip("�v���C���[�Ɍ������Ĕ��˂���I�u�W�F�N�g")] 
    GameObject _ownFire;
    [SerializeField,Tooltip("�e�̃I�u�W�F�N�g�ꗗ�̔z��")] 
    GameObject[] _bulletSpawns;
    [SerializeField,Tooltip("�ړ��|�C���g��Transform�ꗗ�̔z��")] 
    Transform[] _movePoint;
    [SerializeField,Tooltip("�ړ���~����")] 
    float _stopDistance = 0.5f;
    [SerializeField,Tooltip("�ړ����x")] 
    float _moveSpeed = 3f;
    [SerializeField,Tooltip("�U�����̈ړ����x")]
    float _attackSpeed = 5f;
    [SerializeField,Tooltip("�U�����̎���")]
    float _attackTime = 2f;
    [SerializeField,Tooltip("�{�X�̏�Ԃ̗񋓌^")] 
    BossState _bossState;
    [Tooltip("�ړ����~����t���O")]
    bool _stopBool;
    [Tooltip("���O���̍U���t���O")]
    bool _moleAttackBool;
    [Tooltip("�����_���ȕϐ�")]
    int _random;

    // Start is called before the first frame update
    void Start()
    {
        // ����������
        _ownFire.SetActive(false);
        _overAttack.SetActive(false);
        if (_bulletSpawns.Length != 0)
        {
            foreach (var i in _bulletSpawns)
            {
                i.SetActive(false);
            }
        }
    }

    void Update()
    {
        //�U������Ƃ�
        if (_attackBool)
        {
            if (!_oneShot)
            {
                _random = (int)Random.Range(0, 2.9f);
                _stopBool = false;
                StartCoroutine(AttackTime()); 
                _oneShot = true;
            }   //�ړ������āA�U�������̃R���[�`�����Ăяo���AUpdate�ŉ��Ȃ��悤�t���O��true�ɂ���B
            float distance = Vector2.Distance(transform.position, _movePoint[_random].position);
            if (distance > _stopDistance && !_stopBool)
            {
                Vector3 dir = (_movePoint[_random].position - transform.position).normalized * _moveSpeed;
                transform.Translate(dir * Time.deltaTime);
            }   //����̒n�_�ɂ��ǂ蒅���܂ł��̒n�_�Ɍ������Ĉړ�
            else
            {
                _stopBool = true;
            }    //����̒n�_�ɂ��ǂ蒅������A�ړ����~
            if (_moleAttackBool)
            {
                Vector3 dir = Vector3.down.normalized * _attackSpeed;
                transform.Translate(dir * Time.deltaTime);
            }   //������̍U�����ɉ�������������B
        }
    }

    /// <summary>�U���̃R���[�`��</summary>
    IEnumerator AttackTime()
    {
        //�ړ�����~����܂őҋ@
        yield return new WaitWhile(() => _stopBool == false);
        yield return new WaitForSeconds(2f);
        if (_attackBool)
        {
            //�{�X���h���S���̎��̏���
            if (_bossState == BossState.Drgon)
            {
                //�I�[�o�[�U�����A�N�e�B�u�ɂ���B
                _overAttack.SetActive(true);
                BGMManager.Instance.SEPlay(BGMManager.SE.FireBreath);
                yield return new WaitForSeconds(0.2f);
                if (_bulletSpawns.Length > 2)
                {
                    var ram = Random.Range(0, 2);
                    for (var i = ram; i < _bulletSpawns.Length; i += 2)
                    {
                        _bulletSpawns[i].SetActive(true);
                    }
                }   //2�p�^�[���̍U�����烉���_���ɒe�̃X�|�[���|�C���g���o���B
                yield return new WaitForSeconds(3.5f);
                if (_bulletSpawns.Length != 0)
                {
                    foreach (var i in _bulletSpawns)
                    {
                        i.SetActive(false);
                    }
                }   //�S�ẴX�|�[���|�C���g���A�N�e�B�u�ɂ���
                _overAttack.SetActive(false);
            }   // �I�[�o�[�U�����A�N�e�B�u�ɂ���

            //���O���̃{�X�̎��̏���
            else if (_bossState == BossState.MoguraBoss)
            {
                _overAttack.SetActive(true);
                //���O���̂̍U���t���O��true�ɂ���
                _moleAttackBool = true;
                //������̍U�����~���鎞�Ԃ����ҋ@
                yield return new WaitForSeconds(_attackTime);
                //������̍U���t���O��false�ɂ���
                _moleAttackBool = false;
                yield return new WaitForSeconds(0.1f);
                //�I�[�o�[�U�����A�N�e�B�u�ɂ���
                _overAttack.SetActive(false);
                yield return new WaitForSeconds(2f);
            }
        }

        _oneShot = false;
    }

    public void OverMode()
    {
        if (_movePoint.Length > 0)
        {
            transform.position = _movePoint[0].position;
        }   //�ŏ��̃|�C���g�Ɉʒu��ݒ�
        //�U���t���O��؂�ւ�
        _attackBool = !_attackBool;
        //�v���C���[�Ɍ������U����؂�ւ�
        _ownFire.SetActive(_attackBool);
        //�I�[�o�[�U�����A�N�e�B�u�ɂ���
        _overAttack.SetActive(false); 
    }

    enum BossState
    {
        /// <summary>�h���S��</summary>
        Drgon,
        /// <summary>���O��</summary>
        MoguraBoss,
    }
}
