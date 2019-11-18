using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelMove : MonoBehaviour
{
    public GameObject deathPanel;
    public Levelstate levelstate = Levelstate.life;

    List<Transform> lineList = new List<Transform>();
    LevelCreate levelCreate;
    private void Start()
    {
        levelCreate = GetComponent<LevelCreate>();
        lineList = GetAllChild(transform);
        CreateLevel();
        
    }
    private void Update()
    {
        if(levelstate==Levelstate.life&&Input.GetKeyDown(KeyCode.Escape))
        {
            levelstate = Levelstate.pause;
            deathPanel.SetActive(true);
            Time.timeScale = 0;
        }

        else if(levelstate==Levelstate.pause&&Input.GetKeyDown(KeyCode.Escape))
        {
            levelstate = Levelstate.life;
            deathPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    List<Transform> GetAllChild(Transform fatherObj)
    {
        List<Transform> sonList = new List<Transform>();
        int number = fatherObj.childCount;
        for(int i=0;i<number;i++)
        {
            sonList.Add(fatherObj.GetChild(i));
        }
        return sonList;
    }
    void CreateLevel()
    {
        Transform last = lineList[lineList.Count - 1];
        List<Transform> sonList = GetAllChild(last);

        Transform enemy = levelCreate.CreateEnemy();
        int index = Random.Range(0, last.childCount);
        enemy.position = last.GetChild(index).position;
        enemy.parent = last.GetChild(index);

        for(int i=0;i<sonList.Count;i++)
        {
            if(i!=index)
            {
                Transform obj = levelCreate.PaneFactory();
                if(obj!=null)
                {
                    obj.position = sonList[i].position;
                    obj.parent = sonList[i];
                }
            }
        }
    }


    public void LevelGetUp()
    {
        Vector3 tempPos = lineList[lineList.Count - 1].position;
        for(int i=lineList.Count-1;i>=0;i--)
        {
            if (i == 0)
                lineList[i].position = tempPos;
            else
                lineList[i].position = lineList[i - 1].position;
        }
    }
    void DestrotStunt()
    {
        Transform[] lineSon = lineList[0].GetComponentsInChildren<Transform>();
        for(int i=0;i<lineSon.Length;i++)
        {
            if(lineSon[i].tag=="Stunt")
            {
                Destroy(lineSon[i].gameObject);
            }
        }
    }
    bool Death()
    {
        Transform[] lineSon = lineList[0].GetComponentsInChildren<Transform>();
        for(int i=0;i<lineSon.Length;i++)
        {
            if(lineSon[i].tag=="Enemy")
            {
                return true;
            }
        }
        return false;
    }
}
