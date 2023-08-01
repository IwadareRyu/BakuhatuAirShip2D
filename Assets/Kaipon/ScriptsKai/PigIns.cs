using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIns : MonoBehaviour
{
    //�G�̐����ʒu
    Vector2 _pos;
    float _x;
    //��������G�̃v���n�u
    [SerializeField] GameObject _pig;
    [Header("�G�̐����Ԋu")]
    [SerializeField] float _insSec;
    //�G��Y���W�̏���Ɖ���
    [SerializeField] float _high = 4f;
    [SerializeField] float _low = -4f;
    //���Ԍo�߃J�E���g
    float _time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //���Ԍo�߂̌v�Z
        _time += Time.deltaTime;
        if (_time > _insSec)
        {
            _x = Random.Range(_low, _high);
            //�G�̐����ʒu�̐ݒ�
            _pos = new Vector2(_x, this.transform.position.y);
            Instantiate(_pig, _pos, Quaternion.identity);
            //���Ԍo�߂̃J�E���g���Z�b�g
            _time = 0;
        }
    }
    public void InsSecChange(float num)
    {
        //�G�̐����Ԋu��V�����l�ɕύX
        _insSec = num;
    }

}
