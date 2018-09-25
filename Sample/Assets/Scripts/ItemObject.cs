using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour {

    public Button Item;
    public Text Name;
    public Image Icon;
    public Text detail;

    public void showDetail()
    {
        Text display;
        display = GameObject.Find("ItemInformation").GetComponent<Text>();
        display.text = detail.text;
    }
}
