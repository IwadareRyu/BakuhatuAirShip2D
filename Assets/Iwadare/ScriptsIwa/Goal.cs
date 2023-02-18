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
    float _stopdis = 0.5f;
    [SerializeField] float _countDownTime = 60f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetDeadEnemy(_deadEnemy);
        GameManager.Instance.SetCountDown(_countDownTime);
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
        }
    }
    //trigger,collison関わらず、プレイヤーに当たったらイベントが起こる。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _action.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _action.Invoke();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
