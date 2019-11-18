using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : MonoBehaviour
{
    public GameObject doubleHands;
    public float pullSpeed = 0;
    public float maxPull = 20;
    private Vector3 handPos;
    public bool pullStar = false;
    bool holdGO = false;
    private RaycastHit hit;
    public LayerMask mask;
    private Vector3 hitPos;
    private void HandTrail(float trailTime)
    {
        Transform[] transforms = doubleHands.GetComponentsInChildren<Transform>();
        for(var i=1;i<transforms.Length;i++)
        {
            if(transforms[i].GetComponent<TrailRenderer>()==null)
            {

                transforms[i].GetComponent<TrailRenderer>().time = trailTime;


            }
        }
    }
    /// <summary>
    /// 释放技能
    /// </summary>
    void Release()
    {
        HandTrail(1);
        handPos = doubleHands.transform.position;
        pullSpeed = 50;
        pullStar = true;
        if(Physics.Raycast(handPos,doubleHands.transform.forward,out hit,maxPull,mask))
        {
            hitPos = hit.point;
        }
    }

    void KeepPos(Transform Cube)
    {
        if(Cube!=null&&Cube.parent!=null)
        {
            Cube.position = doubleHands.transform.position;
            Cube.rotation = doubleHands.transform.rotation;
        }
    }

    void  Pull()
    {
        if(pullStar==true)
        {
            doubleHands.transform.Translate(0,0,Time.deltaTime * pullSpeed);
            if(hit.transform==null&&(doubleHands.transform.position-handPos).magnitude>maxPull)
            {
                HandTrail(0);
                pullSpeed = -50;
            }
            else if(hit.transform!=null&&(doubleHands.transform.position-hitPos).magnitude<0.5f)
            {
                hit.transform.parent = doubleHands.transform;
                holdGO = true;
                HandTrail(0);
                pullSpeed = -50;
            }
            if(pullSpeed<0&&(doubleHands.transform.position-handPos).magnitude<0.5)
            {
                pullSpeed  =0;
                doubleHands.transform.position = handPos;
                pullStar = false;
            }
        }
    }

    private void Update()
    {
        if(pullStar==false&&holdGO==false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Release();
            }
        }
        else if(holdGO==true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                hit.transform.SetParent(null);
                holdGO = false;
            }
        }
        Pull();
        
    }
}
