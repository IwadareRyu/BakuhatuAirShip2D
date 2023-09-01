using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    [Tooltip("タイムラインの際消したいオブジェクト")]
    [SerializeField] GameObject _title;
    [Tooltip("タイムラインの際、表示させたいオブジェクト")]
    [SerializeField] GameObject _timelime;
    [Tooltip("タイムラインの後シーン移動したいときのオブジェクト")]
    [SerializeField] GameObject _sceneLoad;

    private void Start()
    {
        
    }

    /// <summary>タイムラインがスタートした時呼び出される関数。</summary>
    public void TimeLineStart()
    {
        _title.gameObject.SetActive(false);
        _timelime.gameObject.SetActive(true);
    }

    /// <summary>タイムラインが終了したとき呼び出される関数。</summary>
    public void TimeLineEnd()
    {
        _title.gameObject.SetActive(true);
        StartCoroutine("TimeLineTime");
    }
    /// <summary>タイムラインの後、呼び出したいときに使う関数。</summary>
    public void TimeSceneLoad()
    {
        _sceneLoad.gameObject.SetActive(true);
    }

    /// <summary>タイムラインをスキップしたいときの関数。</summary>
    public void Skip()
    {
        _title.gameObject.SetActive(true);
        _timelime.gameObject.SetActive(false);
    }

    /// <summary>消したいオブジェクトを消すまでのタイムラグ。</summary>
    /// <returns></returns>
    IEnumerator TimeLineTime()
    {
        yield return new WaitForSeconds(0.5f);
        _timelime.gameObject.SetActive(false);
    }
}
