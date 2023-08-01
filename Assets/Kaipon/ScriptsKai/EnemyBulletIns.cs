using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletIns : MonoBehaviour
{
    bool _insbool;
    [Header("�e�̐����Ԋu")]
    [SerializeField] float _inssec = 3f;
    BulletPoolActive _pool;//�v�[���ł̒e�̊Ǘ��N���X
    [Header("�e�̔��ˊp�x")]
    [SerializeField] float _angle = 270f;
    [Header("�e�̃X�s�[�h")]
    [SerializeField] float _speed = 3f;
    [Header("�e�̎��")]
    [SerializeField] BulletTypeClass.BulletSpriteState _type;
    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g���������ꂽ�玞�ɓG�̒e�̃v�[�����擾
        _pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<BulletPoolActive>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_insbool)
        {
            _insbool = true;
            StartCoroutine(InsTime());
        }//�R���[�`�����J�n����
    }
    //�e�̐����Ԋu�Œe�𐶐�����R���[�`��
    IEnumerator InsTime()
    {
        //�e�̐����Ԋu�̑ҋ@����
        yield return new WaitForSeconds(_inssec);
        // �v�[�����痘�p�o����e���擾
        var bullet = _pool.GetBullet();
        var bulletcs = bullet.GetComponent<ActiveBullet>();
        // �e�𐶐�����ʒu�ݒ�
        bullet.transform.position = transform.position;
        bulletcs.BulletAdd(_angle,_speed,_type);
    }
}
