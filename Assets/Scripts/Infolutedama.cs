using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infolutedama : MonoBehaviour
{
    [Tooltip("初期の位置")]
    Vector2 _senterPos;
    [Tooltip("回る位置")]
    Vector2 _rotaPos;
    [Tooltip("最初の位置")]
    float _startTime;
    [Tooltip("円運動の速さ")]
    [SerializeField] float _speed = 2f;
    [SerializeField] GameObject _hit;
    [Tooltip("時計回りか反時計回りを1か-1を入力する。")]
    [SerializeField,Range(1,-1)] float _minas = 1f;
    [Tooltip("円の大きさ")]
    [SerializeField]float _radius = 3f;
    [Tooltip("円運動を始める位置")]
    [SerializeField] float _startCirclePos;
    // Start is called before the first frame update
    void Start()
    {
        _senterPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //ここでSetParentsを使って追尾させたいキャラの子にするのがよいと思う。

        //円運動のスタート位置
        _startTime = Time.time + _startCirclePos;
        //現在の位置から回るようにしている。
        _rotaPos = _senterPos;
        //中央からの距離をxとyのポジションの位置で計算する。
        _rotaPos.x += _radius * Mathf.Cos(2 * Mathf.PI * _speed * _startTime *_minas);
        _rotaPos.y += _radius * Mathf.Sin(2 * Mathf.PI * _speed * _startTime *_minas);
        //現在の位置を代入する。
        transform.position = _rotaPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
        }

    }
}
