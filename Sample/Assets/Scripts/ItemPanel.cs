using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {

    public string Name;
    public Text display;
    private Image SCG;

    public Item[] Inven;
    public Item[] inven;

    public RectTransform Content_Position;

    public GameObject ScrollBar;
    public Scrollbar ValueTemp;

    public GameObject ItemOb;
    public Transform Content;
    public List<Item> ItemList;

    public bool scrollFlag;
    public int expedient;

    // Use this for initialization
    void Start()
    {
        //GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        //Inven = gc.inven;
        //실제 상황에서는 위와 같이 GC에서 가져와야하지만 지금은 테스트라서 그냥 임의로 만들었습니다.
        ItemList = new List<Item>();

        inven = new Item[10];
        for (int i = 0; i < 5; i++)
        {
            string j = i.ToString();
            inven[i] = new Item(1, i, j, j);
        }
        for (int i = 5; i < 10; i++)
        {
            string j = i.ToString();
            inven[i] = new Item(3, i, j, j);
        }

        Inven = inven;

        Name = "CharSCG"; //나중에 캐릭터 SCG 가져올 때 수정해야하는 부분입니다. 

        ScrollBar = GameObject.Find("Scrollbar");
        ValueTemp = ScrollBar.GetComponent<Scrollbar>();

        scrollFlag = false;
        expedient = 0;
    }

    // Update is called once per frame
    void Update()
    {

        SCG = GameObject.Find(Name).GetComponent<Image>();
        Sprite CharSCG_image = Resources.Load<Sprite>("CharSCG");
        SCG.overrideSprite = CharSCG_image;


        //스크롤바 초기 위치를 맨 위로 하고 싶은데 계속 가운데로 오네요...ㅠㅠ
        //이런저런 방법을 써봤는데 한번에는 안되서 편법으로 3번 반복하게 해뒀습니다.
        //2번 반복까지는(...) 뭐가 문제인지 안되네요.
        if (scrollFlag == true)
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

    public void showConsumption()
    {
        destroyBtn();
        ItemList.Clear();
        scrollFlag = true;
        Debug.Log(scrollFlag);

        for(int i = 0; i < Inven.Length; i++)
        {   
            if(Inven[i].id == 1 || Inven[i].id == 2)
            {
                Item temp = new Item();
                Sprite imageTemp = Resources.Load<Sprite>("Test");

                temp.id = Inven[i].id;
                temp.Name = Inven[i].Name;
                temp.count = Inven[i].count;
                temp.detail = Inven[i].detail;

                imageTemp = Resources.Load<Sprite>("Test"); //나중에 각각 아이템 이름에 맞는 아이콘으로 수정
                temp.Icon = imageTemp;

                ItemList.Add(temp);
            }
            
        }

        ItemListView();


    }

    public void showMateiral()
    {
        destroyBtn();
        ItemList.Clear();
        scrollFlag = true;
        Debug.Log(scrollFlag);

        for (int i = 0; i < Inven.Length; i++)
        {
            if (Inven[i].id == 3)
            {
                Item temp = new Item();
                Sprite imageTemp = Resources.Load<Sprite>("Test");

                temp.id = Inven[i].id;
                temp.Name = Inven[i].Name;
                temp.count = Inven[i].count;
                temp.detail = Inven[i].detail;

                imageTemp = Resources.Load<Sprite>("Test"); //나중에 각각 아이템 이름에 맞는 아이콘으로 수정
                temp.Icon = imageTemp;

                ItemList.Add(temp);
            }

        }

        ItemListView();

    }

    private void ItemListView()
    {
        GameObject ItemBtnTemp;
        ItemObject objectTemp;


        foreach (Item curItem in this.ItemList)
        {
            ItemBtnTemp = Instantiate(this.ItemOb) as GameObject;

            objectTemp = ItemBtnTemp.GetComponent<ItemObject>();

            objectTemp.Name.text = curItem.Name;
            objectTemp.Icon.sprite = curItem.Icon;
            objectTemp.detail.text = curItem.detail;

            ItemBtnTemp.transform.SetParent(this.Content);
        }

        if (ItemList.Count < 7) //UI 기획을 봤는데 아이템 개수가 7개보다 작을때 남은 공간을 아예 빈칸으로 두는 건지 아니면 빈 아이템 칸을 만들어야하는건지 모르겠어서
                                 //일단 빈 아이템칸을 만들게 했습니다.
        {
            for (int i = 0; i < 7 - ItemList.Count; i++)
            {
               ItemBtnTemp = Instantiate(this.ItemOb) as GameObject;
               ItemBtnTemp.transform.SetParent(this.Content);
            }
        }


    }

    public void destroyBtn()
    {
        foreach (Transform child in this.Content.transform)
        {
            Destroy(child.gameObject);
        }
        Text display;
        display = GameObject.Find("ItemInformation").GetComponent<Text>();
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


 
}
