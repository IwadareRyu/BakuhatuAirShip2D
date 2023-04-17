using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour
{
    [SerializeField] AudioClip _bGMAudio;
    // Start is called before the first frame update
    void Start()
    {
        SEManager.Instance.BGMPlay(_bGMAudio);
    }
}
