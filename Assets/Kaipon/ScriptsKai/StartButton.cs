using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickStartButton()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    //シーンの読み込みを遅延させる
    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("tutorial");
    }//1秒遅延させ、チュートリアルシーンを読み込む
}
