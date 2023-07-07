using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] GameObject _backGeoundObject;
    [SerializeField] Sprite[] _backGeoundSprites;
    [SerializeField] Image[] _charaImage;
    [SerializeField] Color _standCharaColor;
    int _changeSpritenum = 0;
    Image _backGroundimage;
    Image _tmpImage;

    // Start is called before the first frame update
    void Awake()
    {
        _backGroundimage = _backGeoundObject.GetComponent<Image>();
        foreach(var i in _charaImage)
        {
            i.color = _standCharaColor;
        }
    }

    private void Start()
    {
                _backGroundimage.sprite = _backGeoundSprites[_changeSpritenum];
    }

    public void ChangeImage()
    {
        _changeSpritenum++;
        _backGroundimage.sprite = _backGeoundSprites[_changeSpritenum];
    }

    public void CharaImage(int i)
    {
        if (i != -1)
        {
            if (_tmpImage)
            {
                _tmpImage.color = _standCharaColor;
            }
            _tmpImage = _charaImage[i];
            _tmpImage.color = Color.white;
        }
        else
        {
            if (_tmpImage)
            {
                _tmpImage.color = _standCharaColor;
                _tmpImage = null;
            }
        }
        
    }
}
