using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HebiDama : CopyBase
{
    [SerializeField] GameObject _copyHebidama;
    private GameObject Mazzle;
    float _speed = 10f;
    Rigidbody2D rb;
    //当たり判定に当たったら特定のオブジェクト生成。
    public override void CopyTech()
    {
        Mazzle = GameObject.FindGameObjectWithTag("Atari");
        Instantiate(_copyHebidama, Mazzle.transform.position, Quaternion.identity);
    }
    private void Start()
    {
        //位置の初期化。
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * _speed;
        Mazzle = GameObject.FindGameObjectWithTag("Hebi");
        float sin = Mazzle.transform.position.y +  Mathf.Sin(Time.time);
    }
    private void FixedUpdate()
    {
        //sin1をTime.timeで動かして上下に移動。
        Mazzle = GameObject.FindGameObjectWithTag("Hebi");
        float sin = Mazzle.transform.position.y + Mathf.Sin(Time.time*10);
        Vector3 pos = transform.position;
        pos.y =sin;
        transform.position = pos;
    }
}