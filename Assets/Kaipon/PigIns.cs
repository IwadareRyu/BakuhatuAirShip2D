using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIns : MonoBehaviour
{
    Vector2 _pos;
    float _x;
    [SerializeField] GameObject _pig;
    [SerializeField] float _insSec;
    bool _instansbool;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        _instansbool = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (_instansbool == false)
        {
            _instansbool = true;
            StartCoroutine(Instans());
        }
    }
    IEnumerator Instans()
    {
        yield return new WaitForSeconds(_insSec);
        _x = Random.Range(-4f, 4f);
        _pos = new Vector2(_x, this.transform.position.y);
        Instantiate(_pig, _pos, Quaternion.identity);
        _instansbool = false;
    }
}
