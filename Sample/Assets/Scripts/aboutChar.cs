using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class aboutChar : MonoBehaviour {
    public string name;
    public int HP;
    public int MP;
    public int nowEXP;
    public int nextEXP;
    public int ATK;
    public int AGI;
    public int DEF;
    public int INT;
    public int CRI;
    public string chardetails;
    
    public void setChar ()
    {
        name = "홍길동";
        HP = 100;
        MP = 10;
        nowEXP = 30;
        nextEXP = 100;
        ATK = 10;
        AGI = 10;
        DEF = 10;
        INT = 10;
        CRI = 10;
        chardetails = "임시 캐릭터 홍길동입니다.";
    }
}
