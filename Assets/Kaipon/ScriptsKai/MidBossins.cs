using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossins : MonoBehaviour
{
    Vector2 _pos;
    [SerializeField] GameObject _midBoss;
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        Instantiate(_midBoss,transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
