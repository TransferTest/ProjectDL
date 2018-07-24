using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{
    private int actionId;//0: attack, 1: skill, 2: item...
    private int who;
    private int target;
    private List<monster> monsters;
    private List<Char> party;

    public Action (int id, int who, int target, List<monster> monsters, List<Char> party)
    {
        actionId = id;
        this.who = who;
        this.target = target;
        this.monsters = monsters;
        this.party = party;
    }

    public void animate(List<Animator> effect_monster, List<Animator> effect_nakama)
    {
        if (actionId == 0)
        {
            effect_monster[target].SetTrigger("attack");
            effect_nakama[who].SetTrigger("heal");
        }
    }

    public void doAction()
    {
        if (actionId == 0)
        {
            monsters[target].HP -= 100;
            monsters[target].HP_bar.value = monsters[target].HP;
        }
    }
}
