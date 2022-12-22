using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIns : MonoBehaviour
{
    [SerializeField] float _randomIntervalmax = 3f;
    [SerializeField] float _randomIntervalmin = 1f;
    [SerializeField] GameObject _prehab;
    bool _intervalTime;
    BulletPool _pool;
    
    // Start is called before the first frame update
    void Start()
    {
        _pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_intervalTime)
        {
            _intervalTime = true;
            StartCoroutine(Interval());
        }
    }

    IEnumerator Interval()
    {
        var ram = Random.Range(_randomIntervalmin, _randomIntervalmax);
        yield return new WaitForSeconds(ram);
        var bullet = _pool.GetBullet();
        bullet.transform.localPosition = transform.position;
        _intervalTime = false;
    }
}
