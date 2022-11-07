using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyEnamy : MonoBehaviour
{
    [SerializeField] int _score = 100;
    [SerializeField] int _enemyHP = 1;
    [SerializeField] GameObject _zangeki;
    [SerializeField] GameObject _hit;
    [SerializeField] GameObject _bakuhatu;
    private GameManager GM;
    [SerializeField] bool _event;
    [SerializeField] UnityEvent _action;
    [SerializeField] GameObject[] _money;
    [SerializeField] int[] _moneycount;
    // Start is called before the first frame update
    void Start()
    {
        //GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //�G��HP��0�ɂȂ�����
        if(_enemyHP <= 0)
        {
            //�C�x���g��bool��true�Ȃ�C�x���g���N����B
            if (_event)
            {
                _action.Invoke();
            }
            Instantiate(_hit, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�a���G�t�F�N�g�������Ď��g��HP�����炷�B
        if(collision.gameObject.tag == ("Atari"))
        {
            Instantiate(_zangeki, transform.position, Quaternion.identity);
            _enemyHP--;
        }
        //������G�t�F�N�g�������āA������������j�󂵂���A���g��HP�����炷�B
        if (collision.gameObject.tag == ("Bullet"))
        {
            Instantiate(_bakuhatu, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _enemyHP = _enemyHP - 2;
        }
    }

    public void Money()
    {
        int ram = Random.Range(0, 100);
        if(ram > _moneycount[1])
        {
            InsMoney(2);
        }
        else if(ram > _moneycount[0])
        {
            InsMoney(1);
        }
        else
        {
            InsMoney(0);
        }
    }

    void InsMoney(int i)
    {
        Instantiate(_money[i], transform.position, Quaternion.identity);
    }

    public void Damage()
    {
        Instantiate(_bakuhatu, transform.position, Quaternion.identity);
        _enemyHP = _enemyHP - 2;
    }
}
