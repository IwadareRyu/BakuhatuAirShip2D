using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIns : MonoBehaviour
{
    Vector2 _pos;
    float _x;
    [SerializeField] GameObject _pig;
    [SerializeField] float _insSec;
    [SerializeField] float _high = 4f;
    [SerializeField] float _low = -4f;
    float _time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _insSec)
        {
            _x = Random.Range(_low, _high);
            _pos = new Vector2(_x, this.transform.position.y);
            Instantiate(_pig, _pos, Quaternion.identity);
            _time = 0;
        }
    }
    public void InsSecChange(float num)
    {
        _insSec = num;
    }

}
