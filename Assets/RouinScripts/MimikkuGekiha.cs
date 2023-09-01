using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MimikkuGekiha : MonoBehaviour
{
    GameObject _player;
    Vector2 _playerTrams;
    float minas;
    Rigidbody2D _rb;
    [SerializeField]float _power = 5f;
    [SerializeField] GameObject _audio;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        //コルーチン
        StartCoroutine("GekihaTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //プレイヤーの位置と自分の位置を計算して、向きを変える。
        _playerTrams = (_player.transform.position - transform.position).normalized;
        FlipX(_playerTrams);
    }
    void FlipX(Vector2 horizontal)
    {
        //向き
        if (horizontal.x < 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            minas = -1;
        }
        else if (horizontal.x > 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            minas = 1;
        }
    }
    /// <summary>一定の時間がたった後に、斜め上に力を加え、音を出す。</summary>
    /// <returns></returns>
    IEnumerator GekihaTime()
    {
        yield return new WaitForSeconds(3.5f);
        _rb.AddForce(Vector2.up * _power, ForceMode2D.Impulse);
        _rb.AddForce(Vector2.left * _power * minas, ForceMode2D.Impulse);
        _audio.SetActive(true);
    }
}
