using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextScript : MonoBehaviour
{
    [SerializeField] Text _storyText;
    string _loadText;
    string[] _splitText;
    [SerializeField]int data = 10;
    bool _textskip;
    // Start is called before the first frame update
    void Start()
    {
        //_splitText = _loadText.Split('\n');
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Story(TextAsset _txt)
    {
        _loadText = _txt.text;
        _storyText.DOText(_loadText, data).SetEase(Ease.Linear);
    }
    public void TextSkip()
    {
        if(DOTween.IsTweening(_storyText))
        {
            _storyText.DOComplete();
        }
    }
    public void TextReload()
    {
        if (DOTween.IsTweening(_storyText))
        {
            _storyText.DOComplete();
        }
        _storyText.text = "";
    }
}
