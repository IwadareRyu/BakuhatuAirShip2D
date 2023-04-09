using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : SingletonMonovihair<PowerUp>
{
    public int _bakuhatuPower = 1;
    public int _speedUp = 1;
    public int _airnum = 1;
    private GameManager GM;
    [SerializeField] Text _powerText;
    [SerializeField] Text _speedText;
    [SerializeField] Text _airNumText;
    [SerializeField] Text _powerMoneyText;
    [SerializeField] Text _speedMoneyText;
    [SerializeField] Text _airnumMoneyText;
    string _changeScene;

    protected override bool _dontDestroyOnLoad { get { return true; } }

    public void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        AllTextShow();
    }

    public void AllTextShow()
    {
        TextShow(_powerText, _bakuhatuPower);
        TextShow(_speedText, _speedUp);
        TextShow(_airNumText, _airnum);
        TextShow(_powerMoneyText, _bakuhatuPower * 1000);
        TextShow(_speedMoneyText, _speedUp * 100);
        TextShow(_airnumMoneyText, _airnum * 10000);
    }

    void TextShow(Text text, int up)
    {
        if(text)
        {
            text.text = up.ToString();
        }
    }
    /// <summary>爆発の威力をアップさせる関数</summary>
    public void BakuhatuUp()
    {
        if (GM._totalMoney >= _bakuhatuPower * 500)
        {
            GM.TotalMoney(-_bakuhatuPower * 500);
            _bakuhatuPower++;
            AllTextShow();
        }
    }
    /// <summary>球のスピードをアップさせる関数</summary>
    public void SpeedUp()
    {
        if (GM._totalMoney >= _speedUp * 50)
        {
            GM.TotalMoney(-_speedUp * 50);
            _speedUp++;
            AllTextShow();
        }
    }
    /// <summary>一度に出す機体を増やす関数</summary>
    public void AirNum()
    {
        if (GM._totalMoney >= _airnum * 5000)
        {
            GM.TotalMoney(-_airnum * 5000);
            _airnum++;
            AllTextShow();
        }
    }
    public void BuildUp()
    {
        GameObject _build = transform.GetChild(0).gameObject;
        _build.SetActive(true);
    }

    public void SceneName(string scene)
    {
        _changeScene = scene;
    }

    public void SceneLoad()
    {
        SceneLoader.Instance.SceneLoad(_changeScene);
    }
}
