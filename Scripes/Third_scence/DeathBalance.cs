using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DeathBalance : MonoBehaviour
{
    public Text score;
    public Text bureauScore;
    public Text topScore;
    private void OnEnable()
    {
        bureauScore.text = score.text;
        if (PlayerPrefs.HasKey("分数"))
            topScore.text = PlayerPrefs.GetString("分数");
        if(Convert.ToInt32(bureauScore.text)>Convert.ToInt32(topScore.text))
        {
            topScore.text = bureauScore.text;
            PlayerPrefs.SetString("分数", bureauScore.text);
        }
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EladticBall");
    }
    public void QuiGame()
    {
        Application.Quit();
    }
}
