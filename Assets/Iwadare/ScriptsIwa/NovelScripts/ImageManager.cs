using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField]
    GameObject _backGroundObject;

    [SerializeField]
    Image _fadeBackGroundObject;

    [SerializeField]
    Color _fadeColor;

    [SerializeField]
    Sprite[] _backGroundSprites;

    [SerializeField]
    Image[] _charaImage;

    [SerializeField]
    Image[] _charaReactionImage;

    [SerializeField]
    Sprite[] _charaReactionSprite;

    [SerializeField]
    float _reactionTime = 0.5f;

    [SerializeField]
    Sprite[] _charaSprite;

    [SerializeField]
    Color _standCharaColor;
    int _changeSpriteNum = 0;

    Image _backGroundImage;

    Image _tmpImage;


    [SerializeField]
    Transform[] _charainPos;

    [SerializeField]
    Transform[] _charaOutPos;

    Tween[] _moveTween;


    // Start is called before the first frame update
    void Awake()
    {
        _backGroundImage = _backGroundObject.GetComponent<Image>();
        foreach (var i in _charaImage)
        {
            i.color = _standCharaColor;
        }
        // charaを表示させる数、Tweenを確保しておく。
        _moveTween = new Tween[_charaImage.Length];
    }

    private void Start()
    {
        _backGroundImage.sprite = _backGroundSprites[_changeSpriteNum];
        _fadeBackGroundObject.color = _fadeColor;
        foreach (var i in _charaReactionImage) { i.color = _fadeColor; }
    }

    /// <summary>背景を変更する処理</summary>
    public void ChangeBackImage()
    {
        _changeSpriteNum++;
        _fadeBackGroundObject.sprite = _backGroundSprites[_changeSpriteNum];
        _fadeBackGroundObject.DOFade(1f, 1f).OnComplete(() =>
        {
            _fadeBackGroundObject.color = _fadeColor;
            _backGroundImage.sprite = _backGroundSprites[_changeSpriteNum];
        });

    }

    /// <summary>キャラのイメージを変更する処理。</summary>
    /// <param name="i"></param>
    /// <param name="image"></param>
    public void CharaImage(int i, ImageState image)
    {
        if (i != -1)
        {
            if (_tmpImage)
            {
                _tmpImage.color = _standCharaColor;
            }
            _tmpImage = _charaImage[i];
            _tmpImage.sprite = _charaSprite[(int)image];
            _tmpImage.color = Color.white;
        }
        else
        {
            if (_tmpImage)
            {
                _tmpImage.color = _standCharaColor;
                _tmpImage = null;
            }
        }

    }

    /// <summary>キャラにリアクションをさせる処理</summary>
    /// <param name="chara"></param>
    /// <param name="reaction"></param>
    public void Reaction(int chara, int reaction)
    {
        var sequence = DOTween.Sequence();
        _charaReactionImage[chara].color = Color.white;
        _charaReactionImage[chara].sprite = _charaReactionSprite[reaction];
        var tmpscale = _charaReactionImage[chara].transform.localScale;
        var initialScale = tmpscale;
        initialScale.y = 0f;
        _charaReactionImage[chara].transform.localScale = initialScale;
        if ((ReactionState)reaction == ReactionState.Surprised)
        {
            var tmptrans = _charaReactionImage[chara].transform.position;
            sequence.Append(_charaReactionImage[chara].transform.DOScaleY(tmpscale.y, _reactionTime))
                .Join(_charaReactionImage[chara].transform.DOMoveY(tmptrans.y + 50, _reactionTime))
                .Append(_charaReactionImage[chara].transform.DOMoveY(tmptrans.y, _reactionTime)).SetLink(_charaReactionImage[chara].gameObject);
        }   // ビックリはぴょんと飛ぶイメージ
        else
        {
            sequence.Append(_charaReactionImage[chara].transform.DORotate(new Vector3(0, 0, 15), _reactionTime / 2f))
                .Join(_charaReactionImage[chara].transform.DOScaleY(tmpscale.y, _reactionTime / 2f))
                .Append(_charaReactionImage[chara].transform.DORotate(new Vector3(0, 0, -30), _reactionTime))
                .Append(_charaReactionImage[chara].transform.DORotate(new Vector3(0, 0, 15), _reactionTime / 2f)).SetLink(_charaReactionImage[chara].gameObject);
        }   //ハテナは首を左右に傾けるイメージ
        sequence.Play().OnComplete(() => { _charaReactionImage[chara].DOFade(0f, _reactionTime * 2f); }).SetLink(_charaReactionImage[chara].gameObject);
    }

    /// <summary>画面外に移動させる処理</summary>
    /// <param name="i"></param>
    public void CharaOut(int i)
    {
        if(_moveTween != null)
        {
            _moveTween[i].Complete();
        }   //キャラインが動いている際、強制的にキャラインを完了させる。

        _charaImage[i].transform.position = _charaOutPos[i].position;
    }   //画面外に移動させる。

    /// <summary>キャラが画面内に入る処理</summary>
    /// <param name="i"></param>
    public void CharaIn(int i)
    {
        _moveTween[i] = _charaImage[i].transform.DOMove(_charainPos[i].position, 0.5f)
            .OnComplete(() => _moveTween[i] = null);
    }   //キャラアウトする際、DoMoveが動いているとキャラアウトしないので、動いているTweenとして確保しておく。
}

public enum ReactionState
{
    Surprised,
    Question,
}
