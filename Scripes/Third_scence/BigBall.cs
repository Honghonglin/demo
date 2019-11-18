using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="BigBall")
        {
            collision.transform.localScale *= 1.2f;
            collision.gameObject.tag = "BigBall";
        }
        Destroy(gameObject);
    }
}
