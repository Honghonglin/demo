using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

class RobotController:MonoBehaviour
{
    Dictionary<GameObject, int> boxes = new Dictionary<GameObject, int>();
    int id_counter = 1;

    // 保存货物到boxes容器中，会给货物分配ID
    void SaveNewBox(GameObject box)
    {
        if (box.transform.position.y > 0.251f)
        {
            return;
        }
        if (boxes.ContainsKey(box))
        {
            return;
        }
        boxes[box] = id_counter;
        id_counter++;
    }

    void Update()
    {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in all)
        {
            // 保存货物到boxes中，这里会给货物分配ID
            SaveNewBox(box);
        }
    }
}