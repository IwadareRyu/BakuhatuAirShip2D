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
        //_insboolをfalseに初期化。
        _insbool = false;
    }
    // Update is called once per frame
    void Update()
    {
        //_insboolがfalseの場合に実行
        if(_insbool == false)
        {
           //_insboolをtrueに設定してモンスターの生成を開始
            _insbool = true;
            if(_pattern == Spawn_Pattern.Legion)
            {
                // Spawn_PatternがLegionの場合、HarpeyLegionInstansコルーチンを開始
                StartCoroutine(HarpeyLegionInstans());
            }
            else if(_pattern == Spawn_Pattern.RandomIns)
            {
                // Spawn_PatternがRandomInsの場合、RandomInstanceコルーチンを開始
                StartCoroutine(RandomInstance());
            }
        }
    }
    /// <summary>モンスターを一つずつ順番に生成するコルーチン</summary>
    IEnumerator HarpeyLegionInstans()
    {
        yield return new WaitForSeconds(_inssec);
        var random = Random.Range(0,2);
        // スポーン場所からランダムな位置にharpeyを生成
        Instantiate(_harpey[0], _harpeyDown[random].transform.position,Quaternion.identity);
        yield return new WaitForSeconds(_inssec);
        random = Random.Range(0, 3);
        Instantiate(_harpey[1], _harpeyLeft[random].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_inssec);
        random = Random.Range(0, 3);
        Instantiate(_harpey[2], _harpeyRight[random].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_coolsec);
        // 生成完了後、_insboolをfalseにする
        _insbool = false;
    }
   /// <summary>ランダムな位置にharpeyを生成するコルーチン</summary>
    IEnumerator RandomInstance()
    {
        yield return new WaitForSeconds(_inssec);
        var random = Random.Range(0, 3);
        if (random == 0)
        {
            random = Random.Range(0, 2);
            Instantiate(_harpey[0], _harpeyDown[random].transform.position, Quaternion.identity);
        }   //randomが0の場合、ランダムな値を取得しharpeyを生成
        else if (random == 1)
        {
            random = Random.Range(0, 3);
            Instantiate(_harpey[1], _harpeyLeft[random].transform.position, Quaternion.identity);
        }   //randomが1の場合、ランダムな値を取得しharpeyを生成
        else 
        {
            random = Random.Range(0, 3);
            Instantiate(_harpey[2], _harpeyRight[random].transform.position, Quaternion.identity);
        }   //randomが2の場合、ランダムな値を取得しharpeyを生成
        //生成終了後、_insboolをfalseに戻す
        _insbool = false;   
    }

    //モンスターの生成パターンを指定するための列挙型
    enum Spawn_Pattern
    {
        Legion,  //  軍団
        RandomIns,  //　ランダム生成  
    }
}
