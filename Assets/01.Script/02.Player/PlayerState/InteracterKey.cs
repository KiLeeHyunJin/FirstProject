using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InteracterKey : BaseState<PlayerController.State>
{
    Collider2D[] collider = new Collider2D[1];
    [SerializeField] LayerMask layerMask;
    int count;
    enum Interact{ Attack,Pick };
    Interact type;
    public override void Enter() 
    {
        count = Physics2D.OverlapCircleNonAlloc(pos.Pose, 0.2f, collider, layerMask);
        if (count > 0)
            type = Interact.Pick;
        else
            type = Interact.Attack;
    }

    public override void Transition()
    {
        switch (type)
        {
            case Interact.Attack:
                owner.SetState = PlayerController.State.BasicAtck;
                break;
            case Interact.Pick:
                owner.SetState = PlayerController.State.ItemPick;
                break;
        }
    }
}
