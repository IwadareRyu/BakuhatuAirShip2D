using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : SingletonMonovihair<BGMManager>
{
    [Tooltip("BGM‚Ì‰¹—Ê")]
    float _bgmVal = -10f;
    public float BgmVal => _bgmVal;
    [Tooltip("SE‚Ì‰¹—Ê")]
    float _seVal = 0f;
    public float SeVal => _seVal;
    [SerializeField]AudioClip[] _bGMClip;
    public AudioClip[] BGMClip => _bGMClip;
    [SerializeField] AudioSource[] _sEAudio;
    public AudioSource[] SEAudio => _sEAudio;
    [SerializeField] SE _se;
    [SerializeField]AudioSource _bGM;
    protected override bool _dontDestroyOnLoad { get { return true; } }

    public void SEPlay(SE se)
    {
        if(se == SE.Bakuhatu)
        {
            SEAudioPlay((int)SE.Bakuhatu);
        }
        else if (se == SE.SmallFire)
        {
            SEAudioPlay((int)SE.SmallFire, 0.5f);
        }
        else if (se == SE.EnemyShot)
        {
            SEAudioPlay((int)SE.EnemyShot, 0.5f);
        }
        else if (se == SE.Clear)
        {
            SEAudioPlay((int)SE.Clear, 0.5f);
        }
        else if(se == SE.Click)
        {
            SEAudioPlay((int)SE.Click, 0.5f);
        }
        else if(se == SE.PowerUp)
        {
            SEAudioPlay((int)SE.PowerUp, 0.5f);
        }
        else if(se == SE.Lose)
        {
            SEAudioPlay((int)SE.Lose);
        }
        else if (se == SE.BigFire)
        {
            SEAudioPlay((int)SE.BigFire, 0.5f);
        }
        else if(se == SE.Coin)
        {
            SEAudioPlay((int)SE.Coin, 0.5f);
        }
        else if (se == SE.PlayerShot)
        {
            SEAudioPlay((int)SE.PlayerShot, 0.5f);
        }

    }

    void SEAudioPlay(int num,float volume = 1f)
    {
        _sEAudio[num].volume = volume;
        _sEAudio[num].Play();
    }

    public void BGMPlay(AudioClip audio)
    {
        _bGM.clip = audio;
        _bGM.Play();
    }

    public void BGMStop()
    {
        _bGM.Stop();
    }

    public void BGMValue(float value)
    {
        _bgmVal = value;
    }

    public void SEValue(float value)
    {
        _seVal = value;
    }

    public enum SE
    {
        Bakuhatu,
        SmallFire,
        EnemyShot,
        Clear,
        Click,
        PowerUp,
        Lose,
        BigFire,
        Coin,
        PlayerShot,
    }
}

