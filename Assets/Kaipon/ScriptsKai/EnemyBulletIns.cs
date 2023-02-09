using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletIns : MonoBehaviour
{
    bool _insbool;
    [SerializeField] float _inssec = 3f;
    BulletPoolActive _pool;
    [SerializeField] float _angle = 270f;
    [SerializeField] float _speed = 3f;
    [SerializeField] BulletTypeClass.BulletSpriteState _type;
    // Start is called before the first frame update
    void Start()
    {
        _pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<BulletPoolActive>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_insbool)
        {
            _insbool = true;
            StartCoroutine(InsTime());
        }
    }

    IEnumerator InsTime()
    {
        yield return new WaitForSeconds(_inssec);
        var bullet = _pool.GetBullet();
        var bulletcs = bullet.GetComponent<ActiveBullet>();
        bullet.transform.position = transform.position;
        bulletcs.BulletAdd(_angle,_speed,_type);
    }
}
