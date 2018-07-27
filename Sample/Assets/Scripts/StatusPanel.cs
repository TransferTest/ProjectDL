using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
   
    public Text display;
    private Image sephira;
    private Image SCG; 
    public Dictionary<string, string> charInfo;

    string[] factors = { "Name", "SephirothName", "LV", "SLV", "ATK", "AGI", "DEF", "INT", "CRI", "detail", "HP", "MP", "nowEXP", "nextEXP", "CharSCG" };

    // Use this for initialization
    void Start()
    {

        

        //임시적인 값입니다. 나중에 캐릭터 정보 저장되면 거시서 값 가져오도록 변경이 필요합니다.
        charInfo = new Dictionary<string, string>();
 
        charInfo.Add(factors[0], "Programmer");
        charInfo.Add(factors[1], "sephiroth");
        charInfo.Add(factors[9], "유니티 어렵다");
        charInfo.Add(factors[14], "CharSCG");
        for (int i = 2; i <= 8; i++)
        {
            charInfo.Add(factors[i], "10");
        }
        charInfo.Add(factors[10], "100");
        charInfo.Add(factors[11], "100");
        charInfo.Add(factors[12], "30");
        charInfo.Add(factors[13], "100");

    }

    // Update is called once per frame
    void Update()
    {
        sephira = GameObject.Find(charInfo["SephirothName"]).GetComponent<Image>(); 
        SCG = GameObject.Find(charInfo["CharSCG"]).GetComponent<Image>();

        Sprite sephira_image = Resources.Load<Sprite>("sephiroth");
        sephira.overrideSprite = sephira_image;

        Sprite CharSCG_image = Resources.Load<Sprite>("CharSCG");
        SCG.overrideSprite = CharSCG_image;


        for (int i = 2; i <= 8; i++)
        {
            display = GameObject.Find(factors[i]).GetComponent<Text>();
            display.text = factors[i] + " : " + charInfo[factors[i]];
        }
        display = GameObject.Find("Name").GetComponent<Text>();
        display.text = charInfo["Name"];


        display = GameObject.Find("detail").GetComponent<Text>();
        display.text = charInfo["detail"];
    }

    public void changeChar()
    {
        //여기서 버튼 누르면 위의 charInfo 내용물 바꾸고 다시 출력하게하면 될것 같습니다.
    }
}
