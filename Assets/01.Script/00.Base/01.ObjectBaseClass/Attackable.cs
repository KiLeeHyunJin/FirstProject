using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour, IDamagable
{
    public TransformPos transformPos;
    [field :SerializeField]public IConnectController controller;
    float dam;
    void Start()
    {
        controller = transformPos.GetComponent<IConnectController>();
    }

    public void IGetDamage(float damage)
    {
        dam = damage;
    }

    public void ISetKnockback(Vector2 power, Vector3 pos, Vector3 size, Vector2 offset)
    {
        if (transformPos.attackCheck.CheckAttackCollision(pos, size, offset))
        {
            transformPos.AddForce(power);
            if (controller != null)
                controller.ISetDamage(dam);
            Debug.Log("�浹");
        }
    }
}
