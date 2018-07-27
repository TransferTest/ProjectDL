using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillObject : MonoBehaviour {

    public Button Skill;
    public Text Name;
    public Image Icon;
    public Text detail;

    public void showDetail()
    {
        Text display;
        display = GameObject.Find("SkillInformation").GetComponent<Text>();
        display.text = detail.text;
    }


}
