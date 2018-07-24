using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is scene manager for "encounter" scene
public class SceneManager_encounter : MonoBehaviour {

    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;
    public Sprite sp4;
    public Sprite sp5;
    public Sprite sp6;

    public Button panel_1;
    public Button panel_2;
    public Button panel_3;
    public Button panel_4;
    public Button attackButton;

    public GameObject monsterPrefab;
    public GameObject canvas;
    public Button buttonPrefab;
    public Slider sliderPrefab;
    public GameObject effectPrefab;

    private Button[] panels;
    private int chain;

    private bool[] selected;
    private int cur;

    private int num_enemies;

    private List<Char> party;

    private List<monster> monsters;
    private List<Animator> anims;
    private List<Animator> player_anims;

    private List<Action> actionList;

    private bool monster_turn;
    private bool player_turn;
    private float t;

    private bool patt;

    // Use this for initialization
    void Start () {
        patt = false;
        t = 0;
        actionList = new List<Action>();
        monster_turn = false;

        anims = new List<Animator>();
        player_anims = new List<Animator>();

        party = new List<Char>();
        party.Add(new Char());
        party.Add(new Char());
        party.Add(new Char());
        party.Add(new Char());

        for (int i = 0; i < 4; i++)
        {
            GameObject neweff = Instantiate(effectPrefab);
            neweff.transform.Translate(new Vector3((float)(-8.9 + 2.225 + i * 4.45), (float)(-4.23), 0));
            player_anims.Add(neweff.GetComponent<Animator>());
        }

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
            t += Time.deltaTime;
            if (actionList.Count > 0)
            {
                if (!patt && t > 0.5)
                {
                    patt = true;
                    actionList[0].animate(anims, player_anims);
                }
                if (t > 1.3)
                {
                    Action act = actionList[0];
                    actionList.RemoveAt(0);
                    act.doAction();
                    t = 0;
                    patt = false;
                }
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
                t = 0;
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

        actionList.Add(new Action(0, cur - 1, n, monsters, party));

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
            GameObject neweff = Instantiate(effectPrefab);

            newbut.transform.SetParent(canvas.transform);
            newsli.transform.SetParent(canvas.transform);

            monsters[i].obj = newmon;
            monsters[i].select = newbut;
            monsters[i].HP_bar = newsli;
            monsters[i].effect = neweff;
            monsters[i].effAnim = neweff.GetComponent<Animator>();

            newmon.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 2.5 + ((float)i) * 5), 0, 0));
            neweff.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 2.5 + ((float)i) * 5), 0, 0));
            newbut.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 180 + ((float)i) * 360) + 640, 360, 0));
            newsli.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 180 + ((float)i) * 360) + 640, 360, 0));

            monsters[i].anim = newmon.GetComponent<Animator>();
            monsters[i].sprite = newmon.GetComponent<SpriteRenderer>();
            monsters[i].attacked = false;

            anims.Add(monsters[i].effAnim);

            int n = i;
            newbut.onClick.AddListener(delegate { enemySelect(n); });

            if (monsters[i].id == 1)
            {
                monsters[i].sprite.sprite = sp1;
            }
            else if (monsters[i].id == 2)
            {
                monsters[i].sprite.sprite = sp2;
            }
            else if (monsters[i].id == 3)
            {
                monsters[i].sprite.sprite = sp3;
            }
            else if (monsters[i].id == 4)
            {
                monsters[i].sprite.sprite = sp4;
            }
            else if (monsters[i].id == 5)
            {
                monsters[i].sprite.sprite = sp5;
            }
            else
            {
                monsters[i].sprite.sprite = sp6;
            }
        }
    }
}
