using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _timeText;
    [SerializeField] Text _scoreText;
    [SerializeField] string _resultText = "ResultScore";
    [SerializeField] GameObject _gameOverCanvas;
    string _reScore;
    [Tooltip("���݂̃X�R�A�̒l")]
    [SerializeField]int _score = 1000;
    [Tooltip("�X�R�A�̌��E�l")]
    int _maxScore = 999999;
    [SerializeField] float _countDownTime = 60f;
    bool _isStarted;
    [Tooltip("�f�o�b�N�p")]
    [SerializeField] bool _godmode;
    [SerializeField] float _gaugeInterval = 1f;
    [Tooltip("GameOver��")]
    bool _isgameOver;
    public bool _gameover => _isgameOver;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad��GameManager������Ύ��g��j��A�Ȃ���Ύ��g��DontDestroyOnLoad�Ɉړ����ăX�R�A�ɏ����l�����B
        if(FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            ShowScore();
            _isStarted = true;
        }
        //�X�R�A�̏�����
        _scoreText.text = _score.ToString("D6");
        AddScore(0);
    }
    /// <summary>���U���g�V�[���̍ہAscore��\������B</summary>
    public void ShowScore()
    {
        GameObject go = GameObject.Find(_resultText);
        Text text = go?.GetComponent<Text>();

        if (text)
        {
            text.text = _reScore;
        }

    }
    /// <summary>�X�R�A�̒ǉ�</summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {

        if (!_godmode)
        {
            //�X�R�A�����Z����O�̃X�R�A��������B
            int tempScore = _score;
            //2�̒l�̏������ق���_score�ɑ�������B
            _score = Mathf.Min(_score + score,_maxScore);
            //�X�R�A�����Z����O�̃X�R�A����A��̃X�R�A�܂ł̒l���������l�������Ȃ����̃X�R�A�̒l�ɂȂ�܂ő������B
            DOTween.To(() => tempScore, x => { _scoreText.text = string.Format("{0:D7}",x.ToString("0000000;-000000;")); },_score, _gaugeInterval).OnComplete(() => _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;")));
            _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;"));
        }

    }
    //���U���g�V�[���փX�R�A��������B
    public void SetName(Text input)
    {
        _reScore = input.text;
    }
    // Update is called once per frame
    void Update()
    {
        //�J�E���g�_�E��
        _timeText.text = String.Format("{0:00.00}", _countDownTime);
        _countDownTime = Mathf.Max(_countDownTime - Time.deltaTime,0f);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (_isStarted) ShowScore();
        _score = 1000;
    }
    /// <summary>���G����</summary>
    /// <returns></returns>
}
