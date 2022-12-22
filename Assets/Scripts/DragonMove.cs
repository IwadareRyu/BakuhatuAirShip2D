using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMove : MonoBehaviour
{
    [SerializeField] Transform[] _targets;
    int _current;
    [SerializeField]float _stopdis = 0.5f;
    [SerializeField]float _speed= 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position,_targets[_current].position);
        if(distance > _stopdis)
        {
            Vector3 dir = (_targets[_current].position - transform.position).normalized * _speed;
            transform.Translate(dir * Time.deltaTime);
        }
        else
        {
            _current++;
            _current = _current % _targets.Length;
        }
    }
}
