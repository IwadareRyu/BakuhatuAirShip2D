using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIns : MonoBehaviour
{
    //敵の生成位置
    Vector2 _pos;
    float _x;
    //生成する敵のプレハブ
    [SerializeField] GameObject _pig;
    [Header("敵の生成間隔")]
    [SerializeField] float _insSec;
    //敵のY座標の上限と下限
    [SerializeField] float _high = 4f;
    [SerializeField] float _low = -4f;
    //時間経過カウント
    float _time = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //時間経過の計算
        _time += Time.deltaTime;
        if (_time > _insSec)
        {
            _x = Random.Range(_low, _high);
            //敵の生成位置の設定
            _pos = new Vector2(_x, this.transform.position.y);
            Instantiate(_pig, _pos, Quaternion.identity);
            //時間経過のカウントリセット
            _time = 0;
        }
    }
    public void InsSecChange(float num)
    {
        //敵の生成間隔を新しい値に変更
        _insSec = num;
    }

}
