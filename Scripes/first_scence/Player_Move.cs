using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float xSpeed = 0;
    public float zSpeed = 0;
    public float maxSpeed = 5;
    public GameObject foot;
    private void Update()
    {
        if(transform.GetComponent<ReleaseSkill>().pullStar==false)
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x > -9)
            {
                if (xSpeed > -maxSpeed)
                {
                    xSpeed -= Time.deltaTime * 10;
                }
                else if (xSpeed < -maxSpeed)
                {
                    xSpeed = -maxSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.D) && transform.position.x < 9)
            {
                if (xSpeed < maxSpeed)
                {
                    xSpeed += Time.deltaTime * 10;
                }
                else if (xSpeed > maxSpeed)
                {
                    xSpeed = maxSpeed;
                }
            }
            else
                xSpeed = 0;
            if (Input.GetKey(KeyCode.S) && transform.position.y > -9)
            {
                if (zSpeed > -maxSpeed)
                {
                    zSpeed -= Time.deltaTime * 10;
                }
                else if (zSpeed < -maxSpeed)
                {
                    zSpeed = -maxSpeed;
                }
            }
            else if (Input.GetKey(KeyCode.W) && transform.position.x < 9)
            {
                if (zSpeed < maxSpeed)
                {
                    zSpeed += Time.deltaTime * 10;
                }
                else if (zSpeed > maxSpeed)
                {
                    zSpeed = maxSpeed;
                }
            }
            else
                zSpeed = 0;

            transform.Translate(Time.deltaTime * xSpeed, 0, Time.deltaTime * zSpeed);
            foot.transform.Rotate(zSpeed * 2, 0, -xSpeed * 2, Space.World);

        }

    }
}
