using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouinSceneLoader : MonoBehaviour
{
    /// <summary>シーンのロード</summary>
    /// <param name="sceneName"></param>
    public void SceneLoad(string sceneName)
    {
        StartCoroutine(LoadTime(sceneName));
    }

    /// <summary>リトライとスコア、ライフのリセットのロード</summary>
    public void ActiveSceneLoad()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>スコア、ライフのリセットのシーンのロード</summary>
    /// <param name="sceneName"></param>
    public void ResultSceneLoad(string sceneName)
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        StartCoroutine(LoadTime(sceneName));
    }

    /// <summary>アプリケーションの終了</summary>
    public void EndGame()
    {
        Application.Quit();
    }

    //シーンをロードするまでの待機時間。
    IEnumerator LoadTime(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
