using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

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
    
    [Tooltip("ImageManagerのスクリプト"),Header("ImageManagerのスクリプトがついたオブジェクトをアタッチ")]
    [SerializeField]
    ImageManager _imageManager;
    
    [Tooltip("上書きするシーンの名前")]
    [SerializeField]
    string _sceneName;

    [Tooltip("CinemaChineコンポーネント"),Header("ChinemaChineコンポーネントのついたオブジェクト")]
    [SerializeField] 
    CinemachineVirtualCamera _shakeChinemachine;

    [Tooltip("CinemachineImpulseSourceコンポーネント"),Header("CinemachineImpulseSourceコンポーネントのついたオブジェクト")]
    CinemachineImpulseSource _impulseSource;

    [Tooltip("カメラのゆらす衝撃の大きさ")]
    [SerializeField] 
    float _impulseScale = 5;

    [SerializeField]
    bool _isPlay = true;

    [SerializeField]
    bool _isEnd = false;

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

        _impulseSource = GetComponent<CinemachineImpulseSource>();
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
        else if(_isPlay)
        {
            GameObject.FindObjectOfType<GameStartScript>().Play();
            SceneManager.UnloadSceneAsync(_sceneName);
        }
        else
        {
            GameObject.FindObjectOfType<Goal>().IsEndMethod();
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
                _novelNumber++;
                if (_txtText[_novelNumber] == "&Blue")
                {
                    CharaChange(0,true);
                }
                else
                {
                    CharaChange(1,true);
                }
                break;
            case "&CharaOut":
                _novelNumber++;
                if (_txtText[_novelNumber] == "&Blue")
                {
                    CharaChange(0,charaOut:true);
                }
                else
                {
                    CharaChange(1,charaOut:true);
                }
                break;
            case "&ChangeChara":
                _novelNumber++;
                if (_txtText[_novelNumber] == "&Blue")
                {
                    CharaChange(0, true,true);
                }
                else
                {
                    CharaChange(1, true,true);
                }
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
            case "&BGMPlay":
                NobelBGMPlay();
                break;
            case "&BGMStop":
                BGMManager.Instance.BGMStop();
                break;
            case "SEPlay":
                NobelSEPlay();
                break;
            case "":
                _novelNumber++;
                break;

        }
        if(_novelNumber < _txtText.Count && _txtText[_novelNumber][0] == '&')
        {
            PlayStatement(_txtText[_novelNumber]);
        }
    }

    /// <summary>キャラのイラストのみを変えるメソッド</summary>
    /// <param name="i"></param>
    public void CharaChange(int i = -1,bool charaIn = false, bool charaOut = false)
    {
        ImageState image = ImageState.BlueNormal;
        if (i != -1 || _txtText[_novelNumber][0] != '&')
        {
            _novelNumber++;
            image = Enum.Parse<ImageState>(_txtText[_novelNumber]);
        }

        if(charaOut)
        {
            _imageManager.CharaOut(i);
        }

        _imageManager.CharaImage(i,image);
        
        if(charaIn) 
        {
            _imageManager.CharaIn(i);
        }
        
        _novelNumber++;
        
        if (_txtText[_novelNumber] == "" || _txtText[_novelNumber][0] != '&')
        {
            _nameText.text = _txtText[_novelNumber];
            _novelNumber++;
        }
    }

    void LongShake(bool shakebool)
    {
        _shakeChinemachine.enabled = shakebool;
    }

    void ShortShake()
    {
        _impulseSource.GenerateImpulseAt(new Vector2(0,0),new Vector2(0,_impulseScale));
    }

    void NobelBGMPlay()
    {
        _novelNumber++;
        var state = Enum.Parse<NobelBGM>(_txtText[_novelNumber]);
        if (state != null)
        {
            BGMManager.Instance.StateBGMPlay(state);
            _novelNumber++;
        }
    }

    void NobelSEPlay()
    {
        _novelNumber++;
        var state = Enum.Parse<SE>(_txtText[_novelNumber]);
        if(state != null)
        {
            BGMManager.Instance.SEPlay(state);
            _novelNumber++;
        }
    }
}
