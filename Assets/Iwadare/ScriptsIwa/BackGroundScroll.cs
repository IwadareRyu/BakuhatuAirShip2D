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
    }   //背景を前にクローンする。

    // Update is called once per frame
    void Update()
    {
        _backGround.transform.Translate(0f, Time.deltaTime * _scrollSpeed, 0f);
        _backGroundClone.transform.Translate(0f, Time.deltaTime * _scrollSpeed, 0f);

        BackGroundReset(_backGround,_backGroundClone);
        BackGroundReset(_backGroundClone,_backGround);
    }   //背景を動かし、一定の位置まで動いたら、下の背景を上に持ってくる処理をする。

    /// <summary>背景をリセットする処理</summary>
    /// <param name="m">下の背景</param>
    /// <param name="n">上の背景</param>
    void BackGroundReset(SpriteRenderer m,SpriteRenderer n)
    {
        if(m.transform.position.y < _startPositionY - n.bounds.size.y)
        {
            m.transform.Translate(0f,m.bounds.size.y * 2,0f);
        }   //下の背景を上の背景の上と繋げる。
    }
}
