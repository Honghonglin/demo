using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{


    public CharacterController characterController;
    public Animator playerAnimator;
    public Vector3 MoveIncrements;
    [SerializeField]
    float transverseSpeed = 5.0f;
    public float moveSpeed = 6.0f;
    public float jumpPower;
    [HideInInspector]
    public GameObject nowRoad;
    bool isTrunleftEnd = true;
    bool isTrunRightEnd = true;
    bool isJumpState;
    RuntimeAnimatorController nowController;
    AnimationClip[] cilps;
    private void Start()
    {
        jumpPower = 3.0f;
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        nowController = playerAnimator.runtimeAnimatorController;/////////
        cilps = nowController.animationClips;
        for(int i=0;i<cilps.Length;i++)
        {
            if(cilps[i].events.Length<=0)//如果这个动画剪辑没有动画事件
            {
                switch(cilps[i].name)//根据剪辑的名字选择出
                {
                    case "JUMP00":
                        AnimationEvent endEvent = new AnimationEvent();
                        endEvent.functionName = "JumpEnd";
                        endEvent.time = cilps[i].length - (20f / 56f) * 1.83f;
                        cilps[i].AddEvent(endEvent);
                        AnimationEvent centerEvent = new AnimationEvent();
                        centerEvent.functionName = "JumpCenter";
                        centerEvent.time = cilps[i].length * 0.3f;
                        cilps[i].AddEvent(centerEvent);
                        break;
                    case "SLIDE00":
                        AnimationEvent slideEvent = new AnimationEvent();
                        slideEvent.functionName = "SlideEnd";
                        slideEvent.time = cilps[i].length - (15f / 42f) * 1.33f;
                        cilps[i].AddEvent(slideEvent);
                        break;
                }
            }
        }
    }


    private void Update()
    {
        moveSpeed += Time.deltaTime * 0.3f;
        float moveDir = Input.GetAxis("Horizontal");
        MoveIncrements = transform.forward * moveSpeed * Time.deltaTime;
        MoveIncrements += transform.right * transverseSpeed * Time.deltaTime * moveDir;
        if(isJumpState)
        {
            MoveIncrements.y += jumpPower * Time.deltaTime;
        }
        else
        {
            MoveIncrements.y += characterController.isGrounded ? 0f : -5.0f * Time.deltaTime * 1f;
        }
        characterController.Move(MoveIncrements);
        playerAnimator.SetFloat("MoveSpeed", characterController.velocity.magnitude);
        if(Input.GetKeyDown(KeyCode.J)&&isTrunleftEnd)//向左转
        {
            isTrunleftEnd = false;
            transform.Rotate(Vector3.up, -90);
            Quaternion tmpQuaternion = transform.rotation;
            transform.Rotate(Vector3.up, 90);
            Tween tween = transform.DORotateQuaternion(tmpQuaternion, 0.3f);
            tween.OnComplete(() => isTrunleftEnd = true);
        }


        if(Input.GetKeyDown(KeyCode.L)&&characterController.isGrounded)
        {
            isTrunRightEnd = false;
            transform.Rotate(Vector3.up, 90);
            Quaternion tmpQuaternion = transform.rotation;
            transform.Rotate(Vector3.up, -90);
            Tween tween = transform.DORotateQuaternion(tmpQuaternion, 0.3f);
            tween.OnComplete(() => isTrunRightEnd = true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            isJumpState = true;
            playerAnimator.SetBool("IsJump", true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerAnimator.SetBool("IsSlide", true);
            
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject!=nowRoad)
        {
            nowRoad = hit.gameObject;
            Destroy(hit.gameObject, 1.0f);
            GameMode.instance.BuildRoad();
            GameMode.instance.CloseRoadAnimator();
        }
    }


    public void JumpEnd()
    {
        playerAnimator.SetBool("IsJump", false);
    }

    public void JumpCenter()
    {
        isJumpState = false;
    }


    public void SliderEnd()
    {
        playerAnimator.SetBool("IsSlide", false);
    }

}
