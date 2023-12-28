using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] string _loadScene = "tutorial";
    // Start is called before the first frame update
    public void OnClickStartButton()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    //�V�[���̓ǂݍ��݂�x��������
    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_loadScene);
    }//1�b�x�������A�`���[�g���A���V�[����ǂݍ���
}
