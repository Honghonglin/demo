using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//需要防卡住
public class BallMove : MonoBehaviour
{
    float timer;
    public Ballstate ballstate = Ballstate.Ready;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(ballstate==Ballstate.Battle)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            if(collision.gameObject.tag=="Enemy")
            {
                Text enemyNumber = collision.transform.GetChild(0).GetComponent<Text>();
                Text Score = GameObject.Find("ScoreText").GetComponent<Text>();

                if(tag== "BigBall")
                {
                    enemyNumber.text = ((System.Convert.ToInt32(enemyNumber.text)) - 2).ToString();
                    Score.text = (System.Convert.ToInt32(Score.text) + 2).ToString();
                }
                else
                {
                    enemyNumber.text = ((System.Convert.ToInt32(enemyNumber.text)) - 1).ToString();
                    Score.text = ((System.Convert.ToInt32(Score.text)) + 1).ToString();
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            timer += Time.deltaTime;
            if(timer>1)
            {
                switch(Random.Range(0,4))
                {
                    case 0:
                        GetComponent<Rigidbody2D>().AddForce(transform.up * 0.01f);
                        break;
                    case 1:
                        GetComponent<Rigidbody2D>().AddForce(-transform.up * 0.01f);
                        break;
                    case 2:
                        GetComponent<Rigidbody2D>().AddForce(transform.right * 0.01f);
                        break;
                    case 3:
                        GetComponent<Rigidbody2D>().AddForce(-transform.up * 0.01f);
                        break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        timer = 0;
    }

    private void Update()
    {
        transform.Rotate(0, 0, 0.0001f);//物体处于非完全静止状态，持续碰撞才会生效
        switch(ballstate)
        {
            case Ballstate.Bore:
                GetComponent<Rigidbody2D>().gravityScale = 0;
                break;
            case Ballstate.Ready:
                GetComponent<CircleCollider2D>().isTrigger = false;
                GetComponent<Rigidbody2D>().Sleep();
                break;
        }
    }
}
