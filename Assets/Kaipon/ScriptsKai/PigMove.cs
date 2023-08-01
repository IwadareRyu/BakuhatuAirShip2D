using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] float _speed = 3f;
    //停止する秒数
    [SerializeField] float _stopsec = 10f;
    //弾を発射する位置のオブジェクト
    [SerializeField] GameObject _bulletpoint;
    Rigidbody2D _rb;
    float _y;
    [Tooltip("停止判定")]
    bool _stopBool;
    [SerializeField] bool _takatori;

    private void OnEnable()
    {
        //敵がアクティブになったらランダムなY座標を生成
        _y = Random.Range(0f, 4f);
        _stopBool = false;
    }

    // Start is called before the first frame updat
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        //敵を下方向に移動させる
        _rb.velocity = Vector2.down * _speed;
        _bulletpoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= _y && _stopBool == false)
        {
            _stopBool = true;
            StartCoroutine(StopTime());
        }//敵のY座標が停止位置以下になったら停止する
        if (this.transform.position.y <= -7f)
        {
            Destroy(gameObject);
        }//敵の座標が-7以下になったら敵を破壊
    }
    //一時停止のためのコルーチン
    IEnumerator StopTime()
    {
        _bulletpoint.SetActive(true);
        //敵の速度を0にする
        _rb.velocity *= 0;
        //待機時間
        yield return new WaitForSeconds(_stopsec);
        //待機時間が終わったら敵を再び下方向に移動させる
        _rb.velocity = Vector2.down * _speed;
        if(_takatori)
        {
            _bulletpoint.SetActive(false);
        }
    }

}
