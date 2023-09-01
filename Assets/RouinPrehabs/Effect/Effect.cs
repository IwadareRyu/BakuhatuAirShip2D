using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // ///////////////////////
    /* エフェクトの自動消去 */
    // ///////////////////////

    [SerializeField] int lifeTime;
    int count;

    void Start()
    {
        
    }

    void Update()
    {
        count++;
        if (count > lifeTime) Destroy(gameObject);
    }
}
