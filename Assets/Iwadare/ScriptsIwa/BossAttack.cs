using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    bool _attackbool;
    [SerializeField] GameObject[] _bullet;
    [SerializeField] Transform[] _idouPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_attackbool)
        {

        }
    }

    public void OverMode()
    {
        if(_idouPoint.Length > 0)
        {
            transform.position = _idouPoint[0].position;
        }
        _attackbool = !_attackbool;
    }

}
