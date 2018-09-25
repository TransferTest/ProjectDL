using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monster {
    public int id;

    public int HP;
    public int ATK;
    public int AGI;
    public int CRI;
    public int INT;
    public int DEF;
    public int TYPE;

    public Button select;
    public Slider HP_bar;

    public SpriteRenderer sprite;
    public Animator anim;

    public GameObject effect;
    public Animator effAnim;

    public GameObject obj;

    public bool attacked;

    public monster ()
    {
        id = 0;
        HP = 500;
        ATK = 50;
        AGI = 10;
        CRI = 10;
        INT = 1;
        DEF = 5;
        TYPE = 0;

        attacked = false;
    }
}
