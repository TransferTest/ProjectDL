using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is scene manager for "save" scene
public class SceneManager_save : MonoBehaviour {
    public GameObject ask;
    public int slotnumber = 5;
    private GameObject[] slots;

    private int slotNum;

	// Use this for initialization
	void Start () {
        //finds slots and fills the "slots" variable
        slots = new GameObject[slotnumber];
        for (int i = 0; i < slotnumber; i++)
        {
            string name = "Save_0" + (i + 1).ToString();
            slots[i] = GameObject.Find(name);
        }
        //we should check which slot is empty, and brief information for each not-empty slot
        //and show that information on the slot buttons
    }

    // Update is called once per frame
    void Update () {
		
	}

    //enable slot buttons
    //called when the ask window is closed
    private void enableButtons()
    {
        for (int i = 0; i < slotnumber; i++)
        {
            slots[i].GetComponent<Button>().interactable = true;
        }
    }

    //disable slot buttons
    //called when the ask window is shown
    private void disableButtons()
    {
        for (int i = 0; i < slotnumber; i++)
        {
            slots[i].GetComponent<Button>().interactable = false;
        }
    }

    //called when a slot button is clicked
    //sets "slotNum" variable
    //shows ask window
    public void save(int slotNum)
    {
        this.slotNum = slotNum;
        disableButtons();
        ask.SetActive(true);
    }

    //when the user selected no
    //closes the ask window and enable slot buttons
    public void no ()
    {
        ask.SetActive(false);
        enableButtons();
    }

    //when the user selected yes
    //saves current state to save file
    //right now, there is no save file
    public void yes()
    {
        ask.SetActive(false);
        enableButtons();
    }

    //read save files
    private void readFiles()
    {

    }
}
