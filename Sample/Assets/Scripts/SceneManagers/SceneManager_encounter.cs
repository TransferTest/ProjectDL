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

    public GameObject player_1;
    public GameObject player_2;
    public GameObject player_3;
    public GameObject player_4;
    private PanelScript[] player_panels;

    private Button panel_1; //Button for player 1
    private Button panel_2; //Button for player 2
    private Button panel_3; //Button for player 3
    private Button panel_4; //Button for player 4
    public Button attackButton;
    public Button skillButton;

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
    private bool matt;
    private int mtar;

    public int num_party;

    private int monster_attacking;
    List<List<float>> teff;
    private int curAct;
    private bool ptarget; //Checks whether player can currently be selected as a target.
 //Type effectiveness
    // Use this for initialization
    void Start()
    {
        ptarget = false;
        curAct = 0;
        monster_attacking = 0;
        patt = false;
        t = 0;
        actionList = new List<Action>();
        monster_turn = false;

        anims = new List<Animator>();
        player_anims = new List<Animator>();

        player_panels = new PanelScript[4];
        player_panels[0] = player_1.GetComponent<PanelScript>();
        player_panels[1] = player_2.GetComponent<PanelScript>();
        player_panels[2] = player_3.GetComponent<PanelScript>();
        player_panels[3] = player_4.GetComponent<PanelScript>();

        panel_1 = player_1.transform.Find("PanelButton").GetComponent<Button>();
        panel_2 = player_2.transform.Find("PanelButton").GetComponent<Button>();
        panel_3 = player_3.transform.Find("PanelButton").GetComponent<Button>();
        panel_4 = player_4.transform.Find("PanelButton").GetComponent<Button>();

        for (int i = 0; i < 4; i++)
        {
            GameObject neweff = Instantiate(effectPrefab);
            neweff.transform.SetParent(canvas.transform);
            neweff.transform.Translate(new Vector3((float)(-480 + i * 320) + 640, 60, 0));
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
        party = gc.charInformation.party;
        num_party = party.Count;
        for (int i = 0; i < 4; i++)
        {
            player_panels[i].initiallize(party[i].HP_max, party[i].MP_max);
            player_panels[i].setHP(party[i].HP);
            player_panels[i].setMP(party[i].MP);
        }
        // The code below is a crude type effectiveness code
        teff = new List<List<float>>() {
            new List<float> { 1f, 0.5f, 1.5f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1.5f, 1f, 0.5f }, // Fire
            new List<float> { 1.5f, 1f, 0.5f, 1f, 1f, 1.5f, 1f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1f, 0.5f }, // Wind
            new List<float> { 0.5f, 1.5f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 0.5f }, // Ice
            new List<float> { 1f, 1f, 1f, 1f, 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1.5f, 1.5f, 0.5f }, // Light
            new List<float> { 1f, 1f, 1f, 1.5f, 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1.5f, 1f, 1f, 1f, 1.5f, 0.5f }, // Dark
            new List<float> { 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f }, // Fire/Wind
            new List<float> { 0.5f, 1f, 0.5f, 1f, 1f, 0.5f, 1f, 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f }, // Wind/Ice
            new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f }, // Ice/Fire
            new List<float> { 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 1.5f, 1.5f, 1f, 1f, 1f, 0.5f }, // Light/Fire
            new List<float> { 1f, 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1f, 0.5f, 1f, 1.5f, 1f, 1f, 0.5f }, // Light/Wind
            new List<float> { 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 1.5f, 1f, 1f, 1f, 1.5f, 1f, 0.5f }, // Light/Ice
            new List<float> { 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1f, 1f, 1f, 0.5f, 1.5f, 1f, 0.5f }, // Dark/Fire
            new List<float> { 1f, 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1f, 1.5f, 1f, 0.5f, 1f, 0.5f }, // Dark/Wind
            new List<float> { 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.5f, 1.5f, 1f, 1f, 0.5f}, // Dark/Ice
            new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f , 0.5f}, // Light/Dark
            new List<float> {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f } //Debug
    };

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
                    syncAll();
                    t = 0;
                    patt = false;
                }
            }
            else
            {
                player_turn = false;
                monster_turn = true;
                matt = false;
                monster_attacking = 0;
                chain = 0;
                t = 0;
            }
        }
        else if (monster_turn)
        {
            t += Time.deltaTime;
            if (!matt && t > 0.5)
            {
                matt = true;
                //check
                bool result = true;

                for (int i = 0; i < num_enemies; i++)
                {
                    if (monsters[i].HP > 0)
                        result = false;
                }

                if (result)
                {
                    loadResult(); // End of battle
                    return;
                }

                if (monster_attacking >= monsters.Count)
                {
                    monster_turn = false;
                    initiallizeInteractable(); // Back to player turn
                    t = 0;
                    return;
                }
                if (monsters[monster_attacking].HP <= 0)
                {
                    monster_attacking++;
                    t = 0;
                    matt = false;
                    return;
                }
                do
                {
                    mtar = Random.Range(0, 4);
                } while (party[mtar].HP <= 0);

                player_anims[mtar].SetTrigger("attack");
                anims[monster_attacking].SetTrigger("skill");
            }
            if (t > 1.3)
            {
                matt = false;
                int basedam= (int)System.Math.Ceiling((teff[monsters[monster_attacking].TYPE][party[mtar].TYPE] * (monsters[monster_attacking].ATK) * Random.Range(85, 115) / 100 ));
                if (Random.Range(0,100)>= 100-monsters[monster_attacking].CRI* monsters[monster_attacking].Lv/ party[mtar].Lv) // Critical Chance
                {
                    basedam = (int) basedam * (15 + (10*monsters[monster_attacking].CMULT / 100))/10;
                }
                if (Random.Range(0,1000)<System.Math.Min(18* party[mtar].AGI/(party[mtar].AGI+ monsters[monster_attacking].AGI),10)) // Evade
                {
                    basedam = 0;
                }
                party[mtar].HP -= System.Math.Max(basedam - party[mtar].DEF, 0);
                player_panels[mtar].setHP(party[mtar].HP); //Damage Formula
                t = 0;
                monster_attacking++;
            }
        }
	}

    private void syncAll()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].HP_bar.value = monsters[i].HP;
        }
        for (int i = 0; i < party.Count; i++)
        {
            player_panels[i].setHP(party[i].HP);
            player_panels[i].setMP(party[i].MP);
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
        if (ptarget)
        {
            chain++;
            selected[cur - 1] = true;
            for (int i = 0; i < 4; i++)
            {
                if (selected[i])
                    panels[i].interactable = false;
                else
                    panels[i].interactable = true;
            }
            actionList.Add(new Action(curAct, cur - 1, n - 1, monsters, party, null));

            if (chain >= 4)
            {
                player_turn = true;
            }
            ptarget = false;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (selected[i])
                    continue;
                panels[i].interactable = true;
            }
            panels[n - 1].interactable = false;
            attackButton.interactable = true;
            if (party[n - 1].MP >= party[n - 1].requiredMP && party[n - 1].HP >= party[n - 1].requiredHP)
                skillButton.interactable = true;
            cur = n;
        }
    }

    public void attack()
    {
        for (int i = 0; i < num_enemies; i++)
        {
            if (monsters[i].HP > 0)
                monsters[i].select.interactable = true;
        }
        attackButton.interactable = false;
        skillButton.interactable = false;
        curAct = 0;
    }

    public void skill()
    {
        int type_skill = party[cur - 1].type_skill();
        if (type_skill == 1) //Heal, buff
        {
            ptarget = true;
            for (int i = 0; i < 4; i++)
            {
                panels[i].interactable = true;
            }
        }
        else if (type_skill == 0) //Atk
        {
            ptarget = false;
            for (int i = 0; i < num_enemies; i++)
            {
                if (monsters[i].HP > 0)
                    monsters[i].select.interactable = true;
            }
        }
        else
        {
            ptarget = false;
            chain++;
            selected[cur - 1] = true;

            actionList.Add(new Action(1, cur - 1, 0, monsters, party, null));

            for (int i = 0; i < num_enemies; i++)
            {
                monsters[i].select.interactable = false;
            }
            if (chain >= 4)
            {
                player_turn = true;
            }
        }
        attackButton.interactable = false;
        skillButton.interactable = false;
        curAct = 1;
    }

    public void enemySelect(int n)
    {
        chain++;
        selected[cur - 1] = true;

        actionList.Add(new Action(curAct, cur - 1, n, monsters, party, null));

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

            neweff.transform.SetParent(canvas.transform);
            newbut.transform.SetParent(canvas.transform);
            newsli.transform.SetParent(canvas.transform);

            monsters[i].obj = newmon;
            monsters[i].select = newbut;
            monsters[i].HP_bar = newsli;
            monsters[i].effect = neweff;
            monsters[i].effAnim = neweff.GetComponent<Animator>();

            newmon.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 2.5 + ((float)i) * 5), 0, 0));
            neweff.transform.Translate(new Vector3((float)(-((float)num_enemies - 1) * 180 + ((float)i) * 360) + 640, 400, 0));
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
