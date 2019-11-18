using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ti : MonoBehaviour
{
    public GameObject body;
    public GameObject hammer;

    Rigidbody body_rig;
    Rigidbody hammer_rig;
    Transform hammer_anchor;

    void Start()
    {
        body_rig = body.GetComponent<Rigidbody>();
        hammer_rig = hammer.GetComponent<Rigidbody>();
        hammer_anchor = hammer.transform.GetChild(2);
    }
    //物理相关的操作一般最好放在FixedUpdate里进行,与系统的物理计算保持同步
    private void FixedUpdate()
    {
        HammerControl();
    }
    void HammerControl()
    {
        //获取鼠标在屏幕上的坐标并转换为世界坐标
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //通过修改锤子身上刚体的速度让锤子移动,向量的起点用锤头的坐标
        //注意这里要让锤头的锚点与锤子本身的坐标的Z值相等,为了让旋转轴与世界坐标的Z轴平行,同理鼠标坐标的Z值也直接使用锤子的坐标的Z值
        hammer_rig.velocity = (new Vector3(MousePosition.x, MousePosition.y, hammer.transform.position.z) - hammer_anchor.position) * 10;

        //获取身体到鼠标的方向
        Vector3 direction = (new Vector3(MousePosition.x, MousePosition.y, hammer_anchor.position.z) - new Vector3(body.transform.position.x, body.transform.position.y, hammer_anchor.position.z)).normalized;

        //让锤子沿着锤头锚点转向身体的方向
        hammer.transform.RotateAround(hammer_anchor.position, Vector3.Cross(hammer_anchor.up, direction), Vector3.Angle(hammer_anchor.up, direction));
    }
}
