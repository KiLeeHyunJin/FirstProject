using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[Serializable]
public class Walk : BaseState<PlayerController.State>
{
    enum MotionType
    { run, walk}
    [Header("수직 움직임 속도")]
    [SerializeField] float verticalWalkSpeed;
    [SerializeField] float verticalRunSpeed;

    [Header("수평 움직임 속도")]
    [SerializeField] float horizontalWalkSpeed;
    [SerializeField] float horizontalRunSpeed;
    [SerializeField] Vector2 Speed;
    Vector2 moveValue;
    [SerializeField] Vector3 moveVector;
    float outTime;
    float inTime;
    [SerializeField]bool isMotion;
    MotionType motion;
    protected override void EnterCheck()
    {
        if (
            owner.CurrentState == PlayerController.State.Walk ||
            owner.CurrentState == PlayerController.State.Run ||
            owner.CurrentState == PlayerController.State.Idle ||
            owner.CurrentState == PlayerController.State.Alert ||
            owner.CurrentState == PlayerController.State.Jump
            )
        {
            isEnter = true;
            if (owner.CurrentState == PlayerController.State.Jump)
                isMotion = false;
            else 
                if(pos.yState() == TransformAddForce.YState.None)
                    isMotion = true;
            return;
        }
        isEnter = false;
        return;
    }

    public override void StartedInputAction(InputAction.CallbackContext context) 
    {
        EnterCheck();
        if (isEnter == false)
            return;
        owner.SetState = PlayerController.State.Walk;
        inTime = Time.time;
    }
    public override void CanceledInputAction(InputAction.CallbackContext context) 
    {
        if (coroutin != null)
        {
            outTime = Time.time;
            owner.StopCoroutine(coroutin);
            moveValue = Vector3.zero;
        }

        if (
            owner.CurrentState != PlayerController.State.Run &&
            owner.CurrentState != PlayerController.State.Walk
            )
            return;
        //Debug.Log("제로 호출");
        pos.ForceZero(KeyCode.X);
        owner.SetState = PlayerController.State.Idle;
    }
    public override void PerformedInputAction(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
       
        moveValue = new Vector2(inputValue.x, inputValue.y).normalized;
    }

    public override void Enter() 
    {
        EnterCheck();
        if (isEnter == false)
            return;
        if (coroutin != null)
            owner.StopCoroutine(coroutin);
        coroutin = owner.StartCoroutine(Move());
        if (outTime -inTime < 0.2f)
        {
            motion = MotionType.run;
            Speed = new Vector2(horizontalRunSpeed, verticalRunSpeed);
        }
        else
        {
            motion = MotionType.walk;
            Speed = new Vector2(horizontalWalkSpeed, verticalWalkSpeed);
        }
    }
    void Render()
    {
        if (isMotion == false)
            return;
        int id = motion == MotionType.run? AnimIdTable.GetInstance.RunId : AnimIdTable.GetInstance.WalkId;
        anim.Play(id);
    }

    public override void Exit() 
    {

    }

    Coroutine coroutin = null;
    IEnumerator Move()
    {
        while(true)
        {
            EnterCheck();
            if (isEnter)
            {
                Render();
                Debug.Log("이동 확인 중");
                if (moveValue.x != 0 || moveValue.y != 0)
                {
                    moveVector = new Vector2(
                        moveValue.x * Speed.x,
                        moveValue.y * Speed.y);

                    pos.AddForceMove(moveVector);

                    if (moveValue.x != 0)
                    {
                        TransformPos.Direction before = pos.direction;

                        if (moveValue.x < 0)
                            pos.direction = TransformPos.Direction.Left;
                        else
                            pos.direction = TransformPos.Direction.Right;

                        if (before != pos.direction)
                            renderer.flipX = !renderer.flipX;
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

    public override void Transition()
    {
    }
}
