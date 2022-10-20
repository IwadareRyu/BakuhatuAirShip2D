using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakuhatuScale : MonoBehaviour
{
    PowerUp _power;
    // Start is called before the first frame update
    void Start()
    {
        _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
        transform.localScale = new Vector2(1.0f + _power._bakuhatuPower * 0.1f,1.0f + _power._bakuhatuPower * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
