using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManege : MonoBehaviour
{
    bool _random;
    //生成間隔
    [SerializeField] float _insSec;
    //オブジェクトの配列
    [SerializeField] GameObject[] _pattern;
    //消滅するまでの時間
    [SerializeField] float _desSec;
    int _current = 0;
    bool _pause;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var i in _pattern)
        {
            i.SetActive(false);
        }
    }
    private void OnEnable()
    {
        PauseManager.OnPauseResume += OnStartPause;
    }   // ポーズ

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // ポーズ解除

    // Update is called once per frame
    void Update()
    {
        if (!_pause)
        {
            if (_random == false)
            {
                _random = true;
                StartCoroutine(Instans());
            }//ポーズしていないときに生成を開始
        }
    }
    IEnumerator Instans()
    {
        //指定した生成間隔で待機
        yield return new WaitForSeconds(_insSec);
        //今のパターンをアクティブ
        _pattern[_current].SetActive(true);
        //指定した消滅までの待機時間
        yield return new WaitForSeconds(_desSec);
        //今のパターンを非アクティブ
        _pattern[_current].SetActive(false);
        //次のパターンのインデックス更新
        _current = (_current + 1) % _pattern.Length;
        _random = false;
    }
    public void ResetEnemy()
    {
        //全てのパターンを非アクティブにし、このオブジェクトも非アクティブにする
        foreach (var i in _pattern)
        {
            i.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public virtual void OnStartPause(bool pause)
    {
        _pause = pause;
    }
}
