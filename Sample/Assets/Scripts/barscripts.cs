using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barscripts : MonoBehaviour {
    //나중에 캐릭터 정보 저장되면 거기서 값 가져오는 것으로 변경 필요

    private float fillAmount; //바가 차있는 비율
    private Text text; //바 위에 표시될 텍스트
    
    public float now_value; //현재값
    public float max_value; //최대값

    private Image content; //바

    

	// Use this for initialization
	void Start () {
        string HP = "100";
        string MP = "100";
        string nowEXP = "30";
        string nextEXP = "100";
        
        string setwhat = gameObject.name;
        string[] what = setwhat.Split(new char[] { '_' });


        content = GameObject.Find(what[0]).GetComponent<Image>(); //그래서 그 이름을 가지고 필요한 바를 찾고
        text = GameObject.Find("ValueText"+what[0]).GetComponent<Text>(); //그 바 위에 표시되는 텍스트 박스를 찾습니다.

        if(what[0] == "HP")
        {
            now_value = int.Parse(HP);
            max_value = int.Parse(HP);
        }
        else if(what[0] == "MP")
        {
            now_value = int.Parse(MP);
            max_value = int.Parse(MP);
        }
        else if(what[0] == "EXP")
        {
            now_value = int.Parse(nowEXP);
            max_value = int.Parse(nextEXP);
        }

    }

    // Update is called once per frame
    void Update () {
        HandleBar();
        text.text = now_value + " / " + max_value;
	}
    
    private void HandleBar()
    {
        fillAmount = now_value / max_value; //최대가 1이고 최소가 0인건 나중에.....
        content.fillAmount = fillAmount;
    }

}
