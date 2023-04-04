using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Sansyo();
        GameManager.Instance.GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
