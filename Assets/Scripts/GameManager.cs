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
    [Tooltip("現在のスコアの値")]
    [SerializeField]int _score = 1000;
    [Tooltip("スコアの限界値")]
    int _maxScore = 999999;
    [SerializeField] float _countDownTime = 60f;
    bool _isStarted;
    [Tooltip("デバック用")]
    [SerializeField] bool _godmode;
    [SerializeField] float _gaugeInterval = 1f;
    [Tooltip("GameOver時")]
    bool _isgameOver;
    public bool _gameover => _isgameOver;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoadにGameManagerがあれば自身を破壊、なければ自身をDontDestroyOnLoadに移動してスコアに初期値を代入。
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
        //スコアの初期化
        _scoreText.text = _score.ToString("D6");
        AddScore(0);
    }
    /// <summary>リザルトシーンの際、scoreを表示する。</summary>
    public void ShowScore()
    {
        GameObject go = GameObject.Find(_resultText);
        Text text = go?.GetComponent<Text>();

        if (text)
        {
            text.text = _reScore;
        }

    }
    /// <summary>スコアの追加</summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {

        if (!_godmode)
        {
            //スコアを加算する前のスコアを代入する。
            int tempScore = _score;
            //2つの値の小さいほうが_scoreに代入される。
            _score = Mathf.Min(_score + score,_maxScore);
            //スコアを加算する前のスコアから、後のスコアまでの値を小さい値を代入しながら後のスコアの値になるまで代入する。
            DOTween.To(() => tempScore, x => { _scoreText.text = string.Format("{0:D7}",x.ToString("0000000;-000000;")); },_score, _gaugeInterval).OnComplete(() => _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;")));
            _scoreText.text = string.Format("{0:D7}",_score.ToString("0000000;-000000;"));
        }

    }
    //リザルトシーンへスコアを代入する。
    public void SetName(Text input)
    {
        _reScore = input.text;
    }
    // Update is called once per frame
    void Update()
    {
        //カウントダウン
        _timeText.text = String.Format("{0:00.00}", _countDownTime);
        _countDownTime = Mathf.Max(_countDownTime - Time.deltaTime,0f);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (_isStarted) ShowScore();
        _score = 1000;
    }
    /// <summary>無敵時間</summary>
    /// <returns></returns>
}
