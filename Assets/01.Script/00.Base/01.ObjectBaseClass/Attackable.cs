using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour, IDamagable
{
    public TransformPos transformPos;
    public PlayerController controller;
    public void IGetDamage(float damage)
    {

    }

    public void ISetKnockback(Vector2 power, Vector3 pos, Vector3 size, Vector2 offset)
    {
        if (transformPos.attackCheck.CheckAttackCollision(pos, size, offset))
        {
            transformPos.AddForce(power);
            if (controller != null)
                controller.CallDown();
        }
    }
}
