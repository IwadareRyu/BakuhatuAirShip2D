using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonovihair<SceneLoader>
{
    protected override bool _dontDestroyOnLoad { get { return true; } }
    /// <summary>シーンのロード</summary>
    /// <param name="sceneName"></param>
    public void SceneLoad(string sceneName)
    {
        StartCoroutine(SceneLoadTime(sceneName));
    }

    /// <summary>リトライ</summary>
    public void ActiveSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    /// <summary>スコア、ライフのリセットのシーンのロード</summary>
    /// <param name="sceneName"></param>
    public void ResultSceneLoad(string sceneName)
    {
        GameManager.Instance.ResetScore();
        StartCoroutine(SceneLoadTime(sceneName));
    }

    /// <summary>アプリケーションの終了</summary>
    public void EndGame()
    {
        Application.Quit();
    }

    //シーンをロードするまでの待機時間。
    IEnumerator SceneLoadTime(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void MoveManagerSceneLoad(string sceneName)
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        StartCoroutine(SceneLoadTime(sceneName));
    }
}
