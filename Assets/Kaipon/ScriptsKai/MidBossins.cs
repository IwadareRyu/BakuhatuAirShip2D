using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossins : MonoBehaviour
{
    //�{�X�̐����ʒu
    Vector2 _pos;
    //��������{�X�̃v���n�u
    [SerializeField] GameObject _midBoss;
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        //�{�X�𐶐�
        Instantiate(_midBoss,transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
