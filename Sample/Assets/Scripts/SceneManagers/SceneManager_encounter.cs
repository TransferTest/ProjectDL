using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this is scene manager for "encounter" scene
public class SceneManager_encounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //loads result scene
    public void loadResult()
    {
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        gc.save();
        SceneManager.LoadScene("result");
    }
}
