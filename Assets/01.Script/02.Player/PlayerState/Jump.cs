using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Jump : BaseState<PlayerController.State>
{
    [SerializeField] float jumpPower = 5;
    [SerializeField] bool isJump;
    
    protected override void EnterCheck()
    {
        if (
            owner.CurrentState == PlayerController.State.Walk ||
            owner.CurrentState == PlayerController.State.Run ||
            owner.CurrentState == PlayerController.State.Idle ||
            owner.CurrentState == PlayerController.State.Alert ||
            owner.CurrentState == PlayerController.State.Jump
            )
            isEnter = true;
        else
            isEnter = false;
    }
    Coroutine coroutine = null;
    public override void StartedInputAction(InputAction.CallbackContext context) 
    {
        EnterCheck();
        if (isEnter == false)
            return;

        if (pos.yState() != TransformAddForce.YState.None)
            return;

        pos.AddForce(new Vector3(0, jumpPower, 0));
        owner.SetState = PlayerController.State.Jump;
    }
    public override void Enter()
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(JumpCo());
    }
    public override void Exit() 
    { 

    }
    IEnumerator JumpCo()
    {
        anim.Play(AnimIdTable.GetInstance.JumpUpId);
        while (pos.yState() == TransformAddForce.YState.Up)
            yield return new WaitForFixedUpdate();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Up"))//(AnimIdTable.GetInstance.JumpUpId))
            anim.Play(AnimIdTable.GetInstance.JumpDownId);

        while (pos.yState() != TransformAddForce.YState.None)
            yield return new WaitForFixedUpdate();

        owner.SetState = PlayerController.State.Land;
    }

    public override void Transition()
    {
    }
}
