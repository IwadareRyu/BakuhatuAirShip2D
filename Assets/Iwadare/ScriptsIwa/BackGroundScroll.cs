using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField] SpriteRenderer _backGround;
    [SerializeField] float _scrollSpeed = -2f;
    SpriteRenderer _backGroundClone;
    float _startPositionY;
    // Start is called before the first frame update
    void Start()
    {
        _startPositionY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);
    }   //�w�i��O�ɃN���[������B

    // Update is called once per frame
    void Update()
    {
        _backGround.transform.Translate(0f, Time.deltaTime * _scrollSpeed, 0f);
        _backGroundClone.transform.Translate(0f, Time.deltaTime * _scrollSpeed, 0f);

        BackGroundReset(_backGround,_backGroundClone);
        BackGroundReset(_backGroundClone,_backGround);
    }   //�w�i�𓮂����A���̈ʒu�܂œ�������A���̔w�i����Ɏ����Ă��鏈��������B

    /// <summary>�w�i�����Z�b�g���鏈��</summary>
    /// <param name="m">���̔w�i</param>
    /// <param name="n">��̔w�i</param>
    void BackGroundReset(SpriteRenderer m,SpriteRenderer n)
    {
        if(m.transform.position.y < _startPositionY - n.bounds.size.y)
        {
            m.transform.Translate(0f,m.bounds.size.y * 2,0f);
        }   //���̔w�i����̔w�i�̏�ƌq����B
    }
}
