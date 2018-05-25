using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo_dungeon : SceneInfo {
    public int pos_x, pos_y;
    public List<Enemy_map> enemies;

    public SceneInfo_dungeon()
    {
        sceneName = "dungeon";
    }
}
