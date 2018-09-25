﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{
    private int actionId;//0: attack, 1: skill, 2: item...
    private int who;
    private int target;
    private Item it;
    private List<monster> monsters;
    private List<Char> party;

    public Action (int id, int who, int target, List<monster> monsters, List<Char> party, Item it)
    {
        actionId = id;
        this.who = who;
        this.target = target;
        this.monsters = monsters;
        this.party = party;
        this.it = it;
    }

    public void animate(List<Animator> effect_monster, List<Animator> effect_nakama)
    {
        if (actionId == 0)
        {
            effect_monster[target].SetTrigger("attack");
            effect_nakama[who].SetTrigger("skill");
        }
        else if (actionId == 1)
        {
            int stype = party[who].type_skill();
            if (stype == 0)
            {
                effect_monster[target].SetTrigger("attack");
                effect_nakama[who].SetTrigger("skill");
            }
            else if (stype == 1)
            {
                effect_nakama[target].SetTrigger("heal");
                effect_nakama[who].SetTrigger("skill");
            }
            else
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    effect_monster[i].SetTrigger("attack");
                }
                effect_nakama[who].SetTrigger("skill");
            }
        }
    }

    public void doAction()
    {
        if (actionId == 0)
        {
            monsters[target].HP -= 100;
            //monsters[target].HP_bar.value = monsters[target].HP;
        }
        if (actionId == 1)
        {
            party[who].skill(monsters, party, target);
        }
    }
}