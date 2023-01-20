using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIns : MonoBehaviour
{
    [SerializeField] float _count = 5;
    [SerializeField] BulletPoolActive _pool;
    bool _bulletime;
    ActiveBossBullet bulletcs;
    float _rad;
    [SerializeField] float _bulletspeed;

    // Update is called once per frame
    void Update()
    {
        if (!_bulletime)
        {
            _bulletime = true;
            StartCoroutine(BulletTime());
        }
    }

    IEnumerator BulletTime()
    {
        yield return new WaitForSeconds(_count);
        var random = Random.Range(10,30);
        for (int rad = 0; rad < 360; rad += random)
        {
            var bullet = _pool.GetBullet();
            bulletcs = bullet.GetComponent<ActiveBossBullet>();
            bullet.transform.position = transform.position;
            bulletcs.BulletAdd(rad,_bulletspeed);
        }
        _bulletime = false;

    }
}
