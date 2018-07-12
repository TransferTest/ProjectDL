using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is scene manager for "encounter" scene
public class SceneManager_encounter : MonoBehaviour {

    public int num_enemies;
    public GameObject monster;
    public GameObject monster_2;
    public int monster_HP;
    public int monster_HP_2;
    public Slider monster_bar;
    public Slider monster_bar_2;
    public Button monster_select;
    public Button monster_select_2;
    public Button panel_1;
    public Button panel_2;
    public Button panel_3;
    public Button panel_4;
    public Button attackButton;


    private Button[] panels;
    private int chain;

    private bool[] selected;
    private int cur;

    private SpriteRenderer monster_sprite;
    private SpriteRenderer monster_sprite_2;

    private Animator monster_anim;
    private Animator monster_anim_2;

    private bool monster_attacked;
    private bool monster_attacked_2;

    private bool monster_turn;
    private float t;

    // Use this for initialization
    void Start () {
        t = 0;
        monster_turn = false;
        monster_sprite = monster.GetComponent<SpriteRenderer>();
        monster_sprite_2 = monster_2.GetComponent<SpriteRenderer>();

        monster_anim = monster.GetComponent<Animator>();
        monster_anim_2 = monster_2.GetComponent<Animator>();

        monster_attacked = false;
        monster_attacked_2 = false;

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
        SceneInfo si = gc.sceneInformation;

        Debug.Log("Load Scene");

        if (si == null)//if there is no scene information (it might means this scene is first loaded)
        {
            Debug.Log("no scene information");
            return;
        }
        //if there is scene information
        Debug.Log("loaded");
    }
	
	// Update is called once per frame
	void Update () {
        if (monster_turn)
        {
            t += Time.deltaTime;
            if (t <= 1.5 && ! monster_attacked)
            {
                if (monster_HP > 0)
                {
                    monster_anim.SetTrigger("attack");
                    monster_attacked = true;
                }
                else if (monster_HP_2 > 0)
                {
                    monster_anim_2.SetTrigger("attack");
                    monster_attacked_2 = true;
                    monster_attacked = true;
                }
            }
            else if (t > 1.5)
            {
                if (monster_HP_2 <= 0)
                {
                    monster_turn = false;
                    initiallizeInteractable();
                    return;
                }
                if (! monster_attacked_2)
                {
                    monster_anim_2.SetTrigger("attack");
                    monster_attacked_2 = true;
                }
                else if (monster_HP <= 0)
                {
                    monster_turn = false;
                    initiallizeInteractable();
                    return;
                }
            }

            if (t > 3)
            {
                monster_turn = false;
                initiallizeInteractable();
            }
            return;
        }
        if (chain >= 4)
        {
            monster_turn = true;
            monster_attacked = false;
            monster_attacked_2 = false;
            t = 0;
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
        if (monster_HP > 0)
            monster_select.interactable = true;
        if (monster_HP_2 > 0)
            monster_select_2.interactable = true;
        attackButton.interactable = false;
    }
    public void enemySelect(int n)
    {
        if (n == 1)
            monster_HP -= 100;
        else
            monster_HP_2 -= 100;
        chain++;
        selected[cur - 1] = true;
        monster_select.interactable = false;
        monster_select_2.interactable = false;

        if (monster_HP <= 0 && monster_HP_2 <= 0)
            loadResult();
        if (monster_HP > 0)
            monster_bar.value = monster_HP;
        else
        {
            monster_bar.value = 0;
            //monster_sprite.color = new Color(0, 0, 0);
            monster_anim.SetTrigger("dead");
        }
        if (monster_HP_2 > 0)
            monster_bar_2.value = monster_HP_2;
        else
        {
            monster_bar_2.value = 0;
            //monster_sprite_2.color = new Color(0, 0, 0);
            monster_anim_2.SetTrigger("dead");
        }
    }
}
