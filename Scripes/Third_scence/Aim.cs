using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject ball;
    Rigidbody2D[] allBall;
    LineRenderer aimLine;
    public Transform CriticalPoinLeft;
    public Transform CriticalPointRight;
    public float shootingSpeed = 3.5f;
    public GameObject levelPanel;
    bool levelStop;
    private void Start()
    {
        Time.timeScale = 1;
        allBall = ball.GetComponentsInChildren<Rigidbody2D>();
        aimLine = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if(levelPanel.GetComponent<LevelMove>().levelstate==Levelstate.life)
        {
            
        }
    }
    public bool Homing()
    {
        for(int i=0;i<allBall.Length;i++)
        {
            if(allBall[i].GetComponent<BallMove>().ballstate!=Ballstate.Ready)
            {
                return false;
            }
        }
        return true;
    }
    void AimLaunch()
    {
        if(Input.GetMouseButtonDown(0))
        {
            aimLine.SetPosition(0, transform.position);
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v = DirectionRestriction(v, CriticalPoinLeft, CriticalPointRight);
            aimLine.SetPosition(1, new Vector2(v.x, v.y));
        }
        if(Input.GetMouseButtonUp(0))
        {
            StartCoroutine(LineLaunch(transform.position));
            aimLine.SetPosition(1, transform.position);
            levelStop = !levelStop;
        }
    }
    IEnumerator LineLaunch(Vector3 muzzlePos)
    {
        Vector3 pos1 = aimLine.GetPosition(1);
        Vector3 directionAttack = (pos1 - muzzlePos).normalized;
        for(int i=0;i<allBall.Length;i++)
        {
            allBall[i].GetComponent<BallMove>().ballstate = Ballstate.Battle;
            allBall[i].AddForce(directionAttack * shootingSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
    }
    Vector3 DirectionRestriction(Vector3 v,Transform left,Transform right)
    {
        if(v.x<left.position.x)
        {
            v.x = left.position.x;
        }
        if(v.x>right.position.x)
        {
            v.x = right.position.x;
        }
        if(v.y>left.position.y)
        {
            v.y = left.position.y;
        }
        return v;
    }
}
