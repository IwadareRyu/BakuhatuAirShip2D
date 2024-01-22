using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : SingletonMonovihair<PowerUp>
{
    [Tooltip("爆発範囲")]
    [Range(1, 50)] public int _bakuhatuPower = 1;
    [Tooltip("弾のスピード")]
    [Range(1, 30)] public int _speedUp = 1;
    [Tooltip("一度に出せる弾の総数")]
    [Range(1, 8)] public int _airnum = 1;
    
    [Tooltip("GameManagerのインスタンス")]
    private GameManager GM;

    // UIテキストたちの参照
    [SerializeField] Text _powerText;
    [SerializeField] Text _speedText;
    [SerializeField] Text _airNumText;
    [SerializeField] Text _powerMoneyText;
    [SerializeField] Text _speedMoneyText;
    [SerializeField] Text _airnumMoneyText;
    [SerializeField] Canvas _powerUpCanvas;

    [Tooltip("シーン遷移先の名前を保持する変数")]
    string _changeScene;

    // DontDestroyOnLoad変数
    protected override bool _dontDestroyOnLoad { get { return true; } }


    public void Start()
    {
        // GameManagerのインスタンスを取得
        GM = GameManager.Instance;

        // UIテキストを更新する関数
        AllTextShow();

        _powerUpCanvas.enabled = false;
    }

    /// <summary>全てのUIテキストを更新する処理</summary>
    public void AllTextShow()
    {
        // 爆発の威力を表示
        if (_bakuhatuPower < 50)
        {
            TextShow(_powerText, _bakuhatuPower);
            TextShow(_powerMoneyText, _bakuhatuPower * 150);
        }
        else
        {
            TextShow(_powerText, _bakuhatuPower, true);
            TextShow(_powerMoneyText, _bakuhatuPower * 150, true);
        }

        // 弾のスピードを表示
        if (_speedUp < 30)
        {
            TextShow(_speedMoneyText, _speedUp * 50);
            TextShow(_speedText, _speedUp);
        }
        else
        {
            TextShow(_speedText, _speedUp, true);
            TextShow(_speedMoneyText, _speedUp * 50, true);
        }

        // 一度に出す弾の数を表示
        if (_airnum < 8)
        {
            TextShow(_airNumText, _airnum);
            TextShow(_airnumMoneyText, _airnum * 5000);
        }
        else
        {
            TextShow(_airNumText, _airnum, true);
            TextShow(_airnumMoneyText, _airnum * 5000, true);
        }
    }

    /// <summary>テキストを更新する処理</summary>
    /// <param name="text">表示するテキスト</param>
    /// <param name="up">表示内容</param>
    /// <param name="max">マックス時の処理</param>
    void TextShow(Text text, int up, bool max = false)
    {
        if (text && !max)
        {
            text.text = up.ToString();
        }
        else if (text)
        {
            text.text = "MAX!";
        }
    }

    /// <summary>爆発範囲をアップさせる処理</summary>
    public void BakuhatuUp()
    {
        if (GM._totalMoney >= _bakuhatuPower * 150 && _bakuhatuPower < 50)
        {
            BGMManager.Instance?.SEPlay(SE.PowerUp);
            GM.TotalMoney(-_bakuhatuPower * 150);
            _bakuhatuPower++;
            AllTextShow();
        }
    }

    /// <summary>弾のスピードをアップさせる処理</summary>
    public void SpeedUp()
    {
        if (GM._totalMoney >= _speedUp * 50 && _speedUp < 30)
        {
            BGMManager.Instance?.SEPlay(SE.PowerUp);
            GM.TotalMoney(-_speedUp * 50);
            _speedUp++;
            AllTextShow();
        }
    }

    /// <summary>一度に出す弾の数を増やす処理</summary>
    public void AirNum()
    {
        if (GM._totalMoney >= _airnum * 5000 && _airnum < 8)
        {
            BGMManager.Instance?.SEPlay(SE.PowerUp);
            GM.TotalMoney(-_airnum * 5000);
            _airnum++;
            AllTextShow();
        }
    }

    /// <summary>強化画面を表示する処理</summary>
    public void BuildUp()
    {
        _powerUpCanvas.enabled = true;
    }

    /// <summary>シーン遷移先の名前を設定する処理</summary>
    /// <param name="scene">シーンの名前</param>
    public void SceneName(string scene)
    {
        _changeScene = scene;
    }

    /// <summary>シーン遷移を行う処理</summary>
    public void SceneLoad()
    {
        SceneLoader.Instance.SceneLoad(_changeScene);
    }

    /// <summary>パワーアップの値をリセットする処理</summary>
    public void PowerReset()
    {
        _bakuhatuPower = 1;
        _airnum = 1;
        _speedUp = 1;
    }
}