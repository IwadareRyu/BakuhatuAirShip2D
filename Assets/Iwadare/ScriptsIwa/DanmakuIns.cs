using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmakuIns : MonoBehaviour
{
    [Tooltip(" 弾の発射間隔")]
    [SerializeField] 
    float _count = 5;

    [Tooltip("弾の発射間隔(_countと別の使い方すればよかったなという感じ)")]
    [SerializeField] 
    float _angleCount = 1f;

    [Tooltip("使用する弾のプール")]
    [SerializeField]
    BulletPoolActive _pool;

    [Tooltip("弾幕の発射フラグ")]
    bool _bulletTime;

    [Tooltip("弾のスクリプト")]
    ActiveBullet bulletCs;

    [Header("球と球の間隔を入力。"),Tooltip("球同士の間隔")]
    [SerializeField]
    int _num = 20;

    [Tooltip("弾の速度")]
    [SerializeField]
    float _bulletspeed = 1f;

    [Tooltip("波弾幕の切り替えフラグ")]
    bool _interval;

    [Tooltip("弾幕の発射角度")]
    [SerializeField] 
    float _angle = 0f;

    [Tooltip("弾のリスト")]
    List<GameObject> _bulletList = new List<GameObject>();

    [Header("DanmakuStateがRotateの場合、ColorStateを入力してください。"),Tooltip("弾幕のタイプ")]
    [SerializeField] 
    BulletTypeClass.BulletState _danmakuState;

    [Tooltip("弾のタイプ")]
    [SerializeField]
    BulletTypeClass.BulletSpriteState _colorState;

    [Tooltip("弾の軌道が一度変わる弾幕のタイプの時に自機狙いの球にするかどうかのフラグ")]
    [SerializeField] 
    bool _zikiMode;

    [Tooltip("モグラかどうかのフラグ")]
    [SerializeField]
    bool _mogura;

    [Tooltip("ポーズ")]
    bool _pause;

    private void Start()
    {
        if (!_pool)
        {
            _pool = GameObject.FindGameObjectWithTag("MidBulletPool").GetComponent<BulletPoolActive>();
        }   //プール参照
    }

    private void OnEnable()
    {
        _bulletTime = false;
        PauseManager.OnPauseResume += OnStartPause;
    }   // ポーズ

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // ポーズ解除

    void Update()
    {
        // ポーズしていない場合にのみ弾幕を実行
        if (!_pause)
        {
            if (!_bulletTime)
            {
                _bulletTime = true;
                StartCoroutine(BulletTime());
            }   //フラグがfalseになったら弾幕の実行。
        }
    }

    /// <summary>球の動きや数、パターンを決めるメソッド</summary>
    /// <returns></returns>
    IEnumerator BulletTime()
    {
        // 弾幕のタイプがNamiの場合
        if (_danmakuState == BulletTypeClass.BulletState.Nami)
        {
            StartCoroutine(NamiBullet());
        }
        // 弾幕のタイプがZiki（単発自機狙い）の場合
        else if (_danmakuState == BulletTypeClass.BulletState.Ziki)
        {
            StartCoroutine(SingleZikiBullet());
        }
        // 弾幕のタイプがAllZiki（複数自機狙い）の場合
        else if (_danmakuState == BulletTypeClass.BulletState.AllZiki)
        {
            StartCoroutine(MaltiZikiBullet());
        }
        // 弾幕のタイプがStopBulletの場合
        else if (_danmakuState == BulletTypeClass.BulletState.StopBullet)
        {
            StartCoroutine(StopBullet());
        }
        // 弾幕のタイプがIllusionBullet（分裂弾）の場合
        else if (_danmakuState == BulletTypeClass.BulletState.IllusionBullet)
        {
            StartCoroutine(IllusionBullet());
        }
        // その他のタイプの場合
        else
        {
            //複数の弾の生成の実行
            AllBulletIns(0);
            if (_mogura)
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
            }
            else
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
            }   //弾の音の再生
            yield return new WaitForSeconds(_count);
            _bulletTime = false;
        }
        yield return null;
    }

    /// <summary>NamiBulletの場合の処理</summary>
    IEnumerator NamiBullet()
    {
        //弾のタイプの変更
        _colorState = BulletTypeClass.BulletSpriteState.RedFire;

        //インターバルの切り替えで交互に波の弾幕を生成。
        if (!_interval)
        {
            for (var i = 0; i <= 15; i += 5)
            {
                //複数の弾の生成
                AllBulletIns(i); 
                if (_mogura)
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                }
                else
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                }   //SEの再生処理(モグラとドラゴンで音声を分ける。)
                yield return new WaitForSeconds(_count);
            }
            _interval = true;
        }
        else
        {
            for (var i = 15; i >= 0; i -= 5)
            {
                //複数の弾の生成
                AllBulletIns(i);
                if (_mogura)
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                }
                else
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                }   //SEの再生
                yield return new WaitForSeconds(_count);
            }
            yield return new WaitForSeconds(0.5f);
            _interval = false;
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>単発の自機狙い弾の場合</summary>
    IEnumerator SingleZikiBullet()
    {

        _colorState = BulletTypeClass.BulletSpriteState.BlueFire;
        //単発の弾生成
        OnePointIns();
        if (_mogura)
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
        }
        else
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }


    /// <summary>複数の自機狙い弾の場合</summary>
    IEnumerator MaltiZikiBullet()
    {
        _colorState = BulletTypeClass.BulletSpriteState.BlueFire;
        for (var j = 0; j < 5; j++)
        {
            yield return new WaitForSeconds(0.1f);
            //for文でループして自機狙いの球をたくさん出す。
            for (var k = 0; k < 5; k++)
            {
                yield return new WaitForSeconds(0.01f);
                //単発の弾の生成
                OnePointIns(); 
                if (_mogura)
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                }
                else
                {
                    BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                }
            }
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>StopBullet(一時停止する弾)の処理</summary>
    IEnumerator StopBullet()
    {
        //途中で止まる弾を飛ばして、再度弾を動かせるように弾のリストを作っておく。
        for (var i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(_angleCount);
            //単発弾生成(リストを使って生成した弾のリストを作る、)
            OnePointIns(true, _angle, true);
            if (_mogura)
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
            }
            else
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
            }   //SE再生
        }
        yield return new WaitForSeconds(_angleCount);
        var num = _bulletList.Count;

        //ActiveBulletのスクリプトで再度弾を動かして、リストを空にする。
        for (var j = 0; j < num; j++)
        {
            bulletCs = _bulletList[0].GetComponent<ActiveBullet>();
            bulletCs.StopTrue();
            _bulletList.RemoveAt(0);
            if (_mogura)
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
            }
            else
            {
                BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
            }   //SE再生
        }
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>IllusionBullet(分裂弾)の処理</summary>
    IEnumerator IllusionBullet()
    {
        //180度に単発弾生成
        OnePointIns(false, 180, true);
        //0度に単発弾生成
        OnePointIns(false, 0, true);
        if (_mogura)
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
        }
        else
        {
            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
        }   //SE生成
        yield return new WaitForSeconds(_angleCount);
        if (_bulletList.Count != 0)
        {
            //分裂弾(一回目)
            for (var i = _bulletList.Count; i > 0; i--)
            {
                if (_bulletList[i - 1])
                {
                    if (_bulletList[i - 1].active)
                    {
                        AllBulletIns(0, true);
                        if (_mogura)
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                        }
                        else
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                        }
                        _bulletList.RemoveAt(i - 1);
                    }
                }
                else
                {
                    _bulletList.RemoveAt(i - 1);
                }
            }
        }
        yield return new WaitForSeconds(_angleCount);
        if (_bulletList.Count != 0)
        {
            //分裂弾(2回目)
            for (var i = _bulletList.Count; i > 0; i--)
            {
                if (_bulletList[i - 1])
                {
                    if (_bulletList[i - 1].active)
                    {
                        AllBulletIns(0, true);
                        if (_mogura)
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.EnemyShot);
                        }
                        else
                        {
                            BGMManager.Instance?.SEPlay(BGMManager.SE.SmallFire);
                        }
                        _bulletList.RemoveAt(i - 1);
                    }
                }
                else
                {
                    _bulletList.RemoveAt(i - 1);
                }
            }
        }
        _bulletList.Clear();
        yield return new WaitForSeconds(_count);
        _bulletTime = false;
    }

    /// <summary>365度均等に球を出すメソッド。</summary>
    /// <param name="rad">球の間隔</param>
    /// <param name="illusionPosBool">分裂弾の際、分裂するときの位置を変える際のフラグ</param>
    private void AllBulletIns(int rad, bool illusionPosBool = false)
    {
        //球一つずつに処理を加える。
        for (var i = rad; i < 365; i += _num)
        {
            //球をpoolから取り出す。
            var bullet = _pool.GetBullet();
            bulletCs = bullet.GetComponent<ActiveBullet>();
            //球の位置をスポーンポイントに移動させる。
            if (!illusionPosBool)
            {
                bullet.transform.position = transform.position;
            }
            else if (_bulletList.Count != 0)
            {
                bullet.transform.position = _bulletList[0].transform.position;
            }
            //一回弾の動きを変えるときの処理
            if (_danmakuState == BulletTypeClass.BulletState.ChangeBulletOne)
            {
                if (_zikiMode)
                {
                    bulletCs.BulletAdd(i, _bulletspeed, _colorState, false, true, false, true);
                }
                else
                {
                    bulletCs.BulletAdd(i, _bulletspeed, _colorState, false, true);
                }
            }
            //２回動きを変えるときの処理
            else if (_danmakuState == BulletTypeClass.BulletState.ChangeBulletTwo)
            {
                bulletCs.BulletAdd(i, _bulletspeed, _colorState, false, true, true);
            }
            //それ以外の時の処理
            else
            {
                bulletCs.BulletAdd(i, _bulletspeed, _colorState);
            }
            //illusionbulletの時、リストに追加。
            if (illusionPosBool)
            {
                _bulletList.Add(bullet);
            }
        }

        //生成し終わった分裂弾をリストから削除。
        if (illusionPosBool)
        {
            _bulletList.RemoveAt(0);
        }
    }

    /// <summary>単発で弾を出す処理</summary>
    /// <param name="stop">弾を止めるかどうかのフラグ</param>
    /// <param name="angle">弾を飛ばす角度</param>
    /// <param name="listbool">リストに追加するかのフラグ</param>
    private void OnePointIns(bool stop = false, float angle = 0, bool listbool = false)
    {
        var bullet = _pool.GetBullet();
        bulletCs = bullet.GetComponent<ActiveBullet>();
        bullet.transform.position = transform.position;
        if (listbool)
        {
            _bulletList.Add(bullet);
        }
        bulletCs.BulletAdd(angle, _bulletspeed, _colorState, stop);
    }

    /// <summary>ポーズ処理</summary>
    /// <param name="pause">ポーズフラグ</param>
    public virtual void OnStartPause(bool pause)
    {
        _pause = pause;
    }
}