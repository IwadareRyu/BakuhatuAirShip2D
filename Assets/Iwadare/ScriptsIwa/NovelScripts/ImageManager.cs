using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageManager : MonoBehaviour
{
    [SerializeField] 
    GameObject _backGroundObject;

    [SerializeField]
    Image _fadeBackGroundObject;

    [SerializeField]
    Color _fadeColor;
    
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


    [SerializeField]
    Transform[] _charainPos;

    [SerializeField]
    Transform[] _charaOutPos;


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
        _fadeBackGroundObject.color = _fadeColor;
    }

    public void ChangeImage()
    {
        _changeSpriteNum++;
        _fadeBackGroundObject.sprite = _backGroundSprites[_changeSpriteNum];
        _fadeBackGroundObject.DOFade(1f,1f).OnComplete(() =>
        {

            _fadeBackGroundObject.color = _fadeColor;
            _backGroundImage.sprite = _backGroundSprites[_changeSpriteNum];
        });
      
    }

    public void CharaImage(int i,ImageState image)
    {
        if (i != -1)
        {
            if (_tmpImage)
            {
                _tmpImage.color = _standCharaColor;
            }
            _tmpImage = _charaImage[i];
            _tmpImage.sprite = _charaSprite[(int)image];
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

    public void CharaOut(int i)
    {
        _charaImage[i].transform.position = _charaOutPos[i].position;
    }

    public void CharaIn(int i)
    {
        _charaImage[i].transform.DOMove(_charainPos[i].position,0.5f);
    }
}
