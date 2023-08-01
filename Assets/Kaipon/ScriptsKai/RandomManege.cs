using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManege : MonoBehaviour
{
    bool _random;
    //�����Ԋu
    [SerializeField] float _insSec;
    //�I�u�W�F�N�g�̔z��
    [SerializeField] GameObject[] _pattern;
    //���ł���܂ł̎���
    [SerializeField] float _desSec;
    int _current = 0;
    bool _pause;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var i in _pattern)
        {
            i.SetActive(false);
        }
    }
    private void OnEnable()
    {
        PauseManager.OnPauseResume += OnStartPause;
    }   // �|�[�Y

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // �|�[�Y����

    // Update is called once per frame
    void Update()
    {
        if (!_pause)
        {
            if (_random == false)
            {
                _random = true;
                StartCoroutine(Instans());
            }//�|�[�Y���Ă��Ȃ��Ƃ��ɐ������J�n
        }
    }
    IEnumerator Instans()
    {
        //�w�肵�������Ԋu�őҋ@
        yield return new WaitForSeconds(_insSec);
        //���̃p�^�[�����A�N�e�B�u
        _pattern[_current].SetActive(true);
        //�w�肵�����ł܂ł̑ҋ@����
        yield return new WaitForSeconds(_desSec);
        //���̃p�^�[�����A�N�e�B�u
        _pattern[_current].SetActive(false);
        //���̃p�^�[���̃C���f�b�N�X�X�V
        _current = (_current + 1) % _pattern.Length;
        _random = false;
    }
    public void ResetEnemy()
    {
        //�S�Ẵp�^�[�����A�N�e�B�u�ɂ��A���̃I�u�W�F�N�g����A�N�e�B�u�ɂ���
        foreach (var i in _pattern)
        {
            i.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public virtual void OnStartPause(bool pause)
    {
        _pause = pause;
    }
}
