using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public GameObject[] point;
    public GameObject panel1, panel2;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        
    }


    bool AllStar(GameObject[] point)
    {
        for(int i=0;i<point.Length;i++)
        {
            if(!point[i].GetComponent<StreetLamp>().lightStar)
            {
                return false;
            }
        }
        return true;
    }


    public void Continue()
    {
        panel2.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene("FootBallRobot");
    }
    public void SignOut()
    {
        Application.Quit();//退出程序
    }
}
