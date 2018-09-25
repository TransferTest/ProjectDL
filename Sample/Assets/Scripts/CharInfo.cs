using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this object stores characters available and current state of player
//it is for passing characters' information through scenes
public class CharInfo {
    public List<Char> characters;//list of characters available now
    public List<Char> party;
    public int money;//money that player has

    public CharInfo()
    {
        characters = new List<Char>();
        party = new List<Char>();
        generateRandomParty();
        money = 0;
    }

    //initiallizes with values. used when a save file is loaded
    public CharInfo(List<Char> characters, int money)
    {
        this.characters = characters;
        this.money = money;
    }

    public void generateRandomParty()
    {
        Char c1 = new Char();
        c1.id = 0;
        c1.reinitialize(false); // This changes the character from the generic type to the specific sephira;
        characters.Add(c1);
        party.Add(c1);
        Char c2 = new Char();
        c2.id = 1;
        c2.reinitialize(false);
        characters.Add(c2);
        party.Add(c2);
        Char c3 = new Char();
        c3.id = 2;
        c3.reinitialize(false);
        characters.Add(c3);
        party.Add(c3);
        Char c4 = new Char();
        c4.id = 3;
        c4.reinitialize(false);
        characters.Add(c4);
        party.Add(c4);
    }
}
