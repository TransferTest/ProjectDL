using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is scene manager for "encounter" scene
public class SceneManager_encounter : MonoBehaviour {

    public int num_enemies;
    public GameObject monster;
    public int monster_HP;
    public int monster_max;
    public Slider monster_bar;
    public Button panel_1;
    public Button panel_2;
    public Button panel_3;
    public Button panel_4;
    public Button attackButton;

    private Button[] panels;
    private int chain;

    private bool[] selected;
    private int cur;

    // Use this for initialization
    void Start () {
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
        monster_bar.value = monster_HP;

        if (chain >= 4)
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

        if (monster_HP <= 0)
            loadResult();
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
        monster_HP -= 100;
        chain++;
        attackButton.interactable = false;
        selected[cur - 1] = true;
    }
}
