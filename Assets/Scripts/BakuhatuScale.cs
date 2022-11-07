using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BakuhatuScale : MonoBehaviour
{
    PowerUp _power;
    // Start is called before the first frame update
    void Start()
    {
        _power = GameObject.FindGameObjectWithTag("UP").GetComponent<PowerUp>();
        transform.localScale = new Vector2(1.0f + _power._bakuhatuPower * 0.1f, 1.0f + _power._bakuhatuPower * 0.1f);
        //transform.DOScale(Vector3.one * (1.0f + _power._bakuhatuPower * 0.1f), 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 1.0f + _power._bakuhatuPower * 0.1f);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
}
