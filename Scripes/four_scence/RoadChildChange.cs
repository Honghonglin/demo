using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RoadChildChange : MonoBehaviour
{
    public Vector3 firstLoadPos;
    public Quaternion firstLocalRotation;
    public bool isTurn;
    public GameObject parentObj;
    public float time = 0.3f;
    public RoadType nowType;
    public GameObject gold;
    Quaternion nowQuat;
    private void Awake()
    {
        time = 0.8f;
        firstLoadPos = transform.localPosition;
        firstLocalRotation = transform.localRotation;
        parentObj = transform.parent.gameObject;
        isTurn = false;
    }



    private void Start()
    {
        gold = Resources.Load("Gold") as GameObject;
        bool isCreat = Random.Range(1, 10) == 3 ? true : false;
        if(isCreat)
        {
            Vector3 tmpPos = transform.position;
            gold = Instantiate(gold, tmpPos += transform.up * 1.2f, Quaternion.identity);
            gold.transform.SetParent(transform);
            nowQuat = gold.transform.rotation;
        }
    }

    private void Update()
    {
        if(time>=0.1f)
        {
            time -= Time.deltaTime * 0.15f;
        }
        if(isTurn&&nowType==RoadType.road)
        {
            transform.RotateAround(parentObj.transform.position, parentObj.transform.forward, 30 * Time.deltaTime);
        }
        else if(isTurn&&nowType==RoadType.trap)
        {
            transform.RotateAround(parentObj.transform.position, parentObj.transform.right, 30 * Time.deltaTime);
        }
        if(gold!=null)
        {
            gold.transform.Rotate(Vector3.up, 70 * Time.deltaTime);
        }
    }


    /// <summary>
    /// 混乱物体坐标
    /// </summary>
    public void PosChange()
    {
        int changeValueUp = 0;
        int changeValueRight = 0;
        while(Mathf.Abs(changeValueUp)<=4.0f)
        {
            changeValueUp = Random.Range(-10, 10);
        }

        while (Mathf.Abs(changeValueRight) <= 4.0f)
        {
            changeValueRight = Random.Range(-10, 10);
        }


        transform.localPosition += transform.up * changeValueUp;
        transform.localPosition += transform.right * changeValueRight;

    }


    /// <summary>
    /// 混乱物体旋转
    /// </summary>
    public void ChangeRotate()
    {
        transform.Rotate(transform.right, Random.Range(0, 180));
        transform.Rotate(transform.forward, Random.Range(0, 180));
        transform.Rotate(transform.up, Random.Range(0, 180));
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        transform.DOLocalMove(firstLoadPos, time);

        Tween t = transform.DOLocalRotateQuaternion(firstLocalRotation, time);//自动播放   

        t.OnComplete(() => gold.transform.rotation = nowQuat);
       
    }




}
