using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMove : MonoBehaviour
{
    [Header("�ړ����x")]
    [SerializeField] float _speed = 3f;
    //��~����b��
    [SerializeField] float _stopsec = 10f;
    //�e�𔭎˂���ʒu�̃I�u�W�F�N�g
    [SerializeField] GameObject _bulletpoint;
    Rigidbody2D _rb;
    float _y;
    [Tooltip("��~����")]
    bool _stopBool;
    [SerializeField] bool _takatori;

    private void OnEnable()
    {
        //�G���A�N�e�B�u�ɂȂ����烉���_����Y���W�𐶐�
        _y = Random.Range(0f, 4f);
        _stopBool = false;
    }

    // Start is called before the first frame updat
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        //�G���������Ɉړ�������
        _rb.velocity = Vector2.down * _speed;
        _bulletpoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= _y && _stopBool == false)
        {
            _stopBool = true;
            StartCoroutine(StopTime());
        }//�G��Y���W����~�ʒu�ȉ��ɂȂ������~����
        if (this.transform.position.y <= -7f)
        {
            Destroy(gameObject);
        }//�G�̍��W��-7�ȉ��ɂȂ�����G��j��
    }
    //�ꎞ��~�̂��߂̃R���[�`��
    IEnumerator StopTime()
    {
        _bulletpoint.SetActive(true);
        //�G�̑��x��0�ɂ���
        _rb.velocity *= 0;
        //�ҋ@����
        yield return new WaitForSeconds(_stopsec);
        //�ҋ@���Ԃ��I�������G���Ăщ������Ɉړ�������
        _rb.velocity = Vector2.down * _speed;
        if(_takatori)
        {
            _bulletpoint.SetActive(false);
        }
    }

}
