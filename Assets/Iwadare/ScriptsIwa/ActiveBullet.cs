using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBullet : MonoBehaviour
{
    [Tooltip("跳ぶ向き")]
    [SerializeField] float _angle;
    [Tooltip("速さ")]
    [SerializeField] float _speed = 3f;
    [Tooltip("RotateBulletの時、外に広げるための変数。")]
    float dist = 0;
    [Tooltip("_rb.velocityに代入するための変数。")]
    Vector3 _velocity;
    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    [Tooltip("球のPositionを移動させる変数。")]
    Vector3 _transA;
    [Header("生成時一度止まる球")]
    bool _stop;
    [Header("炎の色をアタッチ")]
    [SerializeField] Sprite _redFire;
    [SerializeField] Sprite _blueFire;
    [Header("スフィアの色をアタッチ")]
    [SerializeField] Sprite _bluesphere;
    [SerializeField] Sprite _redsphere;
    [Tooltip("炎の色、形によって動きを分けるためのenum")]
    BulletTypeClass.BulletSpriteState _state;
    bool _stopbool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //画面の外に出ると、SetActiveがfalseになるReset関数。
        if (transform.position.x > 7 || transform.position.x < -7
            || transform.position.y > 8 || transform.position.y < -7)
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        //RotateBulletの場合の処理。StateがSphereだとその処理になる。
        if (_state == BulletTypeClass.BulletSpriteState.LeftSphere || _state == BulletTypeClass.BulletSpriteState.RightSphere)
        {
            //球をぐるぐる回す処理。球がスポーンした場所を中心に回っている。
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad) * dist;
            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad) * dist;
            _rb.velocity = _velocity;

            //スフィアの色によって回る方向を変える。
            if(_state == BulletTypeClass.BulletSpriteState.LeftSphere)
            {
                _angle += 0.3f;
            }
            else if(_state == BulletTypeClass.BulletSpriteState.RightSphere)
            {
                _angle -= 0.3f;
            }

            //distのインクリメントして広がる球を作っている。
            dist += 0.1f;
        }
        
    }

    /// <summary>球の向きに沿ってまっすぐ飛ぶ球。</summary>
    private void Type0Move()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        _rb.velocity = _velocity;
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }
    }

    /// <summary>プレイヤーに向かって飛ぶ球</summary>
    private void Type1Move()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            Vector2 v = player.transform.position - transform.position;
            v = v.normalized * _speed;
            float zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
            _rb.velocity = v;
        }
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }
    }

    /// <summary>一度球を止めてから一定時間たった後に動き出す処理。</summary>
    IEnumerator StopBullet()
    {
        _rb.simulated = false;
        _stopbool = false;
        yield return new WaitWhile(() => _stopbool == false);
        _rb.simulated = true;
    }

    /// <summary>球をpoolに返す際に実行する処理</summary>
    public void Reset()
    {
        _transA = transform.position;
        _transA.y = 10f;
        transform.position = _transA;
        _rb.simulated = true;
        gameObject.SetActive(false);
    }

    /// <summary>Bulletの動きを設定する処理</summary>
    /// <param name="angle">球の向き</param>
    /// <param name="speed">速さ</param>
    /// <param name="type">球の色、動きを決めるstate
    /// (クラスでstateを決めているので他スクリプトのstateの中身は同じ)</param>
    /// <param name="stop">球を止めるか止めないか
    /// (入力しなくても自動で止めない設定にしている)</param>
    public void BulletAdd(float angle, float speed, BulletTypeClass.BulletSpriteState type,bool stop = false,bool change = false)
    {
        //入力されてきた変数をそれぞれ代入。
        _angle = angle;
        _speed = speed;
        _state = type;
        _stop = stop;
        dist = 0;
        //stateによって球の色を変えたり、動きの処理をするswitch文。
        switch(_state)
        {
            case BulletTypeClass.BulletSpriteState.LeftSphere:
                _sprite.sprite = _bluesphere;
                break;
            case BulletTypeClass.BulletSpriteState.RightSphere:
                _sprite.sprite = _redsphere;
                break;
            case BulletTypeClass.BulletSpriteState.RedFire:
                _sprite.sprite = _redFire;
                Type0Move();
                break;
            case BulletTypeClass.BulletSpriteState.BlueFire:
                _sprite.sprite = _blueFire;
                Type1Move();
                break;
        }
    }
    public void StopTrue()
    {
        _stopbool = true;
    }
}
