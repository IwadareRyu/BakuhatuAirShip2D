using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    //HP��\������X���C�_�[
    [SerializeField] Slider _hpslider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary> HP�o�[�̒l��ݒ肷�郁�\�b�h </summary>
    void Update()
    {
        
    }
    public void HPSlider(float hp)
    {
        //HP�̒l���X�V����HP�o�[���X�V����
        _hpslider.value = hp;
    }
}
