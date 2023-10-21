using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public void Click()
    {
        BGMManager.Instance.SEPlay(SE.Click);
    }
}
