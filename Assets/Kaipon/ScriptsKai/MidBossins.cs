using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossins : MonoBehaviour
{
    //ボスの生成位置
    Vector2 _pos;
    //生成するボスのプレハブ
    [SerializeField] GameObject _midBoss;
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        //ボスを生成
        Instantiate(_midBoss,transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
