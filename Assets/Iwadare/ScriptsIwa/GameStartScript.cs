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
    bool _dummyPause;
    [SerializeField] string _stageNumber = "Stage";
    [SerializeField] string _stageName;
    Text _stageNameText;

    Canvas _uI;
    void Awake()
    {
        _uI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<Canvas>();
        _stageNameText = GameObject.FindGameObjectWithTag("StageNameUI").GetComponent<Text>();
        GameManager.Instance.Sansyo();
        GameManager.Instance.GameSetting();
    }

    private void Start()
    {
        PauseManager.PauseResume();
        if (_sceneName != "")
        {
            _uI.enabled = false;
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
        BGMManager.Instance.BGMPlay(_bGMAudio);


        if (!_uI.enabled) { _uI.enabled = true; }
        StartCoroutine(StartTime());
    }

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
}
