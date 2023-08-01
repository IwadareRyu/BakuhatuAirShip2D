using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpeyIns : MonoBehaviour
{
    bool _insbool; 
    [Header("�����X�^�[�̃X�|�[���ꏊ")]
    [SerializeField] GameObject[] _harpeyDown, _harpeyLeft, _harpeyRight ,_harpey;
    [SerializeField] float _coolsec;
    [SerializeField] float _inssec;
    [SerializeField] Spawn_Pattern _pattern;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        //_insbool��false�ɏ������B
        _insbool = false;
    }
    // Update is called once per frame
    void Update()
    {
        //_insbool��false�̏ꍇ�Ɏ��s
        if(_insbool == false)
        {
           //_insbool��true�ɐݒ肵�ă����X�^�[�̐������J�n
            _insbool = true;
            if(_pattern == Spawn_Pattern.Legion)
            {
                // Spawn_Pattern��Legion�̏ꍇ�AHarpeyLegionInstans�R���[�`�����J�n
                StartCoroutine(HarpeyLegionInstans());
            }
            else if(_pattern == Spawn_Pattern.RandomIns)
            {
                // Spawn_Pattern��RandomIns�̏ꍇ�ARandomInstance�R���[�`�����J�n
                StartCoroutine(RandomInstance());
            }
        }
    }
    /// <summary>�����X�^�[��������Ԃɐ�������R���[�`��</summary>
    IEnumerator HarpeyLegionInstans()
    {
        yield return new WaitForSeconds(_inssec);
        var random = Random.Range(0,2);
        // �X�|�[���ꏊ���烉���_���Ȉʒu��harpey�𐶐�
        Instantiate(_harpey[0], _harpeyDown[random].transform.position,Quaternion.identity);
        yield return new WaitForSeconds(_inssec);
        random = Random.Range(0, 3);
        Instantiate(_harpey[1], _harpeyLeft[random].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_inssec);
        random = Random.Range(0, 3);
        Instantiate(_harpey[2], _harpeyRight[random].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_coolsec);
        // ����������A_insbool��false�ɂ���
        _insbool = false;
    }
   /// <summary>�����_���Ȉʒu��harpey�𐶐�����R���[�`��</summary>
    IEnumerator RandomInstance()
    {
        yield return new WaitForSeconds(_inssec);
        var random = Random.Range(0, 3);
        if (random == 0)
        {
            random = Random.Range(0, 2);
            Instantiate(_harpey[0], _harpeyDown[random].transform.position, Quaternion.identity);
        }   //random��0�̏ꍇ�A�����_���Ȓl���擾��harpey�𐶐�
        else if (random == 1)
        {
            random = Random.Range(0, 3);
            Instantiate(_harpey[1], _harpeyLeft[random].transform.position, Quaternion.identity);
        }   //random��1�̏ꍇ�A�����_���Ȓl���擾��harpey�𐶐�
        else 
        {
            random = Random.Range(0, 3);
            Instantiate(_harpey[2], _harpeyRight[random].transform.position, Quaternion.identity);
        }   //random��2�̏ꍇ�A�����_���Ȓl���擾��harpey�𐶐�
        //�����I����A_insbool��false�ɖ߂�
        _insbool = false;   
    }

    //�����X�^�[�̐����p�^�[�����w�肷�邽�߂̗񋓌^
    enum Spawn_Pattern
    {
        Legion,  //  �R�c
        RandomIns,  //�@�����_������  
    }
}
