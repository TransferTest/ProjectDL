using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is for storing a character's information
public abstract class Char{
    public Weapon wea;//character's weapon
    public string name;//character's name
    public int Lv, SLv;//character's level and sefirah level
    public int exp, exp_next;//character's current exp and needed exp to the next level
    public int HP, MP;//character's current status
    public int HP_max, MP_max;//character's maximum status
    public int ATK, AGI, CRI, INT, DEF;//character's status
    //initiallize
    public Char ()
    {
        name = "dummy";//this character's name will be "dummy" if you don't set name
    }

    abstract public void skill();//character's skill
}
