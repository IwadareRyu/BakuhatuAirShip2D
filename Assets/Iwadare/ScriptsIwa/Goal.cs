using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    [SerializeField] UnityEvent _action;
    [SerializeField] int _deadEnemy = 100;
    [SerializeField] Transform _goalPos;
    [SerializeField] float _speed = 1f;
    [SerializeField] GameObject _clearText;
    float _stopdis = 0.5f;
    [SerializeField] float _countDownTime = 60f;
    bool _goalbool;
    [SerializeField] RandomManege _enemymanege;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetDeadEnemy(_deadEnemy);
        GameManager.Instance.SetCountDown(_countDownTime);
        _clearText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance._enemy == 0)
        {
            float distance = Vector2.Distance(transform.position, _goalPos.position);
            if(distance > _stopdis)
            {
                Vector3 dir = (_goalPos.position - transform.position).normalized * _speed;
                transform.Translate(dir * Time.deltaTime);
            }
            if (!_goalbool) 
            {
                _goalbool = true;
                if (_enemymanege) 
                {
                    _enemymanege.ResetEnemy();
                }
                var bullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
                if (bullet.Length != 0)
                {
                    foreach (var i in bullet)
                    {
                        i.SetActive(false);
                    }
                }
                var enemy = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemy.Length != 0)
                {
                    foreach (var i in enemy)
                    {
                        i.SetActive(false);
                    }
                }
            }
        }
    }
    //trigger,collison関わらず、プレイヤーに当たったらイベントが起こる。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _clearText.SetActive(true);
            //_action.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _clearText.SetActive(true);
            //_action.Invoke();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
