using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextTime : MonoBehaviour
{
    [SerializeField] Text _sentence;
    [SerializeField] PigIns _pig;
    [SerializeField] float _sec;
    bool _startBool = false;
    bool _textTimeBool = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _pig.enabled = false;
    }
    private void OnEnable()
    {
        PauseManager.OnPauseResume += OnStartPause;
    }   // ポーズ

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // ポーズ解除

    // Update is called once per frame
    void Update()
    {
        if(!_startBool && !_textTimeBool)
        {
            _textTimeBool = true;
            StartCoroutine(TimeText());
        }
    }
    IEnumerator TimeText()
    {
        yield return new WaitForSeconds(5.0f);
        _sentence.text = "WASDキーで移動\nSHIFTキーで\n低速移動できるぞ！\n細かい移動に\n使ってくれ！";
        _pig.enabled = true;
        yield return new WaitForSeconds(10f);
        _sentence.text = "Fキーで\n自機を飛ばせるぞ！\n自機を飛ばすと\nお金が100yen\n減るぞ！";
        yield return new WaitForSeconds(5f);
        _sentence.text = "敵を倒すとお金が\n手に入る\nお金は自機の強化に\n使えるぞ！";
        yield return new WaitForSeconds(10f);
        _pig.InsSecChange(_sec);
        _sentence.text = "ダメージを受けると\n画面全体の射撃を\n打ち消す\nボムが出るぞ！！";
        yield return new WaitForSeconds(10f);
        _sentence.text = "上の赤いエリアに\n入ると\nお金が引き寄せ\nられるぞ！！";
        yield return new WaitForSeconds(10f);
        _pig.enabled = false;
        _sentence.text = "だが\nダメージを受けると\nお金が500yen減る\nので注意しろ！！";
        yield return new WaitForSeconds(10f);
        _sentence.text = "君のために\nお金2500yen\n用意したぞ！！\n機体数アップが\nおすすめだぞ！！";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddScore(2500);
        yield return new WaitForSeconds(5f);
        _sentence.text = "ゴールに入れば\n終了だ!\n次はさっそく\nボス戦だ！！\nグットラック！";
        GameManager.Instance.SetDeadEnemy(-100);

    }

    void OnStartPause(bool pause)
    {
        _startBool = pause;
    }
}
