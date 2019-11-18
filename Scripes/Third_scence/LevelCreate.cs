using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreate : MonoBehaviour
{
    public Text scoreText;
    public Transform[] Enemys;
    public Transform[] stunts;

    public Transform PaneFactory()
    {
        int chance = Random.Range(0, 4);
        if (chance < 3)
            return null;
        else//产生东西
            return PaneMange();
    }
    Transform PaneMange()
    {
        int chance = Random.Range(0, 3);
        if (chance < 2)
            return CreateEnemy();
        else
            return CreateStunt();

    }
    Transform CreateStunt()
    {
        int index = Random.Range(0, stunts.Length);
        return Instantiate(stunts[index]);
    }
    public Transform CreateEnemy()
    {
        int index = Random.Range(0, Enemys.Length);
        Transform enemy = Instantiate(Enemys[index]);
        enemy.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        enemy.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));

        Transform tf = enemy.GetComponentInChildren<Text>().transform;

        tf.rotation = Quaternion.Euler(0, 0, 0);

        int score = System.Convert.ToInt32(scoreText.text);
        if (score < 100)
            enemy.GetComponentInChildren<Text>().text = Random.Range(1, 10).ToString();
        else
            enemy.GetComponentInChildren<Text>().text = Random.Range(1, score / 10).ToString();
        return enemy;

    }
}
