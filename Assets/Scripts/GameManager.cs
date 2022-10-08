using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Tooltip("�v���C���[��SpriteRenderer")]
    private SpriteRenderer pl;
    [SerializeField] Text _scoreText;
    [Tooltip("���C�t�̌��E�l")]
    [SerializeField] float _maxLife = 70f;
    [SerializeField] string _resultText = "ResultScore";
    [SerializeField] GameObject _gameOverCanvas;
    string _reScore;
    [Tooltip("���݂̃X�R�A�̒l")]
    [SerializeField]int _score = 0;
    [Tooltip("�X�R�A�̌��E�l")]
    int _maxScore = 999999;
    [Tooltip("���݂̃��C�t�l")]
    float _life;
    bool _isStarted;
    [Tooltip("���G���[�h(�f�o�b�N�p�ł�����)")]
    [SerializeField] bool _godmode;
    [SerializeField] Slider _lifeGauge;
    [SerializeField] float _gaugeInterval = 1f;
    [Tooltip("GameOver��")]
    bool _isgameOver;
    public bool _gameover => _isgameOver;
    int _gameoverScore = -99999;
    [Tooltip("���G����")]
    bool _star;
    public bool star => _star;
    // Start is called before the first frame update
    void Start()
    {
        //GetConponent
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        _lifeGauge = _lifeGauge.GetComponent<Slider>();
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
        AddScore(1000);
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
            DOTween.To(() => tempScore, x => { _scoreText.text = string.Format("{0:D6}",x.ToString("000000;-00000;")); },_score, _gaugeInterval).OnComplete(() => _scoreText.text = string.Format("{0:D6}",_score.ToString("000000;-00000;")));
            _scoreText.text = string.Format("{0:D6}",_score.ToString("000000;-00000;"));
        }

    }
    //���U���g�V�[���փX�R�A��������B
    public void SetName(Text input)
    {
        _reScore = input.text;
    }
    /// <summary>���G���[�h���̓���</summary>
    public void Continue()
    {
        pl.color = Color.yellow;
        AddScore(_gameoverScore);
        _life = 100;
        _gameOverCanvas.SetActive(false);
        _isgameOver = false; 
        _godmode = true;
        _star = true;
    }
    // Update is called once per frame
    void Update()
    {

        //���C�t��0�ɂȂ�����Q�[���I�[�o�[�ɂȂ�B
        if (_life <= 0)
        {
            _isgameOver = true;
            _gameOverCanvas.SetActive(true);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (_isStarted) ShowScore();
    }
    /// <summary>���G����</summary>
    /// <returns></returns>
    public IEnumerator StarTime()
    {
        //_star��true�ɂ��Ĉ�莞�Ԗ��G���Ԃ𔭐�������B
        SpriteRenderer pl = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        _star = true;
        Color _color;
        ColorUtility.TryParseHtmlString("#8CA2FF", out _color);
        pl.color = _color;
        yield return new WaitForSeconds(1f);
        _star = false;
        pl.color = Color.white;
    }
}
