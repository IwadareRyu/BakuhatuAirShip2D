using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    [SerializeField] Text text;
    private string[] wordArray;
    private string words;
    // Start is called before the first frame update
    void Start()
    {
        words = "‚±,‚±,‚Å,‚Í,\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
