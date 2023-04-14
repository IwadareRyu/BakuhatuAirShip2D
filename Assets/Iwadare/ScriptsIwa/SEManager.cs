using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : SingletonMonovihair<SEManager>
{
    AudioSource _audio;
    [SerializeField]AudioClip[] _clip;
    [SerializeField] SE _se;
    AudioSource _bGM;
    protected override bool _dontDestroyOnLoad { get { return true; } }

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SEPlay(SE se)
    {
        if(se == SE.Bakuhatu)
        {
            _audio.clip = _clip[0];
            _audio.volume = 1f;
            _audio.Play();
        }
        else if (se == SE.SmallFire)
        {
            _audio.clip = _clip[1];
            _audio.volume = 1f;
            _audio.Play();
        }
        else if (se == SE.Shot)
        {
            _audio.clip = _clip[2];
            _audio.volume = 0.5f;
            _audio.Play();
        }
        else if (se == SE.Clear)
        {
            _audio.clip = _clip[3];
            _audio.volume = 0.5f;
            _audio.Play();
        }
        else if(se == SE.Click)
        {
            _audio.clip = _clip[4];
            _audio.volume = 0.5f;
            _audio.Play();
        }
        else if(se == SE.PowerUp)
        {
            _audio.clip = _clip[5];
            _audio.volume = 0.5f;
            _audio.Play();
        }
        else if(se == SE.Lose)
        {
            _audio.clip = _clip[6];
            _audio.volume = 1f;
            _audio.Play();
        }


    }
    public void BGMPlay()
    {
        _bGM = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        if (_bGM)
        {
            _bGM.Play();
        }
    }

    public void BGMStop()
    {
        _bGM = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        if(_bGM)
        {
            _bGM.Stop();
        }
    }

    public enum SE
    {
        Bakuhatu,
        SmallFire,
        Shot,
        Clear,
        Click,
        PowerUp,
        Lose,
    }
}

