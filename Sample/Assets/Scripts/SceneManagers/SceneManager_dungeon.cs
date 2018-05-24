using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_dungeon : MonoBehaviour {
    public GameObject mapobj;
    public GameObject obstacle;
    public GameObject road;
    public GameObject player;
    public GameObject camera;
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

    private MapTile[,] map;

	// Use this for initialization
	void Start () {
        anim = false;
        pos_x = init_x;
        pos_y = init_y;

        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();//find the GlobalControl object
        SceneInfo_dungeon si = (SceneInfo_dungeon)gc.sceneInformation;

        Debug.Log("Load Scene");

        if (si == null)//if there is no scene information (it might means this scene is first loaded)
        {
            Debug.Log("no scene information");
        }
        else//if there is scene information
        {
            loadInfo_dungeon(si);
            Debug.Log("loaded");
        }

        player.transform.Translate(new Vector2(start_x + w * (float)pos_x, start_y + h * (float)pos_y) - new Vector2(player.transform.position.x, player.transform.position.y));
        syncCamera();
        generateMap();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim)
        {
            Debug.Log("moving " + progress + " / " + dist);
            progress += Time.deltaTime * walkingSpeed;
            if (progress >= dist)
            {
                anim = false;
                player.transform.Translate(new Vector2(des_x, des_y) - new Vector2(player.transform.position.x, player.transform.position.y));
            }
            else
            {
                float po_x = ((dist - progress) * pre_x + progress * des_x) / dist;
                float po_y = ((dist - progress) * pre_y + progress * des_y) / dist;
                player.transform.Translate(new Vector2(po_x, po_y) - new Vector2(player.transform.position.x, player.transform.position.y));
            }

            syncCamera();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.A))
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
        }

        if (Input.GetKeyDown(KeyCode.D))
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
        }

        if (Input.GetKeyDown(KeyCode.S))
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
        }

        if (Input.GetKeyDown(KeyCode.W))
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
        }
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
                }
                else
                {
                    GameObject newtile = GameObject.Instantiate(road);
                    newtile.transform.SetParent(mapobj.transform);
                    newtile.transform.Translate(new Vector2(start_x + i * w, start_y + j * h));
                    map[i, j] = new MapTile(0, newtile);
                }
            }
        checkConnection();
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
    
    private void syncCamera ()
    {
        camera.transform.Translate(player.transform.position - camera.transform.position + new Vector3(0, 0, -5));
    }

    public void loadEncounter()
    {
        string curName = SceneManager.GetActiveScene().name;

        //saves current scene information
        GlobalControl gc = GameObject.Find("GlobalControl").GetComponent<GlobalControl>();
        SceneInfo_dungeon curscene = new SceneInfo_dungeon();
        curscene.pos_x = pos_x;
        curscene.pos_y = pos_y;

        gc.scenes.Push(curscene);
        gc.sceneInformation = null;
        gc.save();

        SceneManager.LoadScene("encounter");
    }
    public void loadInfo_dungeon (SceneInfo_dungeon info)
    {
        pos_x = info.pos_x;
        pos_y = info.pos_y;
    }
}
