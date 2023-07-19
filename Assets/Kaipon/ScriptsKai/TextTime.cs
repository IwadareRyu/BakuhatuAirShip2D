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
    }   // �|�[�Y

    private void OnDisable()
    {
        PauseManager.OnPauseResume -= OnStartPause;
    }   // �|�[�Y����

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
        _sentence.text = "WASD�L�[�ňړ�\nSHIFT�L�[��\n�ᑬ�ړ��ł��邼�I\n�ׂ����ړ���\n�g���Ă���I";
        _pig.enabled = true;
        yield return new WaitForSeconds(10f);
        _sentence.text = "F�L�[��\n���@���΂��邼�I\n���@���΂���\n������100yen\n���邼�I";
        yield return new WaitForSeconds(5f);
        _sentence.text = "�G��|���Ƃ�����\n��ɓ���\n�����͎��@�̋�����\n�g���邼�I";
        yield return new WaitForSeconds(10f);
        _pig.InsSecChange(_sec);
        _sentence.text = "�_���[�W���󂯂��\n��ʑS�̂̎ˌ���\n�ł�����\n�{�����o�邼�I�I";
        yield return new WaitForSeconds(10f);
        _sentence.text = "��̐Ԃ��G���A��\n�����\n������������\n���邼�I�I";
        yield return new WaitForSeconds(10f);
        _pig.enabled = false;
        _sentence.text = "����\n�_���[�W���󂯂��\n������500yen����\n�̂Œ��ӂ���I�I";
        yield return new WaitForSeconds(10f);
        _sentence.text = "�N�̂��߂�\n����2500yen\n�p�ӂ������I�I\n�@�̐��A�b�v��\n�������߂����I�I";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddScore(2500);
        yield return new WaitForSeconds(5f);
        _sentence.text = "�S�[���ɓ����\n�I����!\n���͂�������\n�{�X�킾�I�I\n�O�b�g���b�N�I";
        GameManager.Instance.SetDeadEnemy(-100);

    }

    void OnStartPause(bool pause)
    {
        _startBool = pause;
    }
}
