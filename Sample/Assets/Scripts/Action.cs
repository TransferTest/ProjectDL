using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{
    private int actionId;//0: attack, 1: skill, 2: item...
    private int who;
    private int target;
    private Item it;
    private List<monster> monsters;
    private List<Char> party;
    List<List<float>> teff= new List<List<float>>() {
            new List<float> { 1f, 0.5f, 1.5f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1.5f, 1f, 0.5f }, // Fire
            new List<float> { 1.5f, 1f, 0.5f, 1f, 1f, 1.5f, 1f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1f, 0.5f }, // Wind
            new List<float> { 0.5f, 1.5f, 1f, 1f, 1f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 1.5f, 1f, 1f, 0.5f }, // Ice
            new List<float> { 1f, 1f, 1f, 1f, 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1.5f, 1.5f, 0.5f }, // Light
            new List<float> { 1f, 1f, 1f, 1.5f, 1f, 1f, 1f, 1f, 1.5f, 1.5f, 1.5f, 1f, 1f, 1f, 1.5f, 0.5f }, // Dark
            new List<float> { 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f }, // Fire/Wind
            new List<float> { 0.5f, 1f, 0.5f, 1f, 1f, 0.5f, 1f, 1.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f }, // Wind/Ice
            new List<float> { 0.5f, 0.5f, 1f, 1f, 1f, 1.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f }, // Ice/Fire
            new List<float> { 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 1.5f, 1.5f, 1f, 1f, 1f, 0.5f }, // Light/Fire
            new List<float> { 1f, 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1f, 0.5f, 1f, 1.5f, 1f, 1f, 0.5f }, // Light/Wind
            new List<float> { 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 1.5f, 1f, 1f, 1f, 1.5f, 1f, 0.5f }, // Light/Ice
            new List<float> { 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1f, 1f, 1f, 0.5f, 1.5f, 1f, 0.5f }, // Dark/Fire
            new List<float> { 1f, 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 1f, 1.5f, 1f, 0.5f, 1f, 0.5f }, // Dark/Wind
            new List<float> { 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f, 0.5f, 1.5f, 1f, 1f, 0.5f}, // Dark/Ice
            new List<float> { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1.5f , 0.5f}, // Light/Dark
            new List<float> {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f } //Debug
    };
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
            int basedam= (int)System.Math.Ceiling((teff[party[who].TYPE][monsters[target].TYPE] * (party[who].ATK) * Random.Range(85, 115) / 100));
            if (Random.Range(0, 100) >= 100 - party[who].CRI * party[who].Lv / monsters[target].Lv) // Critical Chance
            {
                basedam = (int)basedam * (15 + (10 * party[who].CMULT / 100)) / 10;
            }
            if (Random.Range(0, 1000) < System.Math.Min(18 * monsters[target].AGI / (monsters[target].AGI + party[who].AGI), 10)) // Evade
            {
                basedam = 0;
            }
            monsters[target].HP -= System.Math.Max(basedam -monsters[target].DEF,0);
            //monsters[target].HP_bar.value = monsters[target].HP;
        }
        if (actionId == 1)
        {
            party[who].skill(monsters, party, target);
        }
    }
}
