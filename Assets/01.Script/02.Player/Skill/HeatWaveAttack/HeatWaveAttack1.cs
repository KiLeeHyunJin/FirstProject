using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HeatWaveAttack1 : SkillState
{
    protected Collider2D[] targets = new Collider2D[15];
    float checkLength;
    protected override void Attack(float normalTime)
    {
    }

    protected override void EnterAction()
    {
        checkLength = attackData.attackSize.size.x > attackData.attackSize.size.y ? attackData.attackSize.size.x : attackData.attackSize.size.x;
    }

    protected override void ExitAction()
    {
        int layer = 1 << 6;
        Vector3 returnPos = pos.Pose;
        Vector3 returnSize = attackData.attackSize.size * 0.5f;
        Vector2 returnOffset = attackData.attackSize.offset;
        returnOffset.x *= direction;
        int targetCount = Physics2D.OverlapCircleNonAlloc(
          new Vector2(pos.X , pos.Z + pos.Y ) + returnOffset
          , checkLength, targets, layer);
        for (int i = 0; i < targetCount; i++)
        {
            IDamagable damagable = targets[i].GetComponent<IDamagable>();
            if (damagable != null)
            {
                Vector3 returnKnockback = new Vector3(Random.Range(-3, 3), 2, Random.Range(-2, 2));

                //if (AttackPossibleCheck(damagable.IGetStandType()) == false)
                //    continue;

                damagable.IGetDamage(
                    0,
                    AttackEffectType.Down);
                damagable.ISetKnockback(
                    returnKnockback,
                    returnPos,
                    returnSize,
                    returnOffset,
                    attackData.attackCounts[0].stunTime,
                    0
                    );
            }
        }
        skillController.Out();
        owner.activeType = ActiveType.Normal;
    }

    protected override PlayerState NextAnim()
    {
        
        return PlayerState.Idle;
    }

}
