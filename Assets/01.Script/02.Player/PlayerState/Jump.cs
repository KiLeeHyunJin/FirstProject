using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Jump : BaseState<PlayerController.State>
{
    [SerializeField] float jumpPower = 5;
    [SerializeField] Vector2 moveSpeed;
    
    Coroutine coroutine = null;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.JumpUpId);
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(JumpCo());
    }

    public override void Update()
    {
        Vector2 moveValue = owner.moveValue;
        owner.FlipCheck();
        pos.AddForceMove(moveValue * moveSpeed);
        
    }
    public override void FixedUpdate()
    {
        pos.JumpingFreezePosition();
    }
    public override void Exit() 
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }
    IEnumerator JumpCo()
    {
        yield return new WaitForFixedUpdate();

        anim.Play(AnimIdTable.GetInstance.JumpUpId);
        pos.AddForce(new Vector3(0, jumpPower, 0));

        while (pos.yState() == TransformAddForce.YState.Up)
            yield return new WaitForFixedUpdate();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Up"))//(AnimIdTable.GetInstance.JumpUpId))
            anim.Play(AnimIdTable.GetInstance.JumpDownId);

        while (pos.yState() != TransformAddForce.YState.None)
            yield return new WaitForFixedUpdate();

        isTransition = true;    
    }

    public override void Transition()
    {
        if(isTransition)
            owner.SetState = PlayerController.State.Land;
    }
}
