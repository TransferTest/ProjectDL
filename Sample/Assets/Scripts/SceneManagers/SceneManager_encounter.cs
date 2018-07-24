using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is scene manager for "encounter" scene
public class SceneManager_encounter : MonoBehaviour {

    public Button panel_1;
    public Button panel_2;
    public Button panel_3;
    public Button panel_4;
    public Button attackButton;

    public GameObject monsterPrefab;
    public GameObject canvas;
    public Button buttonPrefab;
    public Slider sliderPrefab;

    private Button[] panels;
    private int chain;

    private bool[] selected;
    private int cur;

    private int num_enemies;

    private List<monster> monsters;

    private List<int> targetList;

    private bool monster_turn;
    private bool player_turn;
    private float t;

    // Use this for initialization
    void Start () {
        t = 0;
        targetList = new List<int>();
        monster_turn = false;

        chain = 0;
        panels = new Button[4];
        panels[0] = panel_1;
        panels[1] = panel_2;
        panels[2] = panel_3;
        panels[3] = panel_4;

        selected = new bool[4];
        selected[0] = false;
        selected[1] = false;
        selected[2] = false;
        selected[3] = false;

        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();//find the GlobalControl object
        SceneInfo_encounter si = (SceneInfo_encounter)gc.sceneInformation;

        Debug.Log("Load Scene");

        if (si == null)//if there is no scene information (it might means this scene is first loaded)
        {
            Debug.Log("no scene information");
        }
        //if there is scene information
        else
        {
            loadInfo_encounter(si);
            Debug.Log("loaded");
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (player_turn)
        {
            if (targetList.Count > 0)
            {
                int tar = targetList[0];
                targetList.RemoveAt(0);
                monsters[tar].HP -= 100;
                monsters[tar].HP_bar.value = monsters[tar].HP;
            }
            else
            {
                player_turn = false;
                monster_turn = true;
                chain = 0;
                t = 0;
            }
        }
        else if (monster_turn)
        {
            t += Time.deltaTime;
            if (t > 3)
            {
                bool result = true;

                for (int i = 0; i < num_enemies; i++)
                {
                    if (monsters[i].HP > 0)
                        result = false;
                }

                if (result)
                    loadResult();

                monster_turn = false;
                initiallizeInteractable();
            }
        }
	}
    private void initiallizeInteractable ()
    {
        selected[0] = false;
        selected[1] = false;
        selected[2] = false;
        selected[3] = false;

        panel_1.interactable = true;
        panel_2.interactable = true;
        panel_3.interactable = true;
        panel_4.interactable = true;

        chain = 0;
    }

    //loads result scene
    public void loadResult()
    {
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        gc.save();
        SceneManager.LoadScene("result");
    }

    public void panelSelected(int n)
    {
        for (int i = 0; i < 4; i++)
        {
            if (selected[i])
                continue;
            panels[i].interactable = true;
        }
        panels[n - 1].interactable = false;
        attackButton.interactable = true;
        cur = n;
    }

    public void attack()
    {
        for (int i = 0; i < num_enemies; i++)
        {
            if (monsters[i].HP > 0)
                monsters[i].select.interactable = true;
        }
        attackButton.interactable = false;
    }
    public void enemySelect(int n)
    {
        chain++;
        selected[cur - 1] = true;

        targetList.Add(n);

        for (int i=0; i < num_enemies; i++)
        {
            monsters[i].select.interactable = false;
        }
        if (chain >= 4)
        {
            player_turn = true;
        }
    }

    private void loadInfo_encounter (SceneInfo_encounter si)
    {
        num_enemies = si.monsters.Count;
        monsters = si.monsters;
        for (int i = 0; i < num_enemies; i++)
        {
            GameObject newmon = Instantiate(monsterPrefab);
            Button newbut = Instantiate(buttonPrefab);
            Slider newsli = Instantiate(sliderPrefab);

            newbut.transform.SetParent(canvas.transform);
            newsli.transform.SetParent(canvas.transform);

            monsters[i].obj = newmon;
            monsters[i].select = newbut;
            monsters[i].HP_bar = newsli;

            newmon.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 2.5 + ((float)i) * 5), 0, 0));
            newbut.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 180 + ((float)i) * 360) + 640, 360, 0));
            newsli.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 180 + ((float)i) * 360) + 640, 360, 0));

            monsters[i].anim = newmon.GetComponent<Animator>();
            monsters[i].sprite = newmon.GetComponent<SpriteRenderer>();
            monsters[i].attacked = false;

            int n = i;
            newbut.onClick.AddListener(delegate { enemySelect(n); });
        }
    }
}
