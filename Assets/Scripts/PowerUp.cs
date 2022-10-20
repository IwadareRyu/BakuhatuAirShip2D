using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : SingletonMonovihair<PowerUp>
{
    public int _bakuhatuPower = 1;
    public int _speedUp = 1;
    public int _airnum = 1;
    private GameManager GM;

    protected override bool _dontDestroyOnLoad { get { return true; } }

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    /// <summary>�����̈З͂��A�b�v������֐�</summary>
    public void BakuhatuUp()
    {
        if (GM._totalMoney >= _bakuhatuPower * 1000)
        {
            GM.TotalMoney(-_bakuhatuPower * 1000);
            _bakuhatuPower++;
        }
    }
    /// <summary>���̃X�s�[�h���A�b�v������֐�</summary>
    public void SpeedUp()
    {
        if (GM._totalMoney >= _speedUp * 100)
        {
            GM.TotalMoney(-_speedUp * 100);
            _speedUp++;
        }
    }
    /// <summary>��x�ɏo���@�̂𑝂₷�֐�</summary>
    public void AirNum()
    {
        if (GM._totalMoney >= _airnum * 10000)
        {
            GM.TotalMoney(-_airnum * 10000);
            _airnum++;
        }
    }
}
