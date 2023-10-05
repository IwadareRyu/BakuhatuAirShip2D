using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouinSceneLoader : MonoBehaviour
{
    /// <summary>�V�[���̃��[�h</summary>
    /// <param name="sceneName"></param>
    public void SceneLoad(string sceneName)
    {
        StartCoroutine(LoadTime(sceneName));
    }

    /// <summary>���g���C�ƃX�R�A�A���C�t�̃��Z�b�g�̃��[�h</summary>
    public void ActiveSceneLoad()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>�X�R�A�A���C�t�̃��Z�b�g�̃V�[���̃��[�h</summary>
    /// <param name="sceneName"></param>
    public void ResultSceneLoad(string sceneName)
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        StartCoroutine(LoadTime(sceneName));
    }

    /// <summary>�A�v���P�[�V�����̏I��</summary>
    public void EndGame()
    {
        Application.Quit();
    }

    //�V�[�������[�h����܂ł̑ҋ@���ԁB
    IEnumerator LoadTime(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}