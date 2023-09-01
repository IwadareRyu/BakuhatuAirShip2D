using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    [SerializeField]float m_deadtime;
    // Start is called before the first frame update
    void Start()
    {
        //ˆê’è‚ÌŠÔ‚ªŒo‚Á‚½‚ç©g‚ğ”j‰óB
        Destroy(this.gameObject, m_deadtime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
