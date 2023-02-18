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
    // Start is called before the first frame update
    void Start()
    {
         
        foreach (var i in _pattern)
        {
            i.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_random == false)
        {
            _random = true;
            StartCoroutine(Instans());
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
}
