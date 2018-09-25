using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public int id;//0: empty, 1: HP, 2: MP, 3 : Material 
    public int count;

    public string Name;
    public Sprite Icon;
    public string detail;

    public Item ()
    {
        id = 0;
        count = 0;
        Name = "None";
        detail = "None";
    }

    public Item (int new_id, int new_count, string new_Name, string new_detail)
    {
        id = new_id;
        count = new_count;

        Name = new_Name;
        detail = new_detail;
    }

    public int type_item()//해당 아이템의 효과가 적 대상인지 아군 대상인지 논타겟인지 알려줍니다. 0->적, 1->아군, 2->논타겟
    {
        if (id == 0)
            return 0;
        if (id == 1)
            return 1;
        if (id == 2)
            return 1;
        return 2;
    }
    public void use (List<Char> party, int target)
    {
        if (id == 0)
            return;
        else if (id == 1)
            use_01(party, target);
        else if (id == 2)
            use_02(party, target);
    }
    private void use_01 (List<Char> party, int target)
    {
        party[target].HP += 10;
        if (party[target].HP > party[target].HP_max)
            party[target].HP = party[target].HP_max;
    }
    private void use_02(List<Char> party, int target)
    {
        party[target].MP += 10;
        if (party[target].MP > party[target].MP_max)
            party[target].MP = party[target].MP_max;
    }
}
