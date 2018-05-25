using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile{
    public GameObject tile;
    public int ch;
    public MapTile up, down, left, right;
    public MapTile previous;
    public int x, y;
    public bool check;
    public MapTile (int ch, GameObject tile)
    {
        this.ch = ch;
        this.tile = tile;
    }
}
