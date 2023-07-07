using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBullet : MonoBehaviour
{
    [Tooltip("弾の飛ぶ向き")]
    [SerializeField] float _angle;
    [Tooltip("弾の速さ")]
    [SerializeField] float _speed = 3f;
    [Tooltip("RotateBulletの時、外に広げるための変数。")]
    float dist = 0;
    [Tooltip("Rigidbody2Dのvelocityに代入するための変数")]
    Vector3 _velocity;
    [Tooltip("Rigidbody2D")]
    Rigidbody2D _rb;
    [Tooltip("SpriteRenderer")]
    SpriteRenderer _sprite;
    [Tooltip("弾の位置を移動させるための変数")]
    Vector3 _transA;
    [Header("生成時に一度止まるかどうかを示すフラグ")]
    bool _stop;
    [Header("炎の色をアタッチ")]
    [Tooltip("炎の赤い色")]
    [SerializeField] Sprite _redFire;
    [Tooltip("炎の青い色")]
    [SerializeField] Sprite _blueFire;
    [Header("スフィアの色をアタッチ")]
    [Tooltip("青いスフィア")]
    [SerializeField] Sprite _bluesphere;
    [Tooltip("赤いスフィア")]
    [SerializeField] Sprite _redsphere;
    [Tooltip("炎の色や形によって動きを分けるためのenum")]
    BulletTypeClass.BulletSpriteState _state;
    [Tooltip("弾を一時停止するかどうかを示すフラグ")]
    bool _stopbool;
    [Tooltip("弾の一回目の動きの変化を行うかどうかを示すフラグ")]
    bool _change1;
    [Tooltip("弾の一回目の動きの変化の時にプレイヤーに向かって球が飛ぶかどうかを示すフラグ")]
    bool _change1ziki;
    [Tooltip("弾の二回目の動きの変化を行うかどうかを示すフラグ")]
    bool _change2;
    [Header("弾が消える位置"),Tooltip("弾が消える位置の範囲")]
    [SerializeField] float yup = 8, ydown = -7, xup = 7, xdown = -7;
    [Tooltip("発射音")]
    [SerializeField] AudioClip _fireOrShot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 画面の外に出るとSetActiveがfalseになるReset関数を実行する。
        if (transform.position.x > xup || transform.position.x < xdown
            || transform.position.y > yup || transform.position.y < ydown)
        {
            Reset();
        }
    }

    private void FixedUpdate()
    {
        // RotateBulletの場合の処理。StateがLeftSphere,RightSphereだとこの処理になる。
        if (_state == BulletTypeClass.BulletSpriteState.LeftSphere || _state == BulletTypeClass.BulletSpriteState.RightSphere)
        {
            // 球をぐるぐる回す処理。球がスポーンした場所を中心に回っている。
            _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad) * dist;
            _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad) * dist;
            _rb.velocity = _velocity;

            // スフィアの色によって回る方向を変える。
            if (_state == BulletTypeClass.BulletSpriteState.LeftSphere)
            {
                _angle += 0.3f;
            }
            else if (_state == BulletTypeClass.BulletSpriteState.RightSphere)
            {
                _angle -= 0.3f;
            }

            // distのインクリメントして広がる球を作っている。
            dist += 0.1f;
        }

    }

    /// <summary>
    /// 決まった方向に向かって弾を真っ直ぐ飛ばす処理
    /// </summary>
    private void Type0Move()
    {
        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
        float zAngle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, zAngle);
        _rb.velocity = _velocity;
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }   //一度弾を止める処理
        if (_change1)
        {
            StartCoroutine(ChangeBullet());
        }   //弾の動きを変える処理
    }

    /// <summary>プレイヤーに向かって飛ぶ弾</summary>
    private void Type1Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Vector2 v = player.transform.position - transform.position;
            v = v.normalized * _speed;
            float zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(0, 0, zAngle);
            _rb.velocity = v;
        }   //Playerに向かってまっすぐ飛ぶ。
        if (_stop)
        {
            StartCoroutine(StopBullet());
        }   //一度止まる弾を止めた時の処理
    }

    /// <summary>一度弾を止めてから一定時間たった後に動き出す処理。</summary>
    IEnumerator StopBullet()
    {
        _rb.simulated = false;
        _stopbool = false;
        yield return new WaitWhile(() => _stopbool == false);
        _rb.simulated = true;
    }

    /// <summary>弾に動きの変化をつける処理。</summary>
    IEnumerator ChangeBullet()
    {
        yield return new WaitForSeconds(1f);
        _rb.simulated = false;
        yield return new WaitForSeconds(1f);

        if (!_change1ziki)
        {
            _sprite.sprite = _bluesphere;
            _state = BulletTypeClass.BulletSpriteState.LeftSphere;
            _rb.simulated = true;
            _speed = 0.2f;
        }   //右回りから左回りへ
        else
        {
            _sprite.sprite = _blueFire;
            _rb.simulated = true;
            _speed = 5f;
            Type1Move();
        }   //自機に飛ばす
        if (_change2)
        {
            yield return new WaitForSeconds(2f);
            _rb.simulated = false;
            yield return new WaitForSeconds(1f);
            _sprite.sprite = _redsphere;
            _state = BulletTypeClass.BulletSpriteState.RightSphere;
            _speed = -0.2f;
            _rb.simulated = true;
        }   //２回目の変化を行うときの処理、右回りに戻す。
    }

    /// <summary>弾をpoolに返す際に実行する処理</summary>
    public void Reset()
    {
        _transA = transform.position;
        _transA.y = 10f;
        transform.position = _transA;
        _rb.simulated = true;
        gameObject.SetActive(false);
    }

    /// <summary>Bulletの動きを設定する処理</summary>
    /// <param name="angle">弾の向き</param>
    /// <param name="speed">速さ</param>
    /// <param name="type">弾の色、動きを決めるstate
    /// (クラスでstateを決めているので他スクリプトのstateの中身は同じ)</param>
    /// <param name="stop">弾を止めるか止めないか
    /// (入力しなくても自動で止めない設定にしている)</param>
    public void BulletAdd(float angle, float speed, BulletTypeClass.BulletSpriteState type, bool stop = false, bool change1 = false, bool change2 = false, bool change1ziki = false)
    {
        //入力されてきた変数をそれぞれ代入。
        _angle = angle;
        _speed = speed;
        _state = type;
        _stop = stop;
        _change1 = change1;
        _change2 = change2;
        _change1ziki = change1ziki;
        dist = 0;
        //stateによって弾の色を変えたり、動きの処理をするswitch文。
        switch (_state)
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

    /// <summary>弾を一時停止するための関数</summary>
    public void StopTrue()
    {
        _stopbool = true;
    }

}
