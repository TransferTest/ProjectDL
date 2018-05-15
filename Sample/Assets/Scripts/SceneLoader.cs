using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this class includes functions for loading scenes
public class SceneLoader : MonoBehaviour {
    //initiallize
    //restores scene information if there is any information to restore from GlobalControl object
    void Start ()
    {
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

    //load scene with name
    public void loadScene(string name)
    {
        string curName = SceneManager.GetActiveScene().name;

        //saves current scene information
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        gc.scenes.Push(new SceneInfo(curName));
        gc.sceneInformation = null;
        gc.save();

        SceneManager.LoadScene(name);
    }

    //load previous scene
    public void loadBack()
    {
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        if (gc.scenes.Count <= 0)//when there is no previous scene
        {
            Debug.Log("There is no previous scene");
            return;
        }
        SceneInfo si = gc.scenes.Pop();//the previous scene
        string sName = si.sceneName;//gets the previous scene's name
        
        gc.save();//save current data
        gc.sceneInformation = si;//passes scene information to restore
        SceneManager.LoadScene(sName);
    }
}
