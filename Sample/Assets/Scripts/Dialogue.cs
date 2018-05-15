using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is dialogue class
public class Dialogue {

    public string name;
    public string content;
    public int selectionNum;
    public string[] selection;
    private Dialogue[] next;
    
    public Dialogue()
    {
        content = "";
        selectionNum = 0;
    }
    public Dialogue(string content)
    {
        this.name = "dummy";
        this.content = content;
        selectionNum = 0;
    }
    public Dialogue(string name, string content)
    {
        this.name = name;
        this.content = content;
        selectionNum = 0;
    }

    public void setNext(Dialogue next)
    {
        this.selectionNum = 0;
        this.next = new Dialogue[1];
        this.next[0] = next;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public bool setSelection(string[] selection, Dialogue[] next)
    {
        if (selection.Length != next.Length)
            return false;
        this.selectionNum = selection.Length;
        this.selection = selection;
        this.next = next;
        return true;
    }

    public Dialogue getNext(int n)
    {
        if (next == null)
            return null;
        if (next.Length == 0)//there is no more script
            return null;
        if (selectionNum == 0)//there is no selection
            return next[0];
        if (n >= selectionNum)//wrong input
            return null;
        return next[n];//selected n-th

    }
}
