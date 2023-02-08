using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    float count = 1f;
    float money = 1000f;
    [SerializeField] GameObject _insObj;
    [SerializeField] Text text;
    [SerializeField] Text moneytext;
    // Start is called before the first frame update
    void Start()
    {
        text.text = count.ToString();
        moneytext.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            GameObject obj = Instantiate(_insObj, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
            obj.transform.localScale = new Vector2(count, count);
        }
    }

    public void Count()
    {
        if (money > 300)
        {
            count++;
            text.text = count.ToString();
            money = money - 300;
            moneytext.text = money.ToString();
        }
    }

    void OrenoText(Text input)
    {

    }
}
