using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipController : MonoBehaviour
{
    Rigidbody2D _rb;
    [Tooltip("�U���̃N�[���^�C��")]
    bool _cooltime;
    [Tooltip("��΂���s�@")]
    [SerializeField] GameObject _airShip;
    [Tooltip("��΂���s�@�̏����ʒu")]
    [SerializeField] GameObject _airShipMazzle;
    [Tooltip("�v���C���[�ɕt���Ă����s�@�̃I���I�t")]
    [SerializeField] GameObject _airShipOnOff;
    [Tooltip("�E�������Ă�����+1�A���������Ă�����-1�B")]
    float minas = 1;
    float h;
    float v;
    [Tooltip("�v���C���[�̃X�s�[�h")]
    [SerializeField] float Speed = 3f;
    public float _minas => minas;
    [Tooltip("��s�@���Đ�������鎞��")]
    [SerializeField] float _time = 3f;
    GameManager _gm;
    [Tooltip("���g���C���邽�߂̕ϐ��B")]
    SceneLoader _activeLoad;

    // Start is called before the first frame update
    void Start()
    {
        //���ꂼ��̗v�f��GetConponent����B
        _rb = GetComponent<Rigidbody2D>();
        //AttackAni =GetComponent<Animator>();
        //_gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        //_activeLoad = GameObject.FindGameObjectWithTag("GM").GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_gm._gameover)
        //{
        //�v���C���[�̍��E�̓����B
        h = Input.GetAxisRaw("Horizontal");
        //�v���C���[�̏㉺�̓����B
        v = Input.GetAxisRaw("Vertical");

        //��s�@���΂��B
        if (Input.GetButton("Fire1") && !_cooltime)
        {
            Instantiate(_airShip, _airShipMazzle.transform.position, Quaternion.identity);
            StartCoroutine(CoolTime());
        }

        ////���g���C
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    _activeLoad.ActiveSceneLoad();
        //}
        //else
        //{
        //    h = 0;
        //    v = 0;
        //}
    }
    private void FixedUpdate()
    {
        //�㉺���E�ɓ��͂��ꂽ�Ƃ��̓����̌v�Z�B
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * Speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            if (!_cooltime)
            {
                StartCoroutine(CoolTime());
            }
        }
    }
    IEnumerator CoolTime()
    {
        _airShipOnOff.SetActive(false);
        _cooltime = true;
        yield return new WaitForSeconds(_time);
        _airShipOnOff.SetActive(true);
        _cooltime = false;
    }
}
