using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
public class MainTextScripts : MonoBehaviour
{
    Text _novelText;
    string _text;
    bool _onebool;
    // Start is called before the first frame update
    void Start()
    {
        _novelText = GetComponent<Text>();
        DisPlayText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToTheNextLine()
    {
        if (_onebool)
        {
            UserScriptsManage.instance._novelNumber++;
        }
        else
        {
            _onebool = true;
        }
        UserScriptsManage.instance.IsStatement();
    }

    public void DisPlayText()
    {
        if (DOTween.IsTweening(_novelText))
        {
            _novelText.text = _text;
            _novelText.DOKill();
        }
        else
        {
            GoToTheNextLine();
            _text = UserScriptsManage.instance.GetText();
            _novelText.text = "";
            _novelText.DOText(_text, _text.Length * 0.1f);
            Debug.Log("çXêV");
        }
    }


}
