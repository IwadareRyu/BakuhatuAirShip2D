using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BakuhatuScale : MonoBehaviour
{
    PowerUp _power;
    [SerializeField] float _allPower = 5;
    Vector2 cir;
    Vector2 start;
    bool _attack;
    [SerializeField] Pattern _pattern;

    // Start is called before the first frame update
    void Start()
    {
        SEManager.Instance.SEPlay(0);
        if (_pattern == Pattern.Normal)
        {
            _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
            transform.localScale = new Vector2(1.0f + _power._bakuhatuPower * 0.1f, 1.0f + _power._bakuhatuPower * 0.1f);
            //transform.DOScale(Vector3.one * (1.0f + _power._bakuhatuPower * 0.1f), 0.1f);
            cir = new Vector3(1.0f + _power._bakuhatuPower * 0.1f, 0);
            start = transform.position;
        }
        else
        {
            _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
            transform.localScale = new Vector2(_allPower, _allPower);
            //transform.DOScale(Vector3.one * (1.0f + _power._bakuhatuPower * 0.1f), 0.1f);
            cir = new Vector3(_allPower, 0);
            start = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(start,start + cir/2);
        Collider2D[] attacklange;
        if (_pattern == Pattern.Normal)
        {
            attacklange = Physics2D.OverlapCircleAll(start, (1.0f + _power._bakuhatuPower * 0.1f) / 2);
        }
        else
        {
            attacklange = Physics2D.OverlapCircleAll(start, _allPower / 2);
        }

        if (!_attack)
        {
            foreach (var a in attacklange)
            {
                if (a.gameObject.tag == "Enemy")
                {
                    var dead = a.GetComponent<DestroyEnamy>();
                    dead.Damage();
                }
                else if (a.gameObject.tag == "Boss" && !_attack && _pattern == Pattern.Normal)
                {
                    var boss = a.GetComponent<BossGanerator>();
                    boss.AddBossDamage(-1.0f);
                }
                else if (a.gameObject.tag == "EnemyBullet" && _pattern == Pattern.AllAttack)
                {
                    var rb = a.GetComponent<Rigidbody2D>();
                    if (rb)
                    {
                        rb.simulated = true;
                    }
                    a.gameObject.SetActive(false);
                }
            }
            _attack = true;
        }
    }

    enum Pattern
    {
        Normal,
        AllAttack,
    }
}
