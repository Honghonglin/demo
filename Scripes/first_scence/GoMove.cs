using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoMove : MonoBehaviour
{
    public Transform[] target;
    public float speed = 0;
    private void Start()
    {
        transform.position = new Vector3(Random.Range(-9, 9), 0.75f, Random.Range(9, 9));
        StartCoroutine(MoveToPath());

    }
    /// <summary>
    /// 循环寻路
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToPath()
    {
       while(true)
        {
            for(int i=0;i<target.Length; i++)
            {
                yield return StartCoroutine(MoveToTarget(target[i].position));
            }
        }
    }
    ///寻路方法
    IEnumerator MoveToTarget(Vector3 target)
    {
        while ((transform.position - target).magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
