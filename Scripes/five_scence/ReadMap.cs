using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
namespace Tool
{

    public class Pos
    {
        public int x = 0;
        public int y = 0;
        public Pos()
        {

        }
        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Pos(Pos pos)
        {
            x = pos.x;
            y = pos.y;

        }
         
        public static bool operator ==(Pos pos1,Pos pos2)
        {
            if(pos1.x==pos2.x&&pos1.y==pos2.y)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Pos pos1,Pos pos2)
        {
            if(pos1.x!=pos2.x||pos1.y!=pos2.y)
            {
                return true;
            }
            return false;
        }


    }
    [Serializable]
    public class Map
    {
        public string Path;//地图的路径
        public int Width;//长度最好比TXT中的大
        public int Hight;//高度也是
        private int[,] map;

        public Map(string Path, int Width, int Hight)
        {
            this.Path = Path;
            this.Width = Width;
            this.Hight = Hight;
            map =ReadMapFile();
        }

        /// <summary>
        /// 得到迷宫的图
        /// </summary>
        /// 有数则有障碍物
        public int[,] ReadMapFile()
        {
            int[,] Map = new int[Hight, Width];
            FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = "";
            int y = 0;
            read.ReadLine();//跳过第一行
            strReadline = read.ReadLine();

            while (strReadline != null && y < Hight)
            {
                for (int x = 0; x < Width && x < strReadline.Length; x++)
                {
                    int t;
                    t = System.Convert.ToInt32(strReadline[x]);
                    //空格的情况
                    if (t == 32)
                    {
                        t = 0;
                    }
                    //用*标志这个地图的开始和结束
                    else if (t == 42)
                    { }
                    else
                        Map[y, x] = t - 48;
                    //否则就是这个数字
                }
                y++;
                strReadline = read.ReadLine();
            }
            read.Dispose();//文件流释放
            fs.Close();
            return Map;
        }     
        

        public int[,] GetMap()
        {
            return map;
        }
    }

    [Serializable]
    public class BFS
    {
        private int cur_Pace;
        private int[ ,] map;
        private  int[,] bfs_Map;
        private Pos startPos;//x代表第几列，y代表第几行
        private Pos endPos;
        private Queue<Pos> posQueue = new Queue<Pos>();
        private List<List<Pos> > gameObjectsPOS_str=new List<List<Pos> >();//存各个步数的格子位置(包括起点和终点)
        private List<Pos> bfsPath = new List<Pos>();//存bfs找到的那条路径（不包括终点和起点）(倒序的）
        public BFS(Map map, Pos startPos, Pos endPos)
        {
            bfs_Map = new int[map.Hight, map.Width];
            this.startPos = startPos;
            this.endPos = endPos;
            this.map = map.GetMap();
        }
        public List<Pos> _bfsPath
        {
            get
            {
                return bfsPath;
            }
        }
        public List<List<Pos>> _gameObjectsPOS_str
        {
            get
            {
                return gameObjectsPOS_str;
            }
        }
        private delegate bool Func(Pos cur, int ox, int oy);
        /// <summary>
        /// 创建BFS地图
        /// </summary>
        public void Make_bfs_Map()
        {
            for(int i=0;i<bfs_Map.GetLength(0);i++)
            {
                for(int j=0;j<bfs_Map.GetLength(1);j++)
                {
                    bfs_Map[i,j] = short.MaxValue;
                }
            }
            bfs_Map[startPos.y, startPos.x] = 0;
            cur_Pace = -1;
            posQueue.Enqueue(startPos);
            Func func = (Pos cur, int ox, int oy) =>
              {
                  if(cur.x+ox==endPos.x&&cur.y+oy==endPos.y)
                  {
                      bfs_Map[cur.y + oy, cur.x + ox] = bfs_Map[cur.y, cur.x] + 1;
                      Debug.Log("寻路完成");
                      return true;
                  }

                  if(map[cur.y+oy,cur.x+ox]==0)
                  {
                     if(bfs_Map[cur.y+oy,cur.x+ox]>bfs_Map[cur.y,cur.x]+1)//只有当格上的点比当前的点大是时候才可以，否则不可以
                      {
                          bfs_Map[cur.y + oy, cur.x + ox] = bfs_Map[cur.y, cur.x] + 1;
                          posQueue.Enqueue(new Pos(cur.x + ox,cur.y + oy ));
                      }
                  }
                  return false;

              };
            Debug.Log("here");
            List<Pos> gameObjects_Pos = new List<Pos>();
            gameObjects_Pos.Add(startPos);
            int debug = 1;//调试数
            while (posQueue.Count > 0)
            {

                Pos cur = posQueue.Dequeue();
                //上
                if (cur.y > 0)
                {
                    if (func(cur, 0, -1)) { break; }
                }
                //下
                if (cur.y < map.GetLength(0) - 1)
                {
                    if (func(cur, 0, 1)) { break; }
                }
                //左
                if (cur.x > 0)
                {
                    if (func(cur, -1, 0)) { break; }
                }
                //右
                if (cur.x < map.GetLength(1) - 1)
                {
                    if (func(cur, 1, 0)) { break; }
                }
                if(bfs_Map[cur.y,cur.x]>cur_Pace)
                {
                    cur_Pace = bfs_Map[cur.y, cur.x];

                    //实例化物体(这只能放数据，没有继承monobehavior）
                    gameObjects_Pos.Add(cur);
                
                }
                else
                {
                    gameObjects_Pos.Add(cur);
                }
                Debug.Log("here");
                //分段,当下一个对应格数的步数不一样时，停顿0.5f以便观察
                Pos temp = posQueue.Peek();
                Debug.Log("here");
                if (bfs_Map[cur.y,cur.x]!=bfs_Map[temp.y,temp.x])
                {
                    gameObjectsPOS_str.Add(gameObjects_Pos);
                    
                    //调试代码
                    foreach(var i in gameObjects_Pos)
                    {
                        Debug.Log("第"+debug+ "个物体的横纵坐标"+i.x+","+i.y);
                    }
                    //结束调试
                    gameObjects_Pos = new List<Pos>();
                    debug++;
                }
                

            }
            
        }
        

        /// <summary>
        /// 生成一条终点和起点的路径
        /// </summary>
       public void ShowPath()
        {
            Pos p = endPos;
            while(true)
            {
                int cur_step = bfs_Map[p.y, p.x];
                if (cur_step==0)
                {
                    break;     
                }
                if(p.y>0&&bfs_Map[p.y-1,p.x]==cur_step-1)
                {
                    p.y -= 1;
                }
                else if(p.y<bfs_Map.GetLength(0)-1 && bfs_Map[p.y+1,p.x]==cur_step-1)
                {
                    p.y += 1;
                }
                else if (p.x >0 && bfs_Map[p.y, p.x-1] == cur_step - 1)
                {
                    p.x -= 1;
                }
                else if (p.x < bfs_Map.GetLength(1) - 1 && bfs_Map[p.y, p.x+1] == cur_step - 1)
                {
                    p.x += 1;
                }
                if(p!=startPos)
                {
                    bfsPath.Add(new Pos(p.x,p.y));
                }
            }
        }
    }

}
    