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
    public int CMULT; // character's critical multiplier
    public int requiredMP, requiredHP;
    public int TYPE;
    public List<int> Malkuth_ATK = new List<int> {0, 35, 36, 36, 36, 36, 36, 36, 36, 36, 37, 37, 37, 37, 38, 38, 38, 38, 38, 39, 40, 40, 40, 40, 40, 41, 41, 41, 41, 41, 41 };
    public List<int> Malkuth_AGI = new List<int> {0, 36, 36, 37, 37, 38, 38, 39, 39, 39, 39, 39, 40, 40, 40, 41, 41, 42, 42, 42, 42, 42, 43, 43, 44, 44, 44, 45, 45, 46, 47 };
    public List<int> Malkuth_CRI = new List<int> {0, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15 };
    public List<int> Malkuth_INT = new List<int> {0, 32, 32, 32, 32, 32, 32, 32, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 35, 35, 35, 35, 35, 35, 35, 35 };
    public List<int> Malkuth_DEF = new List<int> {0, 28, 28, 28, 29, 29, 29, 29, 29, 30, 30, 30, 30, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 32, 32, 32 };
    public List<int> Yesod_ATK = new List<int> {0, 36, 37, 37, 38, 38, 38, 38, 39, 39, 40, 40, 40, 41, 41, 42, 42, 43, 43, 43, 44, 44, 44, 45, 46, 46, 46, 46, 47, 47, 48 };
    public List<int> Yesod_AGI = new List<int> {0, 32, 32, 32, 32, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34 };
    public List<int> Yesod_CRI = new List<int> {0, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15 };
    public List<int> Yesod_INT = new List<int> {0, 28, 28, 28, 28, 28, 28, 28, 28, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 30, 30, 30, 30, 30, 30, 30, 30, 31, 31, 31, 31 };
    public List<int> Yesod_DEF = new List<int> {0, 35, 35, 36, 36, 36, 36, 37, 37, 37, 37, 37, 38, 38, 38, 38, 38, 38, 39, 39, 39, 39, 40, 40, 40, 41, 41, 41, 41, 42, 42 };
    public List<int> Hod_ATK = new List<int> {0, 32, 32, 32, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 34, 35, 35, 35, 35, 36, 36, 36, 37, 37, 37, 37, 37, 37, 37, 37 };
    public List<int> Hod_AGI = new List<int> {0, 35, 36, 36, 36, 36, 36, 37, 37, 37, 37, 38, 38, 38, 39, 39, 39, 40, 40, 40, 40, 40, 41, 41, 41, 41, 42, 42, 42, 43, 43 };
    public List<int> Hod_CRI = new List<int> {0, 15, 15, 16, 16, 16, 17, 17, 17, 18, 18, 18, 19, 19, 19, 20, 20, 20, 21, 21, 21, 22, 22, 22, 23, 23, 23, 24, 24, 24, 25 };
    public List<int> Hod_INT = new List<int> {0, 31, 31, 31, 31, 32, 32, 32, 32, 32, 32, 32, 32, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 34 };
    public List<int> Hod_DEF = new List<int> {0, 28, 28, 28, 28, 28, 28, 28, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 30, 30, 30, 30, 30, 30, 30, 30, 30, 31, 31, 31 };
    public List<int> Netzah_ATK = new List<int> {0, 29, 29, 30, 30, 30, 30, 30, 31, 31, 32, 32, 32, 32, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 34, 34, 34 };
    public List<int> Netzah_AGI = new List<int> {0, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 34, 35, 35, 35, 35, 35, 36, 36, 37, 37, 37, 37, 37, 37, 37, 37, 37, 38, 38 };
    public List<int> Netzah_CRI = new List<int> {0, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15 };
    public List<int> Netzah_INT = new List<int> {0, 37, 38, 38, 38, 39, 39, 39, 40, 40, 40, 40, 41, 41, 41, 42, 42, 43, 43, 43, 43, 43, 44, 44, 45, 45, 45, 46, 47, 47, 48 };
    public List<int> Netzah_DEF = new List<int> {0, 32, 32, 32, 32, 32, 32, 32, 32, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 34, 34, 34, 34, 34, 35, 35, 35, 35, 35, 35 };
    //initiallize
    public Char ()
    {
        name = "dummy";//this character's name will be "dummy" if you don't set name
        Lv = 1;
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
        CMULT = 0;
        requiredMP = 16;
        requiredHP = 0;
        TYPE = 15;
    }
    public void reinitialize(bool lvlup) // This function will be called during character creation and after every level up
    {
        if (lvlup) { // If the reinitialize function is called by leveling up, set lvlup to true.
            Lv += 1;
        }
        if (id ==0) // 말쿠트
        {
            name = "Malkuth";
            HP = 170 + 11 * Lv;
            HP_max= 170 + 11 * Lv;
            MP = 67 + 3 * Lv;
            MP_max = 67 + 3 * Lv;
            ATK = Malkuth_ATK[Lv];
            AGI = Malkuth_AGI[Lv];
            DEF = Malkuth_DEF[Lv];
            INT = Malkuth_INT[Lv];
            CRI = Malkuth_CRI[Lv];

        }
        if (id == 1) // 예소드
        {
            name = "Yesod";
            HP = 180 + 14 * Lv;
            HP_max = 180 + 14 * Lv;
            MP = 78 + 3 * Lv;
            MP_max = 78 + 3 * Lv;
            ATK = Yesod_ATK[Lv];
            AGI = Yesod_AGI[Lv];
            DEF = Yesod_DEF[Lv];
            INT = Yesod_INT[Lv];
            CRI = Yesod_CRI[Lv];

        }
        if (id == 2) // 호드
        {
            name = "Hod";
            HP = 180 + 9 * Lv;
            HP_max = 180 + 9 * Lv;
            MP = 50 + 3 * Lv;
            MP_max = 50 + 3 * Lv;
            ATK = Hod_ATK[Lv];
            AGI = Hod_AGI[Lv];
            DEF = Hod_DEF[Lv];
            INT = Hod_INT[Lv];
            CRI = Hod_CRI[Lv];

        }
        if (id == 3) // 네짜흐
        {
            name = "Netzah";
            HP = 170 + 12 * Lv;
            HP_max = 170 + 12 * Lv;
            MP = 90 + 3 * Lv;
            MP_max = 90 + 3 * Lv;
            ATK = Netzah_ATK[Lv];
            AGI = Netzah_AGI[Lv];
            DEF = Netzah_DEF[Lv];
            INT = Netzah_INT[Lv];
            CRI = Netzah_CRI[Lv];
        }
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
        HP -= requiredHP;
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
