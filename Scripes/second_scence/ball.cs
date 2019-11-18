using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    //private rigidbody rigid;
    //[serializefield]
    //private transform _rightsuperbounce;
    //[serializefield]
    //private transform _leftsuperbounce;

    //private void awake()
    //{
    //    rigid = getcomponent<rigidbody>();
    //}

    //private void ontriggerenter(collider other)
    //{
    //    if (other.transform.name == "die")
    //    {
    //        startcoroutine(relive());
    //    }
    //    if (other.transform.name == "leftsuperbounce")
    //    {
    //        rigid.addrelativeforce(-_leftsuperbounce.right * 20, forcemode.impulse);
    //    }
    //    if (other.transform.name == "rightsuperbounce")
    //    {

    //    }
    //    if (other.transform.name == "cylinder")
    //    {

    //    }
    //}


    //ienumerator relive()
    //{
    //    yield return new waitforseconds(2);
    //    transform.position = new vector3(-9.3f, 0, 0);
    //}

    //private Rigidbody m_Rigibody;
    //// Use this for initialization
    //void Start()
    //{

    //    m_Rigibody = gameObject.GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    //m_Rigibody.AddForce(Vector3.forward, ForceMode.Force);//世界坐标系向前力
    //    // m_Rigibody.AddRelativeForce(Vector3.forward * 10, ForceMode.Force);//自身坐标系向前力


    //    //给Z键添加一个自身向前的力

    //    if (Input.GetKey(KeyCode.Z))
    //    {
    //        m_Rigibody.AddRelativeForce(Vector3.forward * 100, ForceMode.Acceleration);
    //    }

    //}



}
