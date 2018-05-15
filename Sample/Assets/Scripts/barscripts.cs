using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barscripts : MonoBehaviour {

    private float fillAmount; //바가 차있는 비율
    private Text text; //바 위에 표시될 텍스트
    
    public float now_value; //현재값
    public float max_value; //최대값

    private Image content; //바

    

	// Use this for initialization
	void Start () {
        
        string setwhat = gameObject.name; //이 스크립트가 속해있는 이미지가 HP인지 MP인지 EXP인지 구분하기 위해 이름을 가져왔습니다.
        string[] what = setwhat.Split(new char[] { '_' }); //이름에서 앞부분만 챙겨와서 what[0]에는 HP,MP,EXP 중 하나가 들어갈 것입니다.

        content = GameObject.Find(what[0]).GetComponent<Image>(); //그래서 그 이름을 가지고 필요한 바를 찾고
        text = GameObject.Find("ValueText"+what[0]).GetComponent<Text>(); //그 바 위에 표시되는 텍스트 박스를 찾습니다.

        if(what[0] == "HP")
        {
            now_value = 100;
            max_value = 100;
        }else if(what[0] == "MP")
        {
            now_value = 100;
            max_value = 100;
        }else if(what[0] == "EXP")
        {
            now_value = 30;
            max_value = 100;
        }


       


    }

    // Update is called once per frame
    void Update () {
        HandleBar(); //매 프레임마다 바 길이를 조절합니다. 후에 now_value를 Update에서 조절하면 될 것 같습니다.
        text.text = now_value + " / " + max_value; //전형적인 HP스테이터스 보여주기 입니다.
	}
    
    private void HandleBar()
    {
        fillAmount = now_value / max_value; //현재값을 최대값으로 나누어서 비율을 찾습니다. 최대가 1이고 최소가 0인건 나중에.....
        content.fillAmount = fillAmount;
    }

}
