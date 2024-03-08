using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    [SerializeField] int maxAttack;
    [SerializeField] TransformPos pos;
    [SerializeField] LayerMask layerMask;

    Collider2D[] colliders;

    [SerializeField] Vector2 offset;
    [SerializeField] Vector3 size;

    Vector2 knockbackPower;
    int targetCount;
    float per;
    float damage;
    bool isStart;

    public void Awake()
    {
        if(maxAttack < 1)
            maxAttack = 5;
        colliders = new Collider2D[maxAttack];
        isStart = true;
        offset = Vector3.zero;
        size = Vector3.zero;
    }

    public void SetPosition(Vector2 _position, Vector3 _size)
    {
        offset = _position;
        size = _size * 0.5f;
    }

    public void SetKnockBack(Vector3 power)
    {
        knockbackPower = power; 
    }

    public void SetDamage(float _perDamage, float _damage)
    {
        per = _perDamage;
        damage = _damage;
    }

    public void OnAttackEnable()
    {
        GetCollisionObject();
    }

    private void GetCollisionObject()
    {
        float checkLength = size.x > size.y ? size.x : size.y;

        targetCount = Physics2D.OverlapCircleNonAlloc(
            new Vector2(pos.X + offset.x, pos.Z + pos.Y + offset.y)
            , checkLength, colliders,layerMask);

        Debug.Log($"count : {targetCount}");
        if(targetCount > 0)
            StartCoroutine(AttackCo());
        //else
        //    SetttingReset();
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmoSize = size;
        float checkLength = gizmoSize.x > gizmoSize.y ? gizmoSize.x : gizmoSize.y;
        float temp = isStart ? 1: 0.5f;
        //범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2(pos.X, pos.Z) + new Vector2(offset.x , 0),
            new Vector2(size.x, size.z) * temp * 2);

        //높이
        Gizmos.color = Color.yellow;
        float realYPos =
            (pos.Z  + pos.Y) + (gizmoSize.y * temp * 0.5f);
        Gizmos.DrawWireSphere(new Vector2(pos.X , realYPos) + offset, checkLength * temp);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(pos.X , realYPos) + offset, 
            new Vector2(size.x * temp * 2, size.y * temp));

    }
    IEnumerator AttackCo()
    {
        float value = damage * per;
        Vector2 returnKnockback = knockbackPower;
        Vector3 returnPos = pos.Pose;
        Vector3 returnSize = size;
        Vector2 returnOffset = offset;
        for (int i = 0; i < targetCount; i++)
        {
            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.ISetKnockback(
                    returnKnockback,
                    returnPos,
                    returnSize,
                    returnOffset
                    );
                damagable.IGetDamage(value);
            }
            yield return new WaitForSeconds(0.01f);
        }
        //SetttingReset();
    }

    private void SetttingReset()
    {
        per = 0;
        targetCount = 0;
        damage = 0;
        offset = Vector2.zero;
        size = Vector3.zero;
        knockbackPower = Vector2.zero;
    }


}
