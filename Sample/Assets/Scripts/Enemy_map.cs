using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_map{
    public int x, y;

    public int num_enemy;
    public List<monster> monsters;
    
    public Enemy_map(int x, int y)
    {
        this.x = x;
        this.y = y;

        monsters = new List<monster>();

        generateRandomEnemy();
    }

    private void generateRandomEnemy ()
    {
        num_enemy = Random.Range(1, 4);
        for (int i = 0; i < num_enemy; i ++)
        {
            monster newM = new monster();
            newM.id = Random.Range(1, 7);
            monsters.Add(newM);
        }
    }
}
