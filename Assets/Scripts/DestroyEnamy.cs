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
    [SerializeField] GameObject _mimikku;
    [SerializeField] GameObject _mimikkuGekiha;
    [SerializeField] GameObject _sceneLoad;
    private GameManager GM;
    [SerializeField]bool _taiho;
    [SerializeField] bool _event;
    [SerializeField] UnityEvent _action;
    // Start is called before the first frame update
    void Start()
    {
        //gameobjectの中身がなくてもエラーを出さない処理
        Debug.LogWarning("ミミックの場合はミミックとミミック撃破にコンポーネントをつけましょう。");
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //敵のHPが0になった時
        if(_enemyHP <= 0)
        {
            //ミミックじゃないなら破壊される。
            if (!_mimikku)
            {
                //イベントのboolがtrueならイベントが起こる。
                if (_event)
                {
                    _action.Invoke();
                }
                Destroy(this.gameObject);
            }
            else
            {
                //3つの中身があれば、ミミック撃破を生成して、シーンロードをtrueにして、自身を破壊する。
                if (_mimikku && _mimikkuGekiha && _sceneLoad)
                {
                    Instantiate(_mimikkuGekiha, transform.position, Quaternion.identity);
                    _sceneLoad.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //斬撃エフェクト生成して自身のHPを減らす。
        if(collision.gameObject.tag == ("Atari"))
        {
            Instantiate(_zangeki, transform.position, Quaternion.identity);
            _enemyHP--;
        }
        //当たりエフェクト生成して、当たった球を破壊した後、自身のHPを減らす。
        if (collision.gameObject.tag == ("Bullet"))
        {
            FindObjectOfType<GameManager>().AddScore(_score * 10);
            Destroy(collision.gameObject);
            Instantiate(_hit, transform.position, Quaternion.identity);
            _enemyHP = _enemyHP - 2;
        }
        //プレイヤーが当たったら、プレイヤーのHPを減らす。
        if(collision.gameObject.tag == "Player" && !GM.star)
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            GM.StartCoroutine("StarTime");
        }
        //貫通系の球
        if(collision.gameObject.tag == "MimikkuBullet" && !_taiho)
        {
            Instantiate(_hit, transform.position, Quaternion.identity);
            _enemyHP = _enemyHP - 2;
        }
    }
}
