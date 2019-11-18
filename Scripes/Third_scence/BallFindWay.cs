using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFindWay : MonoBehaviour
{
    public Transform muzzle;
    public float boreSpeed = 0.2f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D r2d = collision.GetComponent<Rigidbody2D>();
        switch(name)
        {
            case "LeftDown":
                r2d.AddForce(-transform.right * 0.002f);
                break;
            case "RightDown":
                r2d.AddForce(transform.right * 0.003f);
                break;
            case "Left":
            case "Right":
                r2d.AddForce(transform.up * 0.002f);
                break;
            case "Up":
                StartCoroutine(MoveToMuzzle(collision.transform, muzzle));
               
                collision.GetComponent<CircleCollider2D>().isTrigger = true;
                break;

        }
    }
    public IEnumerator MoveToMuzzle(Transform ball,Transform muzzle)
    {
        ball.GetComponent<BallMove>().ballstate = Ballstate.Bore;
        while(ball.GetComponent<BallMove>().ballstate==Ballstate.Bore)
        {
            ball.position = Vector3.MoveTowards(ball.position, muzzle.position, boreSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
            if((ball.position-muzzle.position).sqrMagnitude<=0.001f)
            {
                ball.GetComponent<BallMove>().ballstate = Ballstate.Ready;
                ball.position = muzzle.position;
            }
        }
    }
}
