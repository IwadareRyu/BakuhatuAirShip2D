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
        _sentence.text = "WASDキーで移動";
        yield return new WaitForSeconds(10f);
        _sentence.text = "Fキーで\n自機を飛ばせるぞ！\n自機を飛ばすと\n100円減るぞ！";
        yield return new WaitForSeconds(5f);
        _sentence.text = "敵を倒すとお金が\n手に入る\nお金は自機の強化に\n使えるぞ！";
        yield return new WaitForSeconds(10f);
        _pig.InsSecChange(_sec);
        _sentence.text = "ダメージを受けると\n画面全体の射撃を\n打ち消す\nボムが出るぞ！！";
        yield return new WaitForSeconds(10f);
        _pig.enabled = false;
        yield return new WaitForSeconds(1f);
        _sentence.text = "君のために\n25000円\n用意したぞ！！\n機体数アップが\nおすすめだぞ！！";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddScore(25000);
        yield return new WaitForSeconds(1f);
        _sentence.text = "ゴールに入れば終了だ\n次はさっそくボス戦だ！！\nグットラック！";
        GameManager.Instance.SetDeadEnemy(-100);

    }
}
