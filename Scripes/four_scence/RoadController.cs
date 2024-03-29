﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum RoadType
{
    road,
    trap,
}

public class RoadController : MonoBehaviour
{

    public RoadChildChange[] childrens;
    public void ChangeChildrens()
    {
        if (childrens == null)
        {
            Debug.Log("获取子脚本失败");
            return;
        }
        if (childrens.Length <= 0)
        {
            Debug.Log("脚本数量为0");
            return;
        }
        for (int i = 0; i < childrens.Length; i++)
        {
            childrens[i].nowType = RoadType.road;
            childrens[i].PosChange();
            childrens[i].ChangeRotate();
            childrens[i].isTurn = true;
        }
    }

    /// <summary>
    /// 初始化子物体位置(还原)
    /// </summary>
    public void InitChildrens()
    {
        if(childrens==null)
        {
            print("获取子脚本失败");
            return;
        }
        for(int i=0;i<childrens.Length;i++)
        {
            childrens[i].Init();
            childrens[i].isTurn = false;
        }
    }

    public void ChangeRoadType()
    {
        for(int i=0;i<childrens.Length;i++)
        {
            childrens[i].nowType = RoadType.trap;
        }
    }

}
