using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour, IDamagable
{
    public TransformPos transformPos;
    [field :SerializeField]public IConnectController controller;
    AttackEffectType attackEffectType;
    float dam;
    float jumpPower;
    void Start()
    {
        controller = transformPos.GetComponent<IConnectController>();
    }

    public void IGetDamage(float damage, AttackEffectType effectType)
    {
        dam = damage;
        attackEffectType = effectType;
    }

    public void ISetKnockback(Vector2 power, Vector3 pos, Vector3 size, Vector2 offset, AttackType attackType, float pushTime = 0)
    {
        if (transformPos.attackCheck.CheckAttackCollision(pos, size, offset))
        {
            if (AttackPowerCheck(attackType) == false)
                return;

            Vector2 force = new Vector2(power.x, power.y * jumpPower);
            transformPos.AddForce(force);
            if (controller != null)
                controller.ISetDamage(dam, attackEffectType);
            transformPos.AddForce(force, pushTime);

            Debug.Log("Ãæµ¹");
        }
    }
    bool AttackPowerCheck(AttackType tagetAttackType)
    {
        jumpPower = 0;
        bool answerd = false;
        switch (controller.IGetStandingType())
        {
            case StandingState.Stand:
                if (tagetAttackType == AttackType.Down)
                    jumpPower = 1;
                answerd = true;
                break;
            case StandingState.Down:
                if (tagetAttackType == AttackType.Down)
                {
                    jumpPower = 0.6f;
                    answerd = true;
                }
                else
                    answerd = false;
                break;
            case StandingState.Fall:
                jumpPower = 0.8f;
                answerd = true;
                break;
            case StandingState.Sit:
                answerd = false;
                break;
        }
        return answerd;
    }

    public bool ICollision(Vector2 size, Vector3 pos, Vector2 offset)
    {
        return transformPos.attackCheck.CheckAttackCollision(pos, size, offset);
    }

    public StandingState IGetStandType()
    {
        return controller.IGetStandingType();
    }
}
