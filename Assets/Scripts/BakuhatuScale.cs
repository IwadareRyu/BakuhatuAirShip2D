using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BakuhatuScale : MonoBehaviour
{
    PowerUp _power;
    Vector2 cir;
    Vector2 start;
    // Start is called before the first frame update
    void Start()
    {
        _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
        transform.localScale = new Vector2(1.0f + _power._bakuhatuPower * 0.1f, 1.0f + _power._bakuhatuPower * 0.1f);
        //transform.DOScale(Vector3.one * (1.0f + _power._bakuhatuPower * 0.1f), 0.1f);
        cir = new Vector3(1.0f + _power._bakuhatuPower * 0.1f,0);
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(start,start + cir/2);
        var attacklange = Physics2D.OverlapCircleAll(start, (1.0f + _power._bakuhatuPower * 0.1f)/2);

        foreach(var a in attacklange)
        {
            if(a.gameObject.tag == "Enemy")
            {
                var dead = a.GetComponent<DestroyEnamy>();
                dead.Damage();
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
}
