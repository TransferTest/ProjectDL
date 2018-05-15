using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this object stores characters available and current state of player
//it is for passing characters' information through scenes
public class CharInfo {
    public List<Char> characters;//list of characters available now
    public int money;//money that player has

    public CharInfo()
    {
        characters = new List<Char>();
        money = 0;
    }

    //initiallizes with values. used when a save file is loaded
    public CharInfo(List<Char> characters, int money)
    {
        this.characters = characters;
        this.money = money;
    }
}
