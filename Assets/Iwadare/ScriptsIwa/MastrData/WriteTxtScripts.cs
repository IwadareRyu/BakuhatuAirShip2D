using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterData;
using System.IO;
using UnityEngine.Networking;
using Network;

public class WriteTxtScripts : MonoBehaviour
{
    [SerializeField]
    List<ReadFile> _readFiles = new List<ReadFile>();
    [ContextMenuItem("Setup", "Setup")]
    [SerializeField] string _fileNameURL;
    [SerializeField] bool _writingBool = true;
    [SerializeField] string _txtFileName;

    private void Start()
    {
        TxtWritingMethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{_fileNameURL}?sheet=");
        yield return request.SendWebRequest();
        Debug.Log("Žó‚¯Žæ‚èŠ®—¹");
        string s = request.downloadHandler.text;
        Debug.Log(s);
        MasterDataClass<ReadFile> data = JsonUtility.FromJson<MasterDataClass<ReadFile>>(s);
        _readFiles.Clear();
        foreach(var d in data.Data)
        {
            _readFiles.Add(d);
        }
    }

    private void TxtWritingMethod()
    {
        string path = $"Assets/NobelTextFile/{_txtFileName}";
        Debug.Log(path);
        if (_writingBool || _txtFileName != "")
        {
            using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("UTF-8")))
            {
                for (var i = 0; i < _readFiles.Count;i++)
                {
                    writer.WriteLine(_readFiles[i].Command);
                    writer.WriteLine(_readFiles[i].Text);
                }
            }
        }


    }

}
