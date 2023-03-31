using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBullet : MonoBehaviour
{
    BulletPoolActive _pool;
    bool _getTime;
    // Start is called before the first frame update
    void Start()
    {
        _pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<BulletPoolActive>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_getTime)
        {
            _getTime = true;
            StartCoroutine(Get());
        }
    }

    IEnumerator Get()
    {
        var bullet = _pool.GetBullet();
        bullet.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        _getTime = false;
    }
    
    public void De()
    {
        Debug.Log("âüÇ≥ÇÍÇΩÇÊÅI");
    }
}
