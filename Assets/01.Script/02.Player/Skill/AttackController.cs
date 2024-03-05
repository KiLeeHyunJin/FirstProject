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

    int count;
    float damage;

    public void Start()
    {
        colliders = new Collider2D[maxAttack];
    }
    public void SetPosition(Vector2 _position, Vector3 _size)
    {
        offset = _position;
        size = _size * 0.5f;
        if (pos.direction == TransformPos.Direction.Left)
            offset.x *= -1;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    public void OnAttackEnable()
    {
        GetCollisionObject();
    }

    private void GetCollisionObject()
    {
        count = Physics2D.OverlapCircleNonAlloc(
            new Vector2(pos.X + offset.x, pos.Y + offset.y)
            , size.x, colliders, layerMask);

        if(count > 0)
            StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.07f);
            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            if(damagable != null)
            {
                damagable.ISetKnockback(
                    knockbackPower,
                    pos.Pose,
                    size,
                    offset
                    );
                damagable.IGetDamage(damage);
            }
        }
        SetttingReset();
    }

    private void SetttingReset()
    {
        count = 0;
        damage = 0;
        offset = Vector2.zero;
        size = Vector3.zero;
        knockbackPower = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2(pos.X, pos.Z) + new Vector2(offset.x, 0), 
            new Vector2(size.x,size.z));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(
            new Vector2(pos.X, pos.Z) + offset, 
            new Vector2(size.x,size.y));
    }
}
