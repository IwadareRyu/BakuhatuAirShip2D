using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CopyBase : MonoBehaviour
{
    [Tooltip("パリィしたときに音を鳴らす")]
    [SerializeField] AudioClip _sound;
    [Tooltip("アイテムかコピーか")]
    [SerializeField] Item _whatitem = Item.Copy;
    [SerializeField] GameObject _player;
    [Tooltip("ホーミングする球の際、対象物をホーミングするかしないか")]
    public bool notHoming = false;
    [SerializeField] GameObject _hit;
    private GameManager GM;

    /// <summary>継承</summary>
    public abstract void CopyTech();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        //当たり判定にあたると
        if(collision.gameObject.tag.Equals("Atari"))
        {
            //サウンドが呼び出され
            if(_sound)
            {
                AudioSource.PlayClipAtPoint(_sound, Camera.main.transform.position);
            }
            //列挙型がitemだと、その場でCopytechを呼び出す。
            if(_whatitem == Item.Copy)
            {
                CopyTech();
                Destroy(this.gameObject);
            }
        }
        //ホーミングに対応していない壁や地面に当たると破壊する。
        else if(collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        //ホーミングをやめるブロック
        if(collision.gameObject.tag == "HomingBlock")
        {
            notHoming = true;
        }
        //プレイヤーに当たるかつ無敵時間じゃないとダメージを受けて、ホーミングを止め、無敵時間の開始。
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(_hit, collision.transform.position, Quaternion.identity);
            notHoming = true;
        }
    }

    enum Item
    {
        /// <summary>コピーした時 </summary>
        Copy,
        /// <summary>アイテムを拾ったとき</summary>
        item,
    }
}
