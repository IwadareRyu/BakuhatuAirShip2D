using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextTime : MonoBehaviour
{
    [SerializeField] Text _sentence;
    [SerializeField] PigIns _pig;
    [SerializeField] float _sec;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeText());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator TimeText()
    {
        yield return new WaitForSeconds(5.0f);
        _sentence.text = "WASDキーで移動\nSHIFTキーで\n低速移動できるぞ！\n細かい移動に\n使ってくれ！";
        yield return new WaitForSeconds(10f);
        _sentence.text = "Fキーで\n自機を飛ばせるぞ！\n自機を飛ばすと\nお金が100yen減るぞ！";
        yield return new WaitForSeconds(5f);
        _sentence.text = "敵を倒すとお金が\n手に入る\nお金は自機の強化に\n使えるぞ！";
        yield return new WaitForSeconds(10f);
        _pig.InsSecChange(_sec);
        _sentence.text = "ダメージを受けると\n画面全体の射撃を\n打ち消す\nボムが出るぞ！！";
        yield return new WaitForSeconds(10f);
        _sentence.text = "上の赤いエリアに\n入るとお金が引き寄せるぞ！！";
        yield return new WaitForSeconds(10f);
        _pig.enabled = false;
        _sentence.text = "だがダメージを受けると\nお金が500yen減るので\n注意しろ！！";
        yield return new WaitForSeconds(10f);
        _sentence.text = "君のために\nお金2500yen\n用意したぞ！！\n機体数アップが\nおすすめだぞ！！";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddScore(2500);
        yield return new WaitForSeconds(5f);
        _sentence.text = "ゴールに入れば終了だ\n次はさっそくボス戦だ！！\nグットラック！";
        GameManager.Instance.SetDeadEnemy(-100);

    }
}
