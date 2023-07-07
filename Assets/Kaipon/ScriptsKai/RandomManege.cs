using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManege : MonoBehaviour
{
    bool _random;
    [SerializeField] float _insSec;
    [SerializeField] GameObject[] _pattern;
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
            }
        }
    }
    IEnumerator Instans()
    {
        yield return new WaitForSeconds(_insSec);
        _pattern[_current].SetActive(true);
        yield return new WaitForSeconds(_desSec);
        _pattern[_current].SetActive(false);
        _current = (_current + 1) % _pattern.Length;
        _random = false;
    }
    public void ResetEnemy()
    {
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
