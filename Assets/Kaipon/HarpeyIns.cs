using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpeyIns : MonoBehaviour
{
    bool _insbool; 
    [Header("モンスターのスポーン場所")]
    [SerializeField] GameObject[] _harpeyDown, _harpeyLeft, _harpeyRight ,_harpey;
    [SerializeField] float _coolsec;
    [SerializeField] float _inssec;
    [SerializeField] Spawn_Pattern _pattern;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        _insbool = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(_insbool == false)
        {
            _insbool = true;
            if(_pattern == Spawn_Pattern.Legion)
            {
                StartCoroutine(HarpeyLegionInstans());
            }
            else if(_pattern == Spawn_Pattern.RandomIns)
            {
                StartCoroutine(RandomInstance());
            }
        }
    }

    IEnumerator HarpeyLegionInstans()
    {
        yield return new WaitForSeconds(_inssec);
        var ram = (int)Random.Range(0,1.9f);
        Instantiate(_harpey[0], _harpeyDown[ram].transform.position,Quaternion.identity);
        yield return new WaitForSeconds(_inssec);
        ram = (int)Random.Range(0, 2.9f);
        Instantiate(_harpey[1], _harpeyLeft[ram].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_inssec);
        ram = (int)Random.Range(0, 2.9f);
        Instantiate(_harpey[2], _harpeyRight[ram].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_coolsec);
        _insbool = false;
    }

    IEnumerator RandomInstance()
    {
        yield return new WaitForSeconds(_inssec);
        var ram = (int)Random.Range(0, 2.9f);
        if (ram == 0)
        {
            ram = (int)Random.Range(0, 1.9f);
            Instantiate(_harpey[0], _harpeyDown[ram].transform.position, Quaternion.identity);
        }
        else if (ram == 1)
        {
            ram = (int)Random.Range(0, 2.9f);
            Instantiate(_harpey[1], _harpeyLeft[ram].transform.position, Quaternion.identity);
        }
        else 
        {
            ram = (int)Random.Range(0, 2.9f);
            Instantiate(_harpey[2], _harpeyRight[ram].transform.position, Quaternion.identity);
        }
        _insbool = false;   
    }


    enum Spawn_Pattern
    {
        Legion,  //  軍団
        RandomIns,  //　ランダム生成  
    }
}
