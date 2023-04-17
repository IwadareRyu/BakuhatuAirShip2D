using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sansyo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResultSansyo()
    {
        GameManager.Instance.Resultscore();
        GameManager.Instance.ShowScore();
    }
    public void Build()
    {
        PowerUp.Instance.BuildUp();
    }

    public void SceneSansyo(string scene)
    {
        PowerUp.Instance.SceneName(scene);
    }

    public void ResetSceneSansyo(string scene)
    {
        SceneLoader.Instance.ResultSceneLoad(scene);
    }
}
