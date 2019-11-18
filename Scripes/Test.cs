using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tool;
using System.Text;
public class Test : MonoBehaviour
{
    public Map map;
    public BFS bfs;
    [NonSerialized]
    public int B = 4;
    
    private void Start()
    {
        map = new Map(Application.dataPath + "//" + "map.txt", 10, 10);
        int[,] temp = map.GetMap();
        for (int i = 0; i < map.Hight; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                Debug.Log(temp[i, j]);
            }
        }
        bfs = new BFS(map, new Pos(4, 0), new Pos(7, 9));
        bfs.Make_bfs_Map();
        bfs.ShowPath();
        foreach(var i in bfs._bfsPath)
        {
            Debug.Log("路径坐标(x,y)为(" + i.x +"," + i.y + ")");
        }



    }




}
