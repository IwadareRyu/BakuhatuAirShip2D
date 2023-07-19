using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] 
    GameObject _backGroundObject;
    
    [SerializeField] 
    Sprite[] _backGroundSprites;
    
    [SerializeField] 
    Image[] _charaImage;

    [SerializeField]
    Sprite[] _charaSprite;
    
    [SerializeField] 
    Color _standCharaColor;
    int _changeSpriteNum = 0;
    
    Image _backGroundImage;
    
    Image _tmpImage;

    // Start is called before the first frame update
    void Awake()
    {
        _backGroundImage = _backGroundObject.GetComponent<Image>();
        foreach(var i in _charaImage)
        {
            i.color = _standCharaColor;
        }
    }

    private void Start()
    {
        _backGroundImage.sprite = _backGroundSprites[_changeSpriteNum];
    }

    public void ChangeImage()
    {
        _changeSpriteNum++;
        _backGroundImage.sprite = _backGroundSprites[_changeSpriteNum];
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
            if(_tmpImage.sprite == null)
            {
                _tmpImage.sprite = _charaSprite[i];
            }
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
