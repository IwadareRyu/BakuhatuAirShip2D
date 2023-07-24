using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _bGMSlider;
    [SerializeField] Slider _sESlider;

    private void Start()
    {
        _bGMSlider.value = BGMManager.Instance.BgmVal;
        _sESlider.value = BGMManager.Instance.SeVal;
    }
    public void SetMaster(float volume)
    {
        _audioMixer.SetFloat("MasterVol", volume);

    }

    public void SetBGM()
    {
        _audioMixer.SetFloat("BGMVol", _bGMSlider.value);
        BGMManager.Instance.BGMValue(_bGMSlider.value);
    }

    public void SetSE()
    {
        _audioMixer.SetFloat("SEVol", _sESlider.value);
        BGMManager.Instance.SEValue(_bGMSlider.value);
    }
}
