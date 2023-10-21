using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BGMManager;

/// <summary>BGM��SE���Ǘ�����}�l�[�W���[�N���X</summary>
public class BGMManager : SingletonMonovihair<BGMManager>
{
    [Tooltip("BGM�̉���")]
    float _bgmVolume = -10f;
    public float BgmVolume => _bgmVolume;

    [Tooltip("SE�̉���")]
    float _seVolume = 0f;
    public float SeVolume => _seVolume;

    
    [SerializeField,Header("BGM�̃I�[�f�B�I�ꗗ"),
        Tooltip("BGM�̃I�[�f�B�I�ꗗ(�X�N���v�g�ł͎g���Ă��Ȃ����Ȃ��BGM�����邩�m�F�p)")] 
    AudioClip[] _bgmClip;

    
    [SerializeField,Tooltip("SE�̃I�[�f�B�I�\�[�X�ꗗ"),Header("SE�̃I�[�f�B�I�\�[�X�ꗗ")] 
    AudioSource[] _sESource;
    public AudioSource[] SEAudio => _sESource;
    
    [SerializeField,Tooltip("SE��Enum(��)�^")] 
    SE _sEEnum;

    
    [SerializeField,Header("BGM�̃I�[�f�B�I�\�[�X"),Tooltip("BGM�̃I�[�f�B�I�\�[�X")] 
    AudioSource _bgm;
 
    protected override bool _dontDestroyOnLoad { get { return true; } }

    /// <summary>SE���Đ����郁�\�b�h</summary>
    /// <param name="seEnum">se��Enum�^</param>
    public void SEPlay(SE seEnum)
    {
        // ���ꂼ���SE�ɑΉ�����I�[�f�B�I�\�[�X���Đ�
        // �����A�V����SE��ǉ������ꍇ�́A�����ɏ��������ǉ�����B
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

    /// <summary>SE���Đ����郁�\�b�h</summary>
    /// <param name="enumNumber">�񋓌^�̔ԍ�</param>
    /// <param name="audioVolume">����</param>
    void SEAudioPlay(int enumNumber, float audioVolume = 1f)
    {
        // �w�肳�ꂽ�I�[�f�B�I�\�[�X�̃{�����[����ݒ肵�Đ�
        _sESource[enumNumber].volume = audioVolume;
        _sESource[enumNumber].Play();
    }

    /// <summary>BGM���Đ����郁�\�b�h�ł��B</summary>
    /// <param name="audioClip">�w�肳�ꂽ�I�[�f�B�I�N���b�v</param>
    public void ClipBGMPlay(AudioClip audioClip)
    {
        // �w�肳�ꂽ�I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɐݒ肵�Đ�
        _bgm.clip = audioClip;
        _bgm.Play();
    }

    public void StateBGMPlay(NobelBGM bgm)
    {
        _bgm.clip = _bgmClip[(int)bgm];
        _bgm.Play();
    }

    /// <summary>BGM���~���郁�\�b�h</summary>
    public void BGMStop()
    {
        _bgm.Stop();
    }

    /// <summary>BGM�̉��ʂ�ݒ肷�郁�\�b�h</summary>
    /// <param name="audioVolume">����</param>
    public void BGMValue(float audioVolume)
    {
        _bgmVolume = audioVolume;
    }

    /// <summary>SE�̉��ʂ�ݒ肷�郁�\�b�h</summary>
    /// <param name="audioVolume">����</param>
    public void SEValue(float audioVolume)
    {
        _seVolume = audioVolume;
    }

    private void OnLevelWasLoaded(int level)
    {
        BGMStop();
    }
}

/// <summary>SE�̎�ނ�񋓂����񋓌^</summary>
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

/// <summary>�m�x���Ŏg��BGM�̎�ނ�񋓂����񋓌^</summary>
public enum NobelBGM
{
    Title,
    Field1,
    Field2,
    Boss1,
    Boss2,
    Clear,
}