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
        anim.Play("Jump_Up");
        while (pos.Velocity().y > 0f)
            yield return new WaitForFixedUpdate();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Up"))
            anim.Play("Jump_Down");

        while (pos.Y > 0)
            yield return new WaitForFixedUpdate();

        owner.SetState = PlayerController.State.Land;
    }
}
