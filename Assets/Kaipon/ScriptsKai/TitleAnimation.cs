using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public void TitleAni()
    {
        BGMManager.Instance.SEPlay(BGMManager.SE.Explosion);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
