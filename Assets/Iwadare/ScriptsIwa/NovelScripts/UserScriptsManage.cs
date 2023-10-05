using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cinemachine;
using Unity.VisualScripting;

public class UserScriptsManage : MonoBehaviour
{
    [Tooltip("ノベルテキストのtxt"),Header("ノベルテキストのtxtを入れる")]
    [SerializeField]
    TextAsset _textFile;
    
    [Tooltip("名前を表示するText"),Header("名前を表示するTextを入れる")]
    [SerializeField] 
    Text _nameText;
    
    [Tooltip("現在の番号")]
    [System.NonSerialized]
    public int _novelNumber;
    
    [Tooltip("UserScriptManageのインスタンス")]
    [System.NonSerialized]
    public static UserScriptsManage instance;
    
    [Tooltip("ImageManagerのスクリプト"),Header("ImageManagerのづスクリプトのついたオブジェクトをアタッチ")]
    [SerializeField] 
    ImageManager _imageManager;
    
    [Tooltip("上書きするシーンの名前")]
    [SerializeField]
    string _sceneName;

    [Tooltip("CinemaChineコンポーネント"),Header("ChinemaChineコンポーネントのついたオブジェクト")]
    [SerializeField] 
    CinemachineVirtualCamera _shakeChinemachine;
    [Tooltip("CinemachineImpulseSourceコンポーネント"),Header("CinemachineImpulseSourceコンポーネントのついたオブジェクト")]
    [SerializeField] 
    CinemachineImpulseSource _impulseSource;

    [Tooltip("カメラのゆらす衝撃の大きさ")]
    [SerializeField] 
    float _impulseScale = 5;

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
        _shakeChinemachine.enabled = false;
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
            GameObject.FindObjectOfType<GameStartScript>().Play();
            SceneManager.UnloadSceneAsync(_sceneName);
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
            case "&CharaIn":

                break;
            case "&CharaOut":
                break;
            case "&ChangeChara":

                break;
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
            case "&LongShakeEvent":
                LongShake(true);
                _novelNumber++;
                break;
            case "&EndLongShakeEvent":
                LongShake(false);
                _novelNumber++;
                break;
            case "&ShortShakeEvent":
                ShortShake();
                _novelNumber++;
                break;
                

        }
        if(_novelNumber < _txtText.Count && _txtText[_novelNumber][0] == '&')
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

    void LongShake(bool shakebool)
    {
        _shakeChinemachine.enabled = shakebool;
    }

    void ShortShake()
    {
        _impulseSource.GenerateImpulseAt(new Vector2(0,0),new Vector2(0,_impulseScale));
    }
}
