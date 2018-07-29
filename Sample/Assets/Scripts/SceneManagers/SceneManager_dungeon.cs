using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_dungeon : MonoBehaviour {
    public GameObject encounterButton;
    public GameObject enemy;
    public GameObject mapobj;
    public GameObject obstacle;
    public GameObject road;
    public GameObject player;
    public GameObject cam;
    public int cam_x, cam_y, cam_width, cam_height;
    public int init_x, init_y;
    public int width, height;
    public float w, h;
    public int start_x, start_y;

    public float walkingSpeed;

    private float progress, dist;
    private float pre_x, pre_y;
    private float des_x, des_y;
    private bool anim;
    private int pos_x, pos_y;

    private SpriteRenderer p_sprite;

    private MapTile[,] map;
    private Stack<MapTile> path;
    private List<Enemy_map> enemies;
    private bool metEnemy;

    private Enemy_map encounter;

	// Use this for initialization
	void Start () {
        p_sprite = player.GetComponentInChildren<SpriteRenderer>();

        path = new Stack<MapTile>();
        anim = false;
        pos_x = init_x;
        pos_y = init_y;
        metEnemy = false;

        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();//find the GlobalControl object
        SceneInfo_dungeon si = (SceneInfo_dungeon)gc.sceneInformation;

        Debug.Log("Load Scene");

        if (si == null)//if there is no scene information (it might means this scene is first loaded)
        {
            Debug.Log("no scene information");
            enemies = new List<Enemy_map>();
            enemies.Add(new Enemy_map(10, 15));
            enemies.Add(new Enemy_map(4, 5));
            enemies.Add(new Enemy_map(1, 7));
        }
        else//if there is scene information
        {
            loadInfo_dungeon(si);
            Debug.Log("loaded");
        }
        
        player.transform.Translate(new Vector2(start_x + w * (float)pos_x, start_y + h * (float)pos_y) - new Vector2(player.transform.position.x, player.transform.position.y));
        syncCamera();
        generateMap();

        setEnemies();
	}
	
	// Update is called once per frame
	void Update () {
        if (metEnemy)
            return;
        if (anim)
        {
            move();
            return;
        }
        
        if (path.Count > 0)
        {
            MapTile next = path.Pop();
            
            pre_x = player.transform.position.x;
            pre_y = player.transform.position.y;

            des_x = start_x + next.x * w;
            des_y = start_y + next.y * h;

            pos_x = next.x;
            pos_y = next.y;

            if (next.x == pos_x)
                dist = h;
            else
                dist = w;

            progress = 0;

            anim = true;
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            int dx, dy;
            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dx = (int)((v.x + w / 2) / w);
            dy = (int)((v.y + h / 2) / h);
            if (map[dx, dy].ch == 1)
                return;
            findPath(dx, dy);
            return;
        }

        if (Input.GetAxis("Hor") < 0)
        {
            if (pos_x == 0)
                return;
            if (map[pos_x - 1, pos_y].ch == 1)
                return;
            anim = true;
            pre_x = player.transform.position.x;
            pre_y = player.transform.position.y;
            des_x = pre_x - w;
            des_y = pre_y;
            dist = w;
            pos_x = pos_x - 1;
            progress = 0;
            return;
        }

        if (Input.GetAxis("Hor") > 0)
        {
            if (pos_x == width - 1)
                return;
            if (map[pos_x + 1, pos_y].ch == 1)
                return;
            anim = true;
            pre_x = player.transform.position.x;
            pre_y = player.transform.position.y;
            des_x = pre_x + w;
            des_y = pre_y;
            dist = w;
            pos_x = pos_x + 1;
            progress = 0;
            return;
        }

        if (Input.GetAxis("Ver") < 0)
        {
            if (pos_y == 0)
                return;
            if (map[pos_x, pos_y - 1].ch == 1)
                return;
            anim = true;
            pre_x = player.transform.position.x;
            pre_y = player.transform.position.y;
            des_x = pre_x;
            des_y = pre_y - h;
            dist = w;
            pos_y = pos_y - 1;
            progress = 0;
            return;
        }

        if (Input.GetAxis("Ver") > 0)
        {
            if (pos_y == height - 1)
                return;
            if (map[pos_x, pos_y + 1].ch == 1)
                return;
            anim = true;
            pre_x = player.transform.position.x;
            pre_y = player.transform.position.y;
            des_x = pre_x;
            des_y = pre_y + h;
            dist = w;
            pos_y = pos_y + 1;
            progress = 0;
            return;
        }
    }

    private void setEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy_map newen = enemies[i];
            GameObject en = GameObject.Instantiate(enemy);
            //en.transform.SetParent(mapobj.transform);
            en.transform.Translate(new Vector2(start_x + newen.x * w, start_y + newen.y * h));
            map[newen.x, newen.y].ch = 2;
        }
    }

    private int checkEncounter()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].x == pos_x && enemies[i].y == pos_y)
                return i;
        }
        return -1;
    }

    private void generateMap ()
    {
        map = new MapTile[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                    GameObject newtile = GameObject.Instantiate(obstacle);
                    newtile.transform.SetParent(mapobj.transform);
                    newtile.transform.Translate(new Vector2(start_x + i * w, start_y + j * h));
                    map[i, j] = new MapTile(1, newtile);
                    map[i, j].x = i;
                    map[i, j].y = j;
                }
                else
                {
                    GameObject newtile = GameObject.Instantiate(road);
                    newtile.transform.SetParent(mapobj.transform);
                    newtile.transform.Translate(new Vector2(start_x + i * w, start_y + j * h));
                    map[i, j] = new MapTile(0, newtile);
                    map[i, j].x = i;
                    map[i, j].y = j;
                }
            }
        checkConnection();
    }

    private void move()
    {
        if (des_x - pre_x > 0.01)
        {
            p_sprite.flipX = false;
        }
        else if (des_x - pre_x < -0.01)
        {
            p_sprite.flipX = true;
        }
        Debug.Log("moving " + progress + " / " + dist);
        progress += Time.deltaTime * walkingSpeed;
        if (progress >= dist)
        {
            anim = false;
            if (path.Count == 0)
                player.transform.Translate(new Vector2(des_x, des_y) - new Vector2(player.transform.position.x, player.transform.position.y));
            int enc = checkEncounter();
            if (enc != -1)
            {
                metEnemy = true;
                encounterButton.SetActive(true);
                encounter = enemies[enc];
                enemies.RemoveAt(enc);
            }
        }
        else
        {
            float po_x = ((dist - progress) * pre_x + progress * des_x) / dist;
            float po_y = ((dist - progress) * pre_y + progress * des_y) / dist;
            player.transform.Translate(new Vector2(po_x, po_y) - new Vector2(player.transform.position.x, player.transform.position.y));
        }

        syncCamera();
    }

    private void checkConnection ()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                MapTile cur = map[i, j];
                if (i != 0)
                {
                    MapTile l = map[i - 1, j];
                    if (l.ch == 0)
                        cur.left = l;
                }
                if (j != 0)
                {
                    MapTile u = map[i, j - 1];
                    if (u.ch == 0)
                        cur.up = u;
                }
                if (i != width - 1)
                {
                    MapTile r = map[i + 1, j];
                    if (r.ch == 0)
                        cur.right = r;
                }
                if (j != height - 1)
                {
                    MapTile d = map[i, j + 1];
                    if (d.ch == 0)
                        cur.down = d;
                }
            }
        }
    }
    
    private void findPath (int dx, int dy)
    {
        int sx = pos_x;
        int sy = pos_y;

        List<MapTile> R = new List<MapTile>();//checking

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                map[i, j].previous = null;
                map[i, j].check = false;
            }
        map[sx, sy].check = true;
        R.Add(map[sx, sy]);
        while (R.Count > 0)
        {
            MapTile cur = R[0];
            R.RemoveAt(0);
            if (cur.x == dx && cur.y == dy)
                break;

            if (cur.ch == 2)
                continue;

            MapTile up = cur.up;
            MapTile down = cur.down;
            MapTile right = cur.right;
            MapTile left = cur.left;

            if (up != null)
            {
                if (!up.check)
                {
                    up.previous = cur;
                    up.check = true;
                    R.Add(up);
                }
            }
            if (down != null)
            {
                if (!down.check)
                {
                    down.previous = cur;
                    down.check = true;
                    R.Add(down);
                }
            }
            if (right != null)
            {
                if (!right.check)
                {
                    right.previous = cur;
                    right.check = true;
                    R.Add(right);
                }
            }
            if (left != null)
            {
                if (!left.check)
                {
                    left.previous = cur;
                    left.check = true;
                    R.Add(left);
                }
            }
        }

        if (map[dx, dy].previous == null)
            return;
        MapTile d = map[dx, dy];
        while (d != null)
        {
            path.Push(d);
            d = d.previous;
        }
        path.Pop();
    }

    private void syncCamera ()
    {
        cam.transform.Translate(player.transform.position - cam.transform.position + new Vector3(0, 0, -5));
        if (cam.transform.position.x < cam_x + 640)
        {
            cam.transform.Translate(new Vector2(cam_x + 640, 0) - new Vector2(cam.transform.position.x, 0));
        }
        if (cam.transform.position.x > cam_x + cam_width - 640)
        {
            cam.transform.Translate(new Vector2(cam_x + cam_width - 640, 0) - new Vector2(cam.transform.position.x, 0));
        }
        if (cam.transform.position.y < cam_y + 360)
        {
            cam.transform.Translate(new Vector2(0, cam_y + 360) - new Vector2(0, cam.transform.position.y));
        }
        if (cam.transform.position.y > cam_y + cam_height - 360)
        {
            cam.transform.Translate(new Vector2(0, cam_y + cam_height - 360) - new Vector2(0, cam.transform.position.y));
        }
    }

    public void loadEncounter()
    {
        string curName = SceneManager.GetActiveScene().name;

        //saves current scene information
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        SceneInfo_dungeon curscene = new SceneInfo_dungeon();
        curscene.pos_x = pos_x;
        curscene.pos_y = pos_y;
        curscene.enemies = enemies;

        gc.scenes.Push(curscene);
        SceneInfo_encounter nsi = new SceneInfo_encounter();
        nsi.monsters = encounter.monsters;
        gc.sceneInformation = nsi;
        gc.save();

        SceneManager.LoadScene("encounter");
    }
    public void loadInfo_dungeon (SceneInfo_dungeon info)
    {
        pos_x = info.pos_x;
        pos_y = info.pos_y;
        enemies = info.enemies;
    }
}
