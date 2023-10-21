using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    SceneLoaderAditiveClass _sceneLoaderAditiveClass;
    
    [SerializeField]
    string _nobelSceneName;
    
    [SerializeField] 
    int _deadEnemy = 100;
    
    [SerializeField] 
    Transform _goalPos;
    
    [SerializeField] 
    float _speed = 1f;
    
    [SerializeField] 
    GameObject _clearText;
    
    float _stopdis = 0.5f;
    
    [SerializeField] 
    float _countDownTime = 60f;
    
    bool _goalbool;
    
    [SerializeField] 
    RandomManege _enemymanege;
    
    bool _pause;
    
    [SerializeField] 
    AudioClip _audio;

    [SerializeField]
    Canvas _uI;

    private void OnEnable()
    {
        PauseManager.OnPauseResume += StartPause;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= StartPause;
    }

    private void Awake()
    {
        _uI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<Canvas>();
    }

    void Start()
    {
        GameManager.Instance.SetDeadEnemy(_deadEnemy);
        GameManager.Instance.SetCountDown(_countDownTime);
        _clearText.SetActive(false);
    }

    void Update()
    {
        if (!_pause)
        {
            if (GameManager.Instance._enemy == 0)
            {
                float distance = Vector2.Distance(transform.position, _goalPos.position);
                if (distance > _stopdis)
                {
                    Vector3 dir = (_goalPos.position - transform.position).normalized * _speed;
                    transform.Translate(dir * Time.deltaTime);
                }
                if (!_goalbool)
                {
                    _goalbool = true;
                    if (_enemymanege)
                    {
                        _enemymanege.ResetEnemy();
                    }
                    var bullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
                    if (bullet.Length != 0)
                    {
                        foreach (var i in bullet)
                        {
                            i.SetActive(false);
                        }
                    }
                    var enemy = GameObject.FindGameObjectsWithTag("Enemy");
                    if (enemy.Length != 0)
                    {
                        foreach (var i in enemy)
                        {
                            i.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    //trigger,collison関わらず、プレイヤーに当たったらイベントが起こる。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_pause)
        {
            PauseManager.PauseResume();
            if (_nobelSceneName != "")
            {
                if (_uI.enabled) { _uI.enabled = false; }
                _sceneLoaderAditiveClass = new SceneLoaderAditiveClass();
                _sceneLoaderAditiveClass.SceneLoaderAditive(_nobelSceneName);
            }
            else
            {
                IsEndMethod();
            }
        }
    }

    public void IsEndMethod()
    {
        StartCoroutine(AudioPlay());
        _clearText.SetActive(true);
        if (!_uI.enabled) { _uI.enabled = true; }
    }

    IEnumerator AudioPlay()
    {
        BGMManager.Instance.BGMStop();
        BGMManager.Instance?.SEPlay(SE.Clear);       
        yield return new WaitForSeconds(5f);
        BGMManager.Instance.ClipBGMPlay(_audio);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _clearText.SetActive(true);
            //_action.Invoke();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void StartPause(bool pause)
    {
        _pause = pause;
    }
}
