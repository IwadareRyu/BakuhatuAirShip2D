using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimikkuTouzyou : MonoBehaviour
{
    [SerializeField] GameObject Mimi;
    Mimikku Mimikku;
    // Start is called before the first frame update
    void Start()
    {
        Mimikku = Mimi.GetComponent<Mimikku>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Atari")
        {
            Mimi.gameObject.SetActive(true);
            Mimikku._attackTime = true;
            gameObject.SetActive(false);
        }
    }
}
