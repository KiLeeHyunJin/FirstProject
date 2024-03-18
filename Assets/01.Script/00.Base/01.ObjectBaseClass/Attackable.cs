using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour, IDamagable
{
    public TransformPos transformPos;
    new SpriteRenderer renderer;
    [field :SerializeField]public IConnectController controller;
    AttackEffectType attackEffectType;
    int dam;
    float jumpPower;
    bool isPossible;
    bool isColl;
    void Start()
    {
        controller = transformPos.GetComponent<IConnectController>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public bool IGetDamage(int damage, AttackEffectType effectType)
    {
        dam = damage;
        attackEffectType = effectType;
        return isPossible = AttackPowerCheck(attackEffectType);
    }
    public bool ICollision(Vector3 size, Vector3 pos, Vector2 offset)
    {
        return isColl = transformPos.attackCheck.CheckAttackCollision(pos, size, offset);
    }

    public void ISetKnockback(Vector3 power, float stunTime,float pushTime = 0)
    {
        if (isColl)
        {
            if (isPossible == false)
                return;

            Vector3 force = new Vector3(power.x, power.y * jumpPower, power.z);
            transformPos.AddForce(force);
            if (controller != null)
                controller.ISetDamage(dam, attackEffectType,stunTime);
            transformPos.AddForce(force, pushTime);

            Debug.Log("Ãæµ¹");
        }
    }
    bool AttackPowerCheck(AttackEffectType tagetAttackType)
    {
        jumpPower = 0;
        bool answerd = false;
        switch (controller.IGetStandingType())
        {
            case StandingState.Stand:
                if (tagetAttackType == AttackEffectType.Down)
                    jumpPower = 1;
                answerd = true;
                break;
            case StandingState.Down:
                if (tagetAttackType == AttackEffectType.Down)
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
    public Vector3 IGetPos()
    {
        return transformPos.Pose;
    }
    public Vector2 IGetOffset()
    {
        return transformPos.Offset;
    }


    public StandingState IGetStandType()
    {
        return controller.IGetStandingType();
    }

    public int IGetRenderLayerNum() =>  renderer.sortingOrder;

}
