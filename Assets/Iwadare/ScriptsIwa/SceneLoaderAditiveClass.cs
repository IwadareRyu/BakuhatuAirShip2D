using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAditiveClass
{
    public void SceneLoaderAditive(string loadScene)
    {
        SceneManager.LoadScene(loadScene,LoadSceneMode.Additive);
    }
}
