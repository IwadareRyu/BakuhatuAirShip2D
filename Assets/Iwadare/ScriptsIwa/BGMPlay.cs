using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour
{
    [SerializeField] AudioClip _bGMAudio;
    // Start is called before the first frame update
    private void Start()
    {
        Play();
    }
    public void Play()
    {
        BGMManager.Instance.ClipBGMPlay(_bGMAudio);
    }
}
