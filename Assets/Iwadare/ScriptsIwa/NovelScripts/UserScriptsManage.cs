using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class UserScriptsManage : MonoBehaviour
{
    [Tooltip("�m�x���e�L�X�g��txt"),Header("�m�x���e�L�X�g��txt������")]
    [SerializeField]
    TextAsset _textFile;
    
    [Tooltip("���O��\������Text"),Header("���O��\������Text������")]
    [SerializeField] 
    Text _nameText;
    
    [Tooltip("���݂̔ԍ�")]
    [System.NonSerialized]
    public int _novelNumber;
    
    [Tooltip("UserScriptManage�̃C���X�^���X")]
    [System.NonSerialized]
    public static UserScriptsManage instance;
    
    [Tooltip("ImageManager�̃X�N���v�g"),Header("ImageManager�̃X�N���v�g�������I�u�W�F�N�g���A�^�b�`")]
    [SerializeField]
    ImageManager _imageManager;
    
    [Tooltip("�㏑������V�[���̖��O")]
    [SerializeField]
    string _sceneName;

    [Tooltip("CinemaChine�R���|�[�l���g"),Header("ChinemaChine�R���|�[�l���g�̂����I�u�W�F�N�g")]
    [SerializeField] 
    CinemachineVirtualCamera _shakeChinemachine;

    [Tooltip("CinemachineImpulseSource�R���|�[�l���g"),Header("CinemachineImpulseSource�R���|�[�l���g�̂����I�u�W�F�N�g")]
    CinemachineImpulseSource _impulseSource;

    [Tooltip("�J�����̂�炷�Ռ��̑傫��")]
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

    /// <summary>�L�����̃C���X�g�݂̂�ς��郁�\�b�h</summary>
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
