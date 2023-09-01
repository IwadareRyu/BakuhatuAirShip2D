using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSansyo : MonoBehaviour
{
    private RouinSceneLoader _load;
    /// <summary>別のシーンに飛んで、ボタンを押したとき、GMのSceneLoaderに接続する用のスクリプト</summary>
    /// <param name="load"></param>
    public void Load(string load)
    {
        _load = GameObject.FindGameObjectWithTag("GM").GetComponent<RouinSceneLoader>();
        _load.ResultSceneLoad(load);
    }
}
