using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public void Click()
    {
        SEManager.Instance.SEPlay(SEManager.SE.Click);
    }
}
