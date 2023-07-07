using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    SceneLoaderAditiveClass sceneLoaderAditiveClass;
    [SerializeField] string sceneName;
    void Awake()
    {
        GameManager.Instance.Sansyo();
        GameManager.Instance.GameSetting();
    }

    private void Start()
    {
        if (sceneName != "")
        {
            PauseManager.PauseResume();
            sceneLoaderAditiveClass = new SceneLoaderAditiveClass();
            sceneLoaderAditiveClass.SceneLoaderAditive(sceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
