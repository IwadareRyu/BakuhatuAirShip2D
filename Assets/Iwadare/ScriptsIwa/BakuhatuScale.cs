using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class BakuhatuScale : MonoBehaviour
{
    [Tooltip("PowerUpクラスの参照")]
    PowerUp _power;
    [SerializeField, Tooltip("全体攻撃の爆発範囲の広さ")]
    float _allPowerScale = 5;
    [Tooltip("攻撃範囲の円の半径")]
    Vector2 _circleRadius;
    [Tooltip("攻撃範囲の円の中心座標")]
    Vector2 _cirleCenter;
    [Tooltip("攻撃フラグ")]
    bool _attackbool;
    [SerializeField,Tooltip("攻撃パターンの選択（Normal: 通常攻撃, AllAttack: 全体攻撃）"),Header("攻撃パターンの選択（Normal: 通常攻撃, AllAttack: 全体攻撃)")]
    Pattern _pattern;

    [SerializeField,Tooltip("カメラの振動設定のクラス")]
    CinemachineImpulseSource _impulse;
    [Tooltip("カメラ振動の強さ")]
    [SerializeField] float _impulsePower = 1;


    void Start()
    {
        // 爆発音を再生し、カメラを振動させる
        BGMManager.Instance.SEPlay(SE.Explosion);
        _impulse.GenerateImpulseAt(new Vector2(0, 0), new Vector2(0, _impulsePower));

        // 攻撃パターンの分岐
        if (_pattern == Pattern.Normal)
        {
            // 通常攻撃時の大きさを設定する。
            _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
            transform.localScale = new Vector2(1.0f + _power._bakuhatuPower * 0.15f, 1.0f + _power._bakuhatuPower * 0.15f);
            _circleRadius = new Vector3(1.0f + _power._bakuhatuPower * 0.15f, 0);
            _cirleCenter = transform.position;
        }
        else
        {
            // 全体攻撃時の大きさを設定する。
            _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
            transform.localScale = new Vector2(_allPowerScale, _allPowerScale);
            _circleRadius = new Vector3(_allPowerScale, 0);
            _cirleCenter = transform.position;
        }
    }


    void Update()
    {
        // 攻撃範囲をデバッグで表示
        Debug.DrawLine(_cirleCenter, _cirleCenter + _circleRadius / 2);

        // 攻撃範囲に入った対象全部を保存する配列
        Collider2D[] attacklange;
        if (_pattern == Pattern.Normal)
        {
            // 通常攻撃時の攻撃範囲
            attacklange = Physics2D.OverlapCircleAll(_cirleCenter, (1.0f + _power._bakuhatuPower * 0.15f) / 2);
        }
        else
        {
            // 全体攻撃時の攻撃範囲
            attacklange = Physics2D.OverlapCircleAll(_cirleCenter, _allPowerScale / 2);
        }

        if (!_attackbool)
        {
            foreach (var a in attacklange)
            {
                if (a.gameObject.tag == "Enemy")
                {
                    var dead = a.GetComponent<DestroyEnamy>();
                    dead.Damage();
                }   //攻撃対象がEnemyならダメージ(死亡)
                else if (a.gameObject.tag == "Boss" && !_attackbool && _pattern == Pattern.Normal)
                {
                    var boss = a.GetComponent<BossGanerator>();
                    boss.AddBossDamage(-1.0f);
                }//通常攻撃で攻撃対象がEnemyなら1ダメージ
                else if (a.gameObject.tag == "EnemyBullet" && _pattern == Pattern.AllAttack)
                {
                    var rb = a.GetComponent<Rigidbody2D>();
                    if (rb)
                    {
                        rb.simulated = true;
                    }
                    a.gameObject.SetActive(false);
                }   //全体攻撃で、敵の弾なら弾を使っていない状態にする。
            }
            _attackbool = true;
        }
    }

    /// <summary>攻撃パターンのenum型</summary>
    enum Pattern
    {
        /// <summary>通常攻撃</summary>
        Normal, 
        /// <summary>全体攻撃</summary>
        AllAttack,
    }
}