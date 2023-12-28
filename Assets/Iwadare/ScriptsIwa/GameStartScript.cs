using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    SceneLoaderAditiveClass _sceneLoaderAditiveClass;
    [SerializeField] string _sceneName;
    [SerializeField] bool _startPlay = true;
    [SerializeField] AudioClip _bGMAudio;
    [SerializeField] string _stageNumber = "Stage";
    [SerializeField] string _stageName;
    [SerializeField] float _pageTime = 3f;
    Text _stageNameText;
    [Header("エンディング用設定。")]
    [SerializeField] bool _ending = false;
    [SerializeField] Canvas _endCanvas;
    [SerializeField] Image _backPanel;
    [SerializeField] RectTransform[] _endingText;
    [SerializeField]Color _initialColor = Color.black;

    Canvas _uI;
    void Awake()
    {
        if (!_ending)
        {
            _uI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<Canvas>();
            _stageNameText = GameObject.FindGameObjectWithTag("StageNameUI").GetComponent<Text>();
            GameManager.Instance.Sansyo();
            GameManager.Instance.GameSetting();
        }
        else
        {
            _endCanvas.enabled = false;
            _backPanel.color = _initialColor;
            foreach (var text in _endingText)
            {
                text.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= Dummy;
    }

    void Dummy(bool dummy) { }

    private void Start()
    {
        PauseManager.OnPauseResume += Dummy;
        PauseManager.PauseResume();
        if (_sceneName != "")
        {
            if (_uI != null) { _uI.enabled = false; }
            _sceneLoaderAditiveClass = new SceneLoaderAditiveClass();
            _sceneLoaderAditiveClass.SceneLoaderAditive(_sceneName);
        }

        if (_startPlay)
        {
            Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        BGMManager.Instance?.ClipBGMPlay(_bGMAudio);

        if (_ending)
        {
            StartCoroutine(EndingTime());
            return;
        }
        if (!_uI.enabled) { _uI.enabled = true; }
        StartCoroutine(StartTime());
    }

    /// <summary>ゲームスタート時の処理</summary>
    IEnumerator StartTime()
    {
        _stageNameText.text = "";
        if (_stageName != "")
        {
            yield return _stageNameText.DOText($"{_stageNumber}\n{_stageName}", _stageName.Length * 0.3f).
                WaitForCompletion();
            yield return new WaitForSeconds(2f);
            yield return DOTween.ToAlpha(() => _stageNameText.color, a => _stageNameText.color = a, 0.0f, 2f).
                WaitForCompletion();
        }
        else
        {
            yield return null;
        }
        PauseManager.PauseResume();
    }

    /// <summary>エンディング時の処理</summary>
    IEnumerator EndingTime()
    {
        _endCanvas.enabled = true;
        yield return new WaitForSeconds(2f);
        yield return _backPanel.DOFade(0.5f, 1f);
        _endingText[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(_pageTime);
        for(var i = 1;i < _endingText.Length;i++)
        {
            _endingText[i - 1].gameObject.SetActive(false);
            _endingText[i].gameObject.SetActive(true);
            for(var time = 0f;time < _pageTime;time += Time.deltaTime)
            {
                yield return null;
            }
        }
    }
}
