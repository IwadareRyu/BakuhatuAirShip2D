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
        _sentence.text = "WASD�L�[�ňړ�";
        yield return new WaitForSeconds(10f);
        _sentence.text = "F�L�[��\n���@���΂��邼�I\n���@���΂���\n100�~���邼�I";
        yield return new WaitForSeconds(5f);
        _sentence.text = "�G��|���Ƃ�����\n��ɓ���\n�����͎��@�̋�����\n�g���邼�I";
        yield return new WaitForSeconds(10f);
        _pig.InsSecChange(_sec);
        _sentence.text = "�_���[�W���󂯂��\n��ʑS�̂̎ˌ���\n�ł�����\n�{�����o�邼�I�I";
        yield return new WaitForSeconds(10f);
        _pig.enabled = false;
        yield return new WaitForSeconds(1f);
        _sentence.text = "�N�̂��߂�\n25000�~\n�p�ӂ������I�I\n�@�̐��A�b�v��\n�������߂����I�I";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddScore(25000);
        yield return new WaitForSeconds(1f);
        _sentence.text = "�S�[���ɓ���ΏI����\n���͂��������{�X�킾�I�I\n�O�b�g���b�N�I";
        GameManager.Instance.SetDeadEnemy(-100);

    }
}
