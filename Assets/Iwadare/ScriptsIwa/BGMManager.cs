using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BGMManager;

/// <summary>BGMとSEを管理するマネージャークラス</summary>
public class BGMManager : SingletonMonovihair<BGMManager>
{
    [Tooltip("BGMの音量")]
    float _bgmVolume = -10f;
    public float BgmVolume => _bgmVolume;

    [Tooltip("SEの音量")]
    float _seVolume = 0f;
    public float SeVolume => _seVolume;

    
    [SerializeField,Header("BGMのオーディオ一覧"),
        Tooltip("BGMのオーディオ一覧(スクリプトでは使っていないがなんのBGMがあるか確認用)")] 
    AudioClip[] _bgmClip;

    
    [SerializeField,Tooltip("SEのオーディオソース一覧"),Header("SEのオーディオソース一覧")] 
    AudioSource[] _sESource;
    public AudioSource[] SEAudio => _sESource;
    
    [SerializeField,Tooltip("SEのEnum(列挙)型")] 
    SE _sEEnum;

    
    [SerializeField,Header("BGMのオーディオソース"),Tooltip("BGMのオーディオソース")] 
    AudioSource _bgm;
 
    protected override bool _dontDestroyOnLoad { get { return true; } }

    /// <summary>SEを再生するメソッド</summary>
    /// <param name="seEnum">seのEnum型</param>
    public void SEPlay(SE seEnum)
    {
        // それぞれのSEに対応するオーディオソースを再生
        // もし、新しいSEを追加した場合は、ここに条件分岐を追加する。
        if (seEnum == SE.Explosion)
        {
            SEAudioPlay((int)SE.Explosion);
        }
        else if (seEnum == SE.SmallFire)
        {
            SEAudioPlay((int)SE.SmallFire, 0.5f);
        }
        else if (seEnum == SE.EnemyShot)
        {
            SEAudioPlay((int)SE.EnemyShot, 0.5f);
        }
        else if (seEnum == SE.Clear)
        {
            SEAudioPlay((int)SE.Clear, 0.5f);
        }
        else if (seEnum == SE.Click)
        {
            SEAudioPlay((int)SE.Click, 0.5f);
        }
        else if (seEnum == SE.PowerUp)
        {
            SEAudioPlay((int)SE.PowerUp, 0.5f);
        }
        else if (seEnum == SE.Lose)
        {
            SEAudioPlay((int)SE.Lose);
        }
        else if (seEnum == SE.FireBreath)
        {
            SEAudioPlay((int)SE.FireBreath, 0.5f);
        }
        else if (seEnum == SE.Coin)
        {
            SEAudioPlay((int)SE.Coin, 0.5f);
        }
        else if (seEnum == SE.PlayerShot)
        {
            SEAudioPlay((int)SE.PlayerShot, 0.5f);
        }

    }

    /// <summary>SEを再生するメソッド</summary>
    /// <param name="enumNumber">列挙型の番号</param>
    /// <param name="audioVolume">音量</param>
    void SEAudioPlay(int enumNumber, float audioVolume = 1f)
    {
        // 指定されたオーディオソースのボリュームを設定し再生
        _sESource[enumNumber].volume = audioVolume;
        _sESource[enumNumber].Play();
    }

    /// <summary>BGMを再生するメソッドです。</summary>
    /// <param name="audioClip">指定されたオーディオクリップ</param>
    public void ClipBGMPlay(AudioClip audioClip)
    {
        // 指定されたオーディオクリップをオーディオソースに設定し再生
        _bgm.clip = audioClip;
        _bgm.Play();
    }

    public void StateBGMPlay(NobelBGM bgm)
    {
        _bgm.clip = _bgmClip[(int)bgm];
        _bgm.Play();
    }

    /// <summary>BGMを停止するメソッド</summary>
    public void BGMStop()
    {
        _bgm.Stop();
    }

    /// <summary>BGMの音量を設定するメソッド</summary>
    /// <param name="audioVolume">音量</param>
    public void BGMValue(float audioVolume)
    {
        _bgmVolume = audioVolume;
    }

    /// <summary>SEの音量を設定するメソッド</summary>
    /// <param name="audioVolume">音量</param>
    public void SEValue(float audioVolume)
    {
        _seVolume = audioVolume;
    }

    private void OnLevelWasLoaded(int level)
    {
        BGMStop();
    }
}

/// <summary>SEの種類を列挙した列挙型</summary>
public enum SE
{
    Explosion,
    SmallFire,
    EnemyShot,
    Clear,
    Click,
    PowerUp,
    Lose,
    FireBreath,
    Coin,
    PlayerShot,
}

/// <summary>ノベルで使うBGMの種類を列挙した列挙型</summary>
public enum NobelBGM
{
    Title,
    Field1,
    Field2,
    Boss1,
    Boss2,
    Clear,
}