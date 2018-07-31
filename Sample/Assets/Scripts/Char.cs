using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is for storing a character's information
//완전 대단한게 생각났습니다.
//캐릭터 4명의 스킬을 그냥 다 여기 때려박는거에요.
//if else 떡칠하면 어떻게든 되겠죠.
public class Char{
    public Weapon wea;//character's weapon
    public string name;//character's name
    public int id;//식별을 편하게 하기 위해서 id를 따로 만들었습니다.
    public int Lv, SLv;//character's level and sefirah level
    public int exp, exp_next;//character's current exp and needed exp to the next level
    public int HP, MP;//character's current status
    public int HP_max, MP_max;//character's maximum status
    public int ATK, AGI, CRI, INT, DEF;//character's status
    public int requiredMP;
    //initiallize
    public Char ()
    {
        name = "dummy";//this character's name will be "dummy" if you don't set name
        id = 0;
        HP = 90;
        HP_max = 90;
        MP = 60;
        MP_max = 60;
        exp = 30;
        exp_next = 100;
        ATK = 10;
        AGI = 10;
        DEF = 10;
        INT = 10;
        CRI = 10;
        requiredMP = 16;
    }

    public int type_skill ()//해당 캐릭터의 스킬이 적 대상인지 아군 대상인지 논타겟인지 알려줍니다. 0->적, 1->아군, 2->논타겟
    {
        if (id == 0)
            return 0;
        if (id == 1)
            return 1;
        if (id == 2)
            return 0;
        return 2;
    }

    public void skill(List<monster> monsters, List<Char> party, int target)
    {
        MP -= requiredMP;
        if (id == 0)
        {
            skill_00(monsters, party, target);
            return;
        }
        if (id == 1)
        {
            skill_01(monsters, party, target);
            return;
        }
        if (id == 2)
        {
            skill_02(monsters, party, target);
            return;
        }
        if (id == 3)
        {
            skill_03(monsters, party);
            return;
        }
    }

    private void skill_00 (List<monster> monsters, List<Char> party, int target)//0번 캐릭터의 스킬입니다. 적에게 쓰는 스킬입니다.
    {
        monsters[target].HP -= 200;
        Debug.Log("skill 0");
    }

    private void skill_01 (List<monster> monsters, List<Char> party, int target)//1번 캐릭터의 스킬입니다. 아군에게 쓰는 스킬입니다.
    {
        party[target].HP += 10;
        if (party[target].HP > party[target].HP_max)
            party[target].HP = party[target].HP_max;
        Debug.Log("skill 1");
    }

    private void skill_02(List<monster> monsters, List<Char> party, int target)//2번 캐릭터의 스킬입니다. 적에게 쓰는 스킬입니다.
    {
        monsters[target].HP -= 140;
        HP += 40;
        if (HP > HP_max)
            HP = HP_max;
        Debug.Log("skill 2");
    }

    private void skill_03(List<monster> monsters, List<Char> party)//3번 캐릭터의 스킬입니다. 논타겟 스킬입니다.
    {
        for (int i = 0; i < monsters.Count; i++)
            monsters[i].HP -= 110;
        Debug.Log("skill 3");
    }
}
