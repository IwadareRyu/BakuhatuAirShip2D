using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    bool _attackbool;
    bool _oneshot;
    [SerializeField] GameObject _longFire;
    [SerializeField] GameObject _zikiFire;
    [SerializeField] GameObject[] _bullet;
    [SerializeField] Transform[] _idouPoint;
    [SerializeField] float _stopdis = 0.5f;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _attackSpeed = 5f;
    [SerializeField] float _stopTime = 2f;
    [SerializeField]BossState _boss;
    [SerializeField]bool _stop;
    bool _moguraAttackbool;
    int _ram;

    // Start is called before the first frame update
    void Start()
    {
        _zikiFire.SetActive(false);
        _longFire.SetActive(false);
        if(_bullet.Length != 0)
        {
            foreach(var i in _bullet)
            {
                i.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackbool)
        {
            if (!_oneshot)
            {
                _ram = (int)Random.Range(0, 2.9f);
                _stop = false;
                StartCoroutine(AttackTime());
                _oneshot = true;
            }
            float distance = Vector2.Distance(transform.position, _idouPoint[_ram].position);
            if (distance > _stopdis && !_stop)
            {
                Vector3 dir = (_idouPoint[_ram].position - transform.position).normalized * _speed;
                transform.Translate(dir * Time.deltaTime);
            }
            else
            {
                _stop = true;
            }
            if (_moguraAttackbool)
            {
                Vector3 dir = Vector3.down.normalized * _attackSpeed;
                transform.Translate(dir * Time.deltaTime);
            }
        }
    }

    IEnumerator AttackTime()
    {
        yield return new WaitWhile(() => _stop == false);
        yield return new WaitForSeconds(2f);
        if (_attackbool)
        {
            if (_boss == BossState.Drgon)
            {
                _longFire.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                if (_bullet.Length > 2)
                {
                    var ram = (int)Random.Range(0f, 1.9f);
                    for (var i = ram; i < _bullet.Length; i += 2)
                    {
                        _bullet[i].SetActive(true);
                    }
                }
                yield return new WaitForSeconds(3.5f);
                if (_bullet.Length != 0)
                {
                    foreach (var i in _bullet)
                    {
                        i.SetActive(false);
                    }
                }
                _longFire.SetActive(false);
            }
            else if (_boss == BossState.MoguraBoss)
            {
                _longFire.SetActive(true);
                _moguraAttackbool = true;
                yield return new WaitForSeconds(_stopTime);
                _moguraAttackbool = false;
                yield return new WaitForSeconds(0.1f);
                _longFire.SetActive(false);
                yield return new WaitForSeconds(2f);
            }
        }

        _oneshot = false;
    }

    public void OverMode()
    {
        if(_idouPoint.Length > 0)
        {
            transform.position = _idouPoint[0].position;
        }
        _attackbool = !_attackbool;
        _zikiFire.SetActive(_attackbool);
        _longFire.SetActive(false);
    }

    enum BossState
    {
        Drgon,
        MoguraBoss,
    }
}
