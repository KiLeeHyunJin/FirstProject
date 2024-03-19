using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Chase : MonsterState<GoblinState>
{
    Vector2 optimumRange;
    Transform target;
    bool isTransition;
    bool isNotFound;
    public override void Enter()
    {
        isTransition = false;
        isNotFound = false;
        target = owner.sensor.target;
        optimumRange = owner.GetAtckData(0).optimumRange;
    }
    public override void Update()
    {
        Vector2 currentPos = new Vector2(pos.X, pos.Z);
        if (target == null)
        {
            isNotFound = true;
            return;
        }
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
        base.FixedUpdate();
        pos.Synchro();
        owner.FlipCheck(pos.Velocity2D());
    }
    public override void Exit()
    {
        base.Exit();
        pos.ForceZero(KeyCode.X);
    }
    public override void Transition()
    {
        if (isTransition)
            owner.SetState = GoblinState.Atck;
        else if(isNotFound)
            owner.SetState = GoblinState.Idle;
    }
}
