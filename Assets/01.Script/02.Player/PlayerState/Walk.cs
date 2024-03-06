using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[Serializable]
public class Walk : BaseState<PlayerController.State>
{
    [Header("���� ������ �ӵ�")]
    [SerializeField] float verticalWalkSpeed;
    [SerializeField] float verticalRunSpeed;

    [Header("���� ������ �ӵ�")]
    [SerializeField] float horizontalWalkSpeed;
    [SerializeField] float horizontalRunSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float horizontalSpeed;
    Vector2 moveValue;
    float outTime;
    float inTime;
    public Walk(Animator _anim, TransformPos _transform, AttackController _atckCon, SpriteRenderer _renderer) 
        : base(_anim, _transform, _atckCon, _renderer)
    {
    }

    public override void StartedInputAction(InputAction.CallbackContext context) 
    {
        //Vector2 moveValue = context.ReadValue<Vector2>();

        // ���⿡�� �ʿ��� ������ ����
        Debug.Log("�ʱ� ����");
        inTime = Time.time;

    }
    public override void CanceledInputAction(InputAction.CallbackContext context) 
    {
        outTime = Time.time;
        moveValue = Vector3.zero;
        pos.ForceZero(KeyCode.X);
        anim.Play("Idle");
    }
    public override void PerformedInputAction(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();

        //Debug.Log(inputValue);
        if (inTime - outTime < 0.2f)
        {
            anim.Play("Run");
            horizontalSpeed = horizontalRunSpeed;
            verticalSpeed = verticalRunSpeed;
        }
        else
        {
            anim.Play("Walk");
            horizontalSpeed = horizontalWalkSpeed;
            verticalSpeed = verticalWalkSpeed;
        }

        moveValue = new Vector2(inputValue.x, inputValue.y).normalized;
        //Debug.Log(moveValue.sqrMagnitude);

        //if (moveValue.sqrMagnitude == 0)
        //{
        //    Debug.Log("Zero");

        //}
    }
    public override void Enter() 
    { 
    
    }
    public override void Exit() 
    {

    }
    public override void Update() 
    {
        if (moveValue.magnitude != 0)
        {
            Vector3 moveVector = new Vector3( moveValue.x * horizontalSpeed, 0, moveValue.y * verticalSpeed);
            //Debug.Log($"���� �Ϸ� value : {moveVector}");

            pos.AddForce(moveVector);
            //rigid.velocity = moveVector;
            TransformPos.Direction before = pos.direction;

            if (moveValue.x != 0)
            {
                if (moveValue.x < 0)
                    pos.direction = TransformPos.Direction.Left;
                else if (moveValue.x > 0)
                    pos.direction = TransformPos.Direction.Right;
            }

            if (before != pos.direction)
            {
                renderer.flipX = !renderer.flipX;
            }
        }
    }

}
