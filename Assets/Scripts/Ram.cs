using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    [SerializeField] float _interval = 3f;
    float _timer;
    [SerializeField] GameObject _prehab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        //一定の時間がたったらObject生成
        if (_timer > _interval)
        {
            _timer = 0;
            Instantiate(_prehab, transform.position, transform.rotation);
        }
    }
}
