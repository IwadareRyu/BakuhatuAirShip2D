using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletIns : MonoBehaviour
{
    bool _insbool;
    [Header("弾の生成間隔")]
    [SerializeField] float _inssec = 3f;
    BulletPoolActive _pool;//プールでの弾の管理クラス
    [Header("弾の発射角度")]
    [SerializeField] float _angle = 270f;
    [Header("弾のスピード")]
    [SerializeField] float _speed = 3f;
    [Header("弾の種類")]
    [SerializeField] BulletTypeClass.BulletSpriteState _type;
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトが生成されたら時に敵の弾のプールを取得
        _pool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<BulletPoolActive>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_insbool)
        {
            _insbool = true;
            StartCoroutine(InsTime());
        }//コルーチンを開始する
    }
    //弾の生成間隔で弾を生成するコルーチン
    IEnumerator InsTime()
    {
        //弾の生成間隔の待機時間
        yield return new WaitForSeconds(_inssec);
        // プールから利用出来る弾を取得
        var bullet = _pool.GetBullet();
        var bulletcs = bullet.GetComponent<ActiveBullet>();
        // 弾を生成する位置設定
        bullet.transform.position = transform.position;
        bulletcs.BulletAdd(_angle,_speed,_type);
    }
}
