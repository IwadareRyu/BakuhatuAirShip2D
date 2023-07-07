using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UserScriptsManage : MonoBehaviour
{
    [SerializeField] TextAsset _textFile;
    [SerializeField] Text _nameText;
    [System.NonSerialized] public int _novelNumber;
    public static UserScriptsManage instance;
    [SerializeField] ImageManager _imageManager;
    [SerializeField] string _sceneName;

    List<string> _txtText = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        StringReader reader = new StringReader(_textFile.text);
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _txtText.Add(line);
        }
        if (!instance) instance = this;
        _novelNumber = 0;
    }

    // Update is called once per frame
    public string GetText()
    {
        if (_novelNumber < _txtText.Count)
        {

            string[] text = new string[3];
            for (var i = 0; _txtText[_novelNumber] != "&Stop"; i++)
            {
                text[i] = _txtText[_novelNumber] + "\n";
                _novelNumber++;
            }
            return text[0] + text[1] + text[2];
        }
        else
        {
            PauseManager.PauseResume();
            SceneManager.UnloadScene(_sceneName);
        }
        return "";
    }

    public void IsStatement()
    {
        if (_novelNumber < _txtText.Count)
        {
            if (_txtText[_novelNumber][0] == '&')
            {
                PlayStatement(_txtText[_novelNumber]);
            }
        }
    }

    public void PlayStatement(string sentence)
    {
        switch(sentence)
        {
            case "&Chara":
                CharaChange(-1);
                break;
            case "&Stop":
                _novelNumber++;
                break;
            case "&Blue":
                CharaChange(0);
                break;
            case "&Gen":
                CharaChange(1);
                break;
            case "&Image":
                _imageManager.ChangeImage();
                _novelNumber++;
                break;

        }
        if(_txtText[_novelNumber][0] == '&')
        {
            PlayStatement(_txtText[_novelNumber]);
        }
    }

    public void CharaChange(int i)
    {
        _imageManager.CharaImage(i);
        _novelNumber++;
        _nameText.text = _txtText[_novelNumber];
        _novelNumber++;
    }
}
