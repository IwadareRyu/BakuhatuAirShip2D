using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RouinDestroyEnamy : MonoBehaviour
{
    [SerializeField] int _score = 100;
    [SerializeField] int _enemyHP = 1;
    [SerializeField] GameObject _zangeki;
    [SerializeField] GameObject _hit;
    [SerializeField] GameObject _sceneLoad;
    [SerializeField] GameObject _mimikku;
    [SerializeField] GameObject _mimikkuGekiha;
    [SerializeField]bool _taiho;
    [SerializeField] bool _event;
    private RouinGameManager GM;
    [SerializeField] UnityEvent _action;
    // Start is called before the first frame update
    void Start()
    {
        //gameobject�̒��g���Ȃ��Ă��G���[���o���Ȃ�����
        Debug.LogWarning("�~�~�b�N�̏ꍇ�̓~�~�b�N�ƃ~�~�b�N���j�ɃR���|�[�l���g�����܂��傤�B");
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<RouinGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //�G��HP��0�ɂȂ�����
        if (_enemyHP <= 0)
        {
            //�C�x���g��bool��true�Ȃ�C�x���g���N����B
            if (_event)
            {
                _action.Invoke();
            }
            else
            {
                if (_mimikku && _mimikkuGekiha && _sceneLoad)
                {
                    Instantiate(_mimikkuGekiha, transform.position, Quaternion.identity);
                    _sceneLoad.SetActive(true);
                }
            }
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
            GM.AddScore(_score * 10);
            Destroy(collision.gameObject);
            Instantiate(_hit, transform.position, Quaternion.identity);
            _enemyHP = _enemyHP - 2;
        }
        if (collision.gameObject.tag == "Player" && !GM.star)
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            GM.AddLife(-5f);
            GM.StartCoroutine("StarTime");
        }
        if (collision.gameObject.tag == "MimikkuBullet" && !_taiho)
        {
            Instantiate(_hit, transform.position, Quaternion.identity);
            _enemyHP = _enemyHP - 2;
        }
    }
}