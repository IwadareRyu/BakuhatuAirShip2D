using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonovihair<GameManager>
{
    private Text _timeText;
    private Text _scoreText;
    private Text _totalMoneyText;
    private Text _deadEnemyText;
    [SerializeField] GameObject _resultText;
    [SerializeField] GameObject _gameOverCanvas;
    string _reScore;
    [Tooltip("現在のスコアの値")]
    [SerializeField] int _startScore = 4000;
    [SerializeField] int _score = 4000;
    public int _totalMoney = 0;
    [Tooltip("スコアの限界値")]
    int _maxScore = 9999999;
    [SerializeField] float _countDownTime = 60f;
    bool _isStarted;
    [Tooltip("倒す敵の数")]
    int _deadEnemy;
    public int _enemy => _deadEnemy;
    [Tooltip("デバック用")]
    [SerializeField] bool _godmode;
    [SerializeField] float _gaugeInterval = 1f;
    [Tooltip("GameOver時")]
    bool _isgameOver;
    bool _loseBool;
    public bool Gameover => _isgameOver;

    protected override bool _dontDestroyOnLoad { get { return true; } }

    private void OnEnable()
    {
        PauseManager.OnPauseResume += OnStartPause;
    }   // ポーズ

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // ポーズ解除

    void Start()
    {
        //Sansyo();
        //GameSetting();
    }

    public void Sansyo()
    {
        _scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        _timeText = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        _totalMoneyText = GameObject.FindGameObjectWithTag("TotalMoney").GetComponent<Text>();
        _deadEnemyText = GameObject.FindGameObjectWithTag("EnemyText").GetComponent<Text>();
    }

    public void GameSetting()
    {
        //スコアの初期化
        _score = _startScore;
        ShowScore();
        _gameOverCanvas.SetActive(false);
        _scoreText.text = _score.ToString("0000000");
        AddScore(0);
        _isStarted = true;
        _totalMoneyText.text = _totalMoney.ToString("0000000");
        _timeText.text = String.Format("{0:00.00}", _countDownTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (_isStarted)
        {
            if (_timeText)
            {
                //カウントダウン
                _timeText.text = String.Format("{0:00.00}", _countDownTime);
                _countDownTime = Mathf.Max(_countDownTime - Time.deltaTime, 0f);
            }
            if (!_loseBool && _countDownTime == 0 || !_loseBool && _score < 0)
            {
                _loseBool = true;
                BGMManager.Instance?.SEPlay(BGMManager.SE.Lose);
                _gameOverCanvas.SetActive(true);
                PauseManager.PauseResume();
            }
        }
    }

    /// <summary>強化画面の際、合計金額を表示する。</summary>
    public void ShowScore()
    {
        Text text = _resultText?.GetComponent<Text>();

        if (text)
        {
            text.text = _totalMoney.ToString("0000000");
        }

    }
    /// <summary>スコアの追加</summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {

        if (!_godmode)
        {
            //スコアを加算する前のスコアを代入する。
            float tempScore = _score;
            //2つの値の小さいほうが_scoreに代入される。
            _score = Mathf.Min(_score + score,_maxScore);
            //スコアを加算する前のスコアから、後のスコアまでの値を小さい値を代入しながら後のスコアの値になるまで代入する。
            DOTween.To(() => tempScore, x => { _scoreText.text = string.Format("{0:D7}",x.ToString("0000000;-000000;")); },_score, _gaugeInterval)
                .OnComplete(() => _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;")));
            _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;"));
        }

    }

    /// <summary>リザルトで_scoreの引数のTotalmoney関数呼び出し用</summary>
    public void Resultscore()
    {
        TotalMoney(_score);
    }
    /// <summary>合計金額を増減させる関数</summary>
    /// <param name="score"></param>
    public void TotalMoney(int score)
    {
        _totalMoney = Mathf.Min(_totalMoney + score, _maxScore);
        ShowScore();
    }

    //リザルトシーンへスコアを代入する。
    public void SetName(Text input)
    {
        _reScore = input.text;
    }

    public void SetCountDown(float time)
    {
        _countDownTime = time;
    }

    public void SetDeadEnemy(int enemy)
    {
        _deadEnemy = Mathf.Max(0,_deadEnemy + enemy);
        _deadEnemyText.text = _deadEnemy.ToString("00000");
    }

    public void ResetScore()
    {
        _totalMoney = 0;
        PowerUp.Instance.PowerReset();
        _isStarted = false;
    }

    public void StartScoreTime()
    {
        _score = _startScore;
        _countDownTime = 100;
        _loseBool = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        //if (_isStarted) ShowScore();
        if (PowerUp.Instance) PowerUp.Instance.Start();
    }

    public virtual void OnStartPause(bool pause)
    {
        _isStarted = !pause;
    }
}
