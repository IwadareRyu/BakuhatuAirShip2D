using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    //HPを表示するスライダー
    [SerializeField] Slider _hpslider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary> HPバーの値を設定するメソッド </summary>
    void Update()
    {
        
    }
    public void HPSlider(float hp)
    {
        //HPの値を更新してHPバーを更新する
        _hpslider.value = hp;
    }
}
