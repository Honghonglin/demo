using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Move : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public LayerMask layerMask;
    public GameObject body;
    void Update()
    {
        if (transform.GetComponent<ReleaseSkill>().pullStar == false)
        {
            body.transform.LookAt(InputMove());
        }
    }
    /// <summary>
    /// 鼠标在世界的移动
    /// </summary>
    /// <returns></returns>
    private Vector3 InputMove()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            return new Vector3(hit.point.x, hit.point.y + body.transform.position.y, hit.point.z);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
}
