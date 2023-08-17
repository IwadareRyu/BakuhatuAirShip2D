using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HebiDamaCopy : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    Rigidbody2D rb;
    BoundController _muki;
    private GameObject Y;
    // Start is called before the first frame update
    void Start()
    {
        //ヘビのコピー。プレイヤーの向き判定しているところ以外はHebiDama.csとやっていることは一緒。
        rb = GetComponent<Rigidbody2D>();
        _muki = GameObject.FindWithTag("Player").GetComponent<BoundController>();
        Y = GameObject.FindGameObjectWithTag("Hebi");
        rb.velocity = Vector2.right * _speed * _muki._minas;
        transform.localScale = new Vector3(_muki._minas, 1, 1);
        float sin = Y.transform.position.y + Mathf.Sin(Time.time * 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //ヘビのコピー。
        Y = GameObject.FindGameObjectWithTag("Hebi");
        float sin = Y.transform.position.y +  Mathf.Sin(Time.time * 10);
        Vector3 pos = transform.position;
        pos.y = sin;
        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}