using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this is scene manager for "menu" scene
public class SceneManager_menu : MonoBehaviour {

    private GameObject[] buttons;
    public GameObject ask;

    public GameObject Status;
    public GameObject Skill;

    public int buttonnumber = 8;

	// Use this for initialization
	void Start () {
        //finds slots and fills the "slots" variable
        buttons = new GameObject[buttonnumber];
        for (int i = 0; i < buttonnumber; i++)
        {
            string name = "Button (" + i.ToString() + ")";
            buttons[i] = GameObject.Find(name);
        }

       // Status = GameObject.Find("Status_Panel");
        Status.SetActive(false);

        //Skill = GameObject.Find("Skill_Panel");
        Skill.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //enable buttons
    //called when the ask window disappears
    private void enableButtons()
    {
        for (int i = 0; i < buttonnumber; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }

    //disable buttons
    //called when the ask window appears
    private void disableButtons()
    {
        for (int i = 0; i < buttonnumber; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    //called when the "go to title" button is pressed
    //shows ask window and disables buttons
    public void goTitleButton()
    {
        disableButtons();
        ask.SetActive(true);
    }

    //when the player selected no
    //closes the ask window and enable slot buttons
    public void no()
    {
        ask.SetActive(false);
        enableButtons();
    }

    //when the player selected yes
    //resets scene informations and go to the title
    public void goTitle()
    {
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();//find the GlobalControl object
        gc.reset();
        SceneManager.LoadScene("title");
    }

    public void disactiveOthers()
    {
        Status.SetActive(false);
        Skill.SetActive(false);
    }

    public void StatusButton()
    {
        disactiveOthers();
        Status.SetActive(true);
    }

    public void SkillButton()
    {
        disactiveOthers();
        Skill.SetActive(true);
    }

}
