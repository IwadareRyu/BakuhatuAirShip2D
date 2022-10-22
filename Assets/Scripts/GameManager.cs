using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : SingletonMonovihair<GameManager>
{
    private Text _timeText;
    private Text _scoreText;
    private Text _totalMoneyText;
    [SerializeField] GameObject _resultText;
    [SerializeField] GameObject _gameOverCanvas;
    string _reScore;
    [Tooltip("���݂̃X�R�A�̒l")]
    public int _score = 1000;
    public int _totalMoney = 0;
    [Tooltip("�X�R�A�̌��E�l")]
    int _maxScore = 9999999;
    [SerializeField] float _countDownTime = 60f;
    bool _isStarted;
    [Tooltip("�f�o�b�N�p")]
    [SerializeField] bool _godmode;
    [SerializeField] float _gaugeInterval = 1f;
    [Tooltip("GameOver��")]
    bool _isgameOver;
    public bool _gameover => _isgameOver;

    protected override bool _dontDestroyOnLoad { get { return true; } }

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        _timeText = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        _totalMoneyText = GameObject.FindGameObjectWithTag("TotalMoney").GetComponent<Text>();
        //�X�R�A�̏�����
        ShowScore();
        _isStarted = true;
        _scoreText.text = _score.ToString("0000000");
        AddScore(0);
        _totalMoneyText.text = _totalMoney.ToString("0000000");
    }
    /// <summary>������ʂ̍ہA���v���z��\������B</summary>
    public void ShowScore()
    {
        Text text = _resultText?.GetComponent<Text>();

        if (text)
        {
            text.text = _totalMoney.ToString("0000000");
        }

    }
    /// <summary>�X�R�A�̒ǉ�</summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {

        if (!_godmode)
        {
            //�X�R�A�����Z����O�̃X�R�A��������B
            float tempScore = _score;
            //2�̒l�̏������ق���_score�ɑ�������B
            _score = Mathf.Min(_score + score,_maxScore);
            //�X�R�A�����Z����O�̃X�R�A����A��̃X�R�A�܂ł̒l���������l�������Ȃ����̃X�R�A�̒l�ɂȂ�܂ő������B
            DOTween.To(() => tempScore, x => { _scoreText.text = string.Format("{0:D7}",x.ToString("0000000;-000000;")); },_score, _gaugeInterval).OnComplete(() => _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;")));
            _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;"));
        }

    }

    /// <summary>���U���g��_score�̈�����Totalmoney�֐��Ăяo���p</summary>
    public void Resultscore()
    {
        TotalMoney(_score);
    }
    /// <summary>���v���z�𑝌�������֐�</summary>
    /// <param name="score"></param>
    public void TotalMoney(int score)
    {
        _totalMoney = Mathf.Min(_totalMoney + score, _maxScore);
        ShowScore();
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
        //if (_isStarted) ShowScore();
        _score = 1000;
        Start();
        PowerUp.Instance.Start();
    }
}
