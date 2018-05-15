using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this object survives through scenes
//it stores scene information, character information, and stack of previous scenes
public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public Stack<SceneInfo> scenes;//stack of previous scenes

    public SceneInfo sceneInformation;//current scene information
    public CharInfo charInformation;//current character information

    public string text;//text for testing whether it is still alive

    //initiallize
    void Start()
    {
        Debug.Log("Newly started");
        text = GlobalControl.Instance.text;
        scenes = GlobalControl.Instance.scenes;
        if (scenes == null)
            scenes = new Stack<SceneInfo>();
        charInformation = GlobalControl.Instance.charInformation;
        if (charInformation == null)
            charInformation = new CharInfo();
        sceneInformation = GlobalControl.Instance.sceneInformation;
    }

    //I don't know what it is
    //I guess it removes another GlobalControl in the parallel world and replaces with itself
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //saves current information
    public void save()
    {
        GlobalControl.Instance.text = text;
        GlobalControl.Instance.scenes = scenes;
        GlobalControl.Instance.sceneInformation = sceneInformation;
        GlobalControl.Instance.charInformation = charInformation;
    }

    //resets all informations
    //called when the user goes back to the title
    public void reset()
    {
        Debug.Log("Reset scene information");

        GlobalControl.Instance.text = "";
        GlobalControl.Instance.scenes = new Stack<SceneInfo>();
        GlobalControl.Instance.sceneInformation = null;
        GlobalControl.Instance.charInformation = null;

        text = GlobalControl.Instance.text;
        scenes = GlobalControl.Instance.scenes;
        if (scenes == null)
            scenes = new Stack<SceneInfo>();
        if (charInformation == null)
            charInformation = new CharInfo();
        sceneInformation = GlobalControl.Instance.sceneInformation;
    }
}