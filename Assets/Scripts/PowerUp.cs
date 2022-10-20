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
    /// <summary>爆発の威力をアップさせる関数</summary>
    public void BakuhatuUp()
    {
        if (GM._totalMoney >= _bakuhatuPower * 1000)
        {
            GM.TotalMoney(-_bakuhatuPower * 1000);
            _bakuhatuPower++;
        }
    }
    /// <summary>球のスピードをアップさせる関数</summary>
    public void SpeedUp()
    {
        if (GM._totalMoney >= _speedUp * 100)
        {
            GM.TotalMoney(-_speedUp * 100);
            _speedUp++;
        }
    }
    /// <summary>一度に出す機体を増やす関数</summary>
    public void AirNum()
    {
        if (GM._totalMoney >= _airnum * 10000)
        {
            GM.TotalMoney(-_airnum * 10000);
            _airnum++;
        }
    }
}
