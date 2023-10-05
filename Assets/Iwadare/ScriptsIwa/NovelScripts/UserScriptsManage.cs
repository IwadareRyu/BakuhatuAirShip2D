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
    
    [Tooltip("ImageManager�̃X�N���v�g"),Header("ImageManager�̂ÃX�N���v�g�̂����I�u�W�F�N�g���A�^�b�`")]
    [SerializeField] 
    ImageManager _imageManager;
    
    [Tooltip("�㏑������V�[���̖��O")]
    [SerializeField]
    string _sceneName;

    [Tooltip("CinemaChine�R���|�[�l���g"),Header("ChinemaChine�R���|�[�l���g�̂����I�u�W�F�N�g")]
    [SerializeField] 
    CinemachineVirtualCamera _shakeChinemachine;
    [Tooltip("CinemachineImpulseSource�R���|�[�l���g"),Header("CinemachineImpulseSource�R���|�[�l���g�̂����I�u�W�F�N�g")]
    [SerializeField] 
    CinemachineImpulseSource _impulseSource;

    [Tooltip("�J�����̂�炷�Ռ��̑傫��")]
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
