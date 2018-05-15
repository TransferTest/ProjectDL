using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanalContorl : MonoBehaviour {

    private GameObject now_panel; //지금 띄우려고 하는 패널에 관한 정보를 담으려고 합니다. 아직 스테이터스밖에 없지만..ㅎㅎ
    private bool activeflag; //패널의 활성에 관한 정보를 담을 예정입니다.
    public Text display; //화면에 표시되는 텍스트
    private Image sephira; //세피라 이미지
    private Image SCG; //SCG


	// Use this for initialization
	void Start () {
        sephira = GameObject.Find("sephiroth").GetComponent<Image>(); //resources 폴더에서 세피로트 이미지를 가져옵니다.
        SCG = GameObject.Find("CharSCG").GetComponent<Image>(); //resources 폴더에서 SCG 이미지를 가져옵니다.

        Sprite sephira_image = Resources.Load<Sprite>("sephiroth"); //가져온 이미지를 스프라이트 형태로 덮어씌웁니다.
        sephira.overrideSprite = sephira_image;

        Sprite CharSCG_image = Resources.Load<Sprite>("CharSCG");
        SCG.overrideSprite = CharSCG_image;


        string[] need_to_display = { "LV", "SLV", "ATK", "AGI", "DEF", "INT", "CRI", "detail" }; //화면에 표시될 것들 이름을 나열해봤습니다.
        int value = 10;
        string details = "멋있는 설명";

        for (int i = 0; i < need_to_display.Length; i++)
        {
            display = GameObject.Find(need_to_display[i]).GetComponent<Text>();
            if (need_to_display[i] == "detail") display.text = details + ".";
            else display.text = need_to_display[i] + " : " + value.ToString();

        }
       
        now_panel = GameObject.Find("Status_Panel");
        activeflag = true;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void turniton()  //버튼 눌렀을대 꺼지고 켜지게 하려고 만든 함수입니다.
    {
        now_panel.SetActive(activeflag);
    }

    public void didyouclick()
    {
        if (activeflag == true) activeflag = false;
        else if (activeflag == false) activeflag = true;
    }

}
