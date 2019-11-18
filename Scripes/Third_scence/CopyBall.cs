using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyBall : MonoBehaviour
{
    public Transform ball;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform tf = collision.transform;
        Transform newBall = Instantiate(ball, tf.position, tf.rotation);
        newBall.parent = tf.parent;
        newBall.GetComponent<Rigidbody2D>().AddForce(transform.right * 0.02f);
        tf.GetComponent<Rigidbody2D>().AddForce(-transform.right * 0.02f);
        Destroy(gameObject);
    }
}
