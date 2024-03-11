using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Chase : MonsterState<TauState>
{
    Vector2 optimumRange;
    Transform target;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        target = owner.sensor.target;
        int typeIdx = (int)owner.AtckType;
        optimumRange = owner.GetAtckData(typeIdx).optimumRange;
    }
    public override void Update()
    {
        Vector2 currentPos = new Vector2(pos.X, pos.Z);
        Vector3 targetPos = target.position;

        Vector2 distance = new Vector2(targetPos.x, targetPos.y) - currentPos;
        Vector2 direction =
            new Vector2(
            targetPos.x > currentPos.x ? 1 : -1,
            targetPos.y > currentPos.y ? 1 : -1);
        distance.x *= distance.x > 0 ? 1 : -1;
        distance.y *= distance.y > 0 ? 1 : -1;

        pos.AddForceMove(direction.normalized * owner.moveSpeed);
        owner.FlipCheck(pos.Velocity2D());

        if (distance.x < optimumRange.x)
            direction.x = 0;
        if (distance.y < optimumRange.y)
            direction.y = 0;
        if (direction.x == 0 && direction.y == 0)
            isTransition = true;
    }
   
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    
    public override void Exit()
    {
        base.Exit();
        pos.ForceZero(KeyCode.X);
    }
    public override void Transition()
    {
        if (isTransition)
        {
            switch (owner.AtckType)
            {
                case MonsterController<TauState>.AtckEnum.Atck1:
                    owner.SetState = TauState.Atck1;
                    break;
                case MonsterController<TauState>.AtckEnum.Atck2:
                    owner.SetState = TauState.AtckReady;
                    break;
                case MonsterController<TauState>.AtckEnum.Atck3:
                    owner.SetState = TauState.AtckReady1;
                    break;
            }
        }
    }
}
