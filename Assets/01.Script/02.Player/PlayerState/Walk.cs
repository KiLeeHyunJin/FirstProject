using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[Serializable]
public class Walk : BaseState<PlayerController.State>
{
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
            if (owner.CurrentState == PlayerController.State.Jump)
                isMotion = false;
            else 
                isMotion = true;
            isEnter = true;
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
        if(isMotion)
            ChangeState(PlayerController.State.Walk);
        if(coroutin != null)
            owner.StopCoroutine(coroutin);
        coroutin = owner.StartCoroutine(Move());
        inTime = Time.time;
    }
    public override void CanceledInputAction(InputAction.CallbackContext context) 
    {
        if (coroutin != null)
        {
            outTime = Time.time;
            moveValue = Vector3.zero;
        }
            owner.StopCoroutine(coroutin);
        if (
            owner.CurrentState != PlayerController.State.Run &&
            owner.CurrentState != PlayerController.State.Walk
            )
            return;
        pos.ForceZero(KeyCode.X);
        anim.Play("Idle");
    }
    public override void PerformedInputAction(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (inTime - outTime < 0.2f)
        {
            if(isMotion)
                anim.Play("Run");
            Speed = new Vector2(horizontalRunSpeed, verticalRunSpeed);
        }
        else
        {
            if (isMotion)
            {
                anim.Play("Walk");
                owner.SetState = PlayerController.State.Walk;
            }
            Speed = new Vector2(horizontalWalkSpeed, verticalWalkSpeed);
        }
        moveValue = new Vector2(inputValue.x, inputValue.y).normalized;
    }
    public override void Enter() 
    { 
    
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
                if (moveValue.magnitude != 0)
                {
                    moveVector = new Vector3(
                        moveValue.x * Speed.x,
                        0,
                        moveValue.y * Speed.y);
                    //Debug.Log($"진입 완료 value : {moveVector}");

                    pos.AddForce(moveVector);
                    //rigid.velocity = moveVector;
                    TransformPos.Direction before = pos.direction;

                    if (moveValue.x != 0)
                    {
                        if (moveValue.x < 0)
                            pos.direction = TransformPos.Direction.Left;
                        else
                            pos.direction = TransformPos.Direction.Right;
                    }

                    if (before != pos.direction)
                    {
                        renderer.flipX = !renderer.flipX;
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }


}
