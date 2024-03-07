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
    int attackCount;
    int targetCount;
    float damage;
    bool isStart;

    public void Start()
    {
        if(maxAttack < 1)
            maxAttack = 5;
        colliders = new Collider2D[maxAttack];
        isStart = true;
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

    public void SetDamage(float _damage, int count)
    {
        damage = _damage;
        attackCount = count;
    }

    public void OnAttackEnable()
    {
        GetCollisionObject();
    }

    private void GetCollisionObject()
    {

        targetCount = Physics2D.OverlapCircleNonAlloc(
            new Vector2(pos.X + offset.x, pos.Z + pos.Y + offset.y)
            , size.x, colliders,layerMask);

        Debug.Log($"count : {targetCount}");
        if(targetCount > 0)
            StartCoroutine(AttackCo());
        //else
        //    SetttingReset();
    }
    private void OnDrawGizmos()
    {
        int temp = isStart ? 2 : 1;
        //����
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2(pos.X, pos.Z) + new Vector2(offset.x , 0),
            new Vector2(size.x, size.z) *temp);

        //����
        Gizmos.color = Color.yellow;
        float realYPos =
            (pos.Z + offset.y + pos.Y) + (size.y * 0.25f * temp);
        Gizmos.DrawWireSphere(new Vector2(pos.X + offset.x, realYPos), size.x  * 0.5f* temp);
    }

    IEnumerator AttackCo()
    {
        for (int i = 0; i < targetCount; i++)
        {
            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.ISetKnockback(
                    knockbackPower,
                    pos.Pose,
                    size,
                    offset
                    );
                damagable.IGetDamage(damage);
            }
            yield return new WaitForSeconds(0.01f);
        }

        //SetttingReset();
    }

    private void SetttingReset()
    {
        attackCount = 0;
        targetCount = 0;
        damage = 0;
        offset = Vector2.zero;
        size = Vector3.zero;
        knockbackPower = Vector2.zero;
    }


}
