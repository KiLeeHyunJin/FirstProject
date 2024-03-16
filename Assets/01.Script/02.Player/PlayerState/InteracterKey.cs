using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InteracterKey : PlayerBaseState<PlayerState>
{
    Collider2D[] collider = new Collider2D[1];
    [SerializeField] LayerMask layerMask;
    int count;
    enum Interact{ Attack,Pick };
    Interact type;
    bool isTransition;
    public override void Enter() 
    {
        isTransition = false;
        count = Physics2D.OverlapCircleNonAlloc(new Vector2(pos.X,pos.Z), 0.2f, collider, layerMask);
        if (count > 0)
        {
            isTransition = true;
            type = Interact.Pick;

        }
        else
        {
            isTransition = true;
            type = Interact.Attack;
        }
    }

    public override void Transition()
    {
        if (isTransition == false)
            return;
        switch (type)
        {
            case Interact.Attack:
                owner.SetState = PlayerState.BasicAtck;
                break;
            case Interact.Pick:
                owner.SetState = PlayerState.ItemPick;
                break;
        }
    }
    public override void Exit()
    {
        base.Exit();
        isTransition = false;
    }
}
