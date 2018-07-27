using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    public string Name;
    public Text display;
    private Image SCG;

    public Dictionary<string, string> passive_skillInfo;
    public Dictionary<string, string> active_skillInfo;

    public RectTransform Content_Position;

    public GameObject ScrollBar;
    public Scrollbar ValueTemp;

    public GameObject SkillOb;
    public Transform Content;
    public List<Skill> SkillList;

    public bool scrollFlag;
    public int expedient;

    // Use this for initialization
    void Start () {
        passive_skillInfo = new Dictionary<string, string>();
        active_skillInfo = new Dictionary<string, string>();
        SkillList = new List<Skill>();
        
        //그냥 임의로 딕셔너리로 넣었습니다. 나중에 스킬 정보 저장형식 정해지면 그때 수정해야합니다.
        passive_skillInfo.Add("HardCoding", "으아아아");
        passive_skillInfo.Add("Patient", "사실 병원에서 일어난지 얼마 안됬습니다.");

        active_skillInfo.Add("Sleep", "졸리다.");
        active_skillInfo.Add("1", "아무의미없습니다.");
        active_skillInfo.Add("2", "아무의미없습니.");
        active_skillInfo.Add("3", "아무의미없습.");
        active_skillInfo.Add("4", "아무의미없.");
        active_skillInfo.Add("5", "아무의.");
        active_skillInfo.Add("6", "아무.");
        active_skillInfo.Add("7", "아.");

        Name = "CharSCG"; //나중에 캐릭터 SCG 가져올 때 수정해야하는 부분입니다. 

        ScrollBar = GameObject.Find("Scrollbar");
        ValueTemp = ScrollBar.GetComponent<Scrollbar>();

        scrollFlag = false;
        expedient = 0;
    }

    // Update is called once per frame
    void Update () {

        SCG = GameObject.Find(Name).GetComponent<Image>();
        Sprite CharSCG_image = Resources.Load<Sprite>("CharSCG");
        SCG.overrideSprite = CharSCG_image;

        //스크롤바 초기 위치를 맨 위로 하고 싶은데 계속 가운데로 오네요...ㅠㅠ
        //이런저런 방법을 써봤는데 한번에는 안되서 편법으로 3번 반복하게 해뒀습니다.
        //2번 반복까지는(...) 뭐가 문제인지 안되네요.
        if(scrollFlag == true)
        {
            scrollReset();
            expedient = expedient + 1;
            if (expedient >= 3)
            {
                scrollFlag = false;
                expedient = 0;
            }
        }

    }

    public void showPassive()
    {
        destroyBtn();
        SkillList.Clear();
        scrollFlag = true;
        Debug.Log(scrollFlag);
        
        foreach (KeyValuePair<string, string> Skills in passive_skillInfo)
        {
            
            Skill temp = new Skill();
            Sprite imageTemp = new Sprite();

            temp.Name = Skills.Key;
            temp.detail = Skills.Value;

            imageTemp = Resources.Load<Sprite>("Test"); //나중에 각각 스킬이름에 맞는 아이콘으로 수정
            temp.Icon = imageTemp;

            SkillList.Add(temp);

        }

        SkillListView();
        
        
    }

    public void showActive()
    {
        destroyBtn();
        SkillList.Clear();
        scrollFlag = true;
        Debug.Log(scrollFlag);

        foreach (KeyValuePair<string, string> Skills in active_skillInfo)
        {
            
            Skill temp = new Skill();
            Sprite imageTemp = new Sprite();

            temp.Name = Skills.Key;
            temp.detail = Skills.Value;

            imageTemp = Resources.Load<Sprite>("Test"); //나중에 각각 스킬이름에 맞는 아이콘으로 수정
            temp.Icon = imageTemp;

            SkillList.Add(temp);
            
        }

        SkillListView();
        
    }

    private void SkillListView()
    {
        GameObject SkillBtnTemp;
        SkillObject objectTemp;

    
        foreach (Skill skill in this.SkillList)
        {
            SkillBtnTemp = Instantiate(this.SkillOb) as GameObject;

            objectTemp = SkillBtnTemp.GetComponent<SkillObject>();

            objectTemp.Name.text = skill.Name;
            objectTemp.Icon.sprite = skill.Icon;
            objectTemp.detail.text = skill.detail;

            SkillBtnTemp.transform.SetParent(this.Content);
        }

        if(SkillList.Count < 7) //UI 기획을 봤는데 스킬 개수가 7개보다 작을때 남은 공간을 아예 빈칸으로 두는 건지 아니면 빈 스킬 칸을 만들어야하는건지 모르겠어서
                                //일단 빈 스킬칸을 만들게 했습니다.
        {
            for(int i = 0;i< 7 - SkillList.Count; i++)
            {
                SkillBtnTemp = Instantiate(this.SkillOb) as GameObject;
                SkillBtnTemp.transform.SetParent(this.Content);
            }
        }
        

        
    }

    public void destroyBtn()
    {
        foreach(Transform child in this.Content.transform)
        {
            Destroy(child.gameObject);
        }
        Text display;
        display = GameObject.Find("SkillInformation").GetComponent<Text>();
        display.text = null;
    }

    public void scrollUp()
    {
        ValueTemp.value = ValueTemp.value + 0.1F;
    }

    public void scrollDown()
    {
        ValueTemp.value = ValueTemp.value - 0.1F;
    }
    
    public void scrollReset()
    {
        ValueTemp.value = 1.0F;
    }


    public void changeChar()
    {
        //status랑 마찬가지로 버튼 누르면 새 캐릭터 정보 불러와서 스킬이랑 SCG바뀌게 하면 될것 같습니다.
    }

}
