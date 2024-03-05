using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AttackPos : MonoBehaviour
{
    [SerializeField] TransformPos ownerPos;
    new CircleCollider2D collider;
    Vector2 pushPower;
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }
    public void SetData(Vector2 power, int damage)
    {
        pushPower = power;
    }
    public void OnEnableCollider()
    {
        collider.enabled = true;
        if(co != null)
            StopCoroutine(co);
        co = StartCoroutine(OffCo());
    }
    Coroutine co = null;
    IEnumerator OffCo()
    {
        yield return new WaitForSeconds(0.4f);
        collider.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {

            TransformPos pos = collision.GetComponent<Attackable>().transformPos;

            if (pos == null)
                return;
            if (pos.attackCheck.CheckAttackCollision(
                ownerPos.Pose, 
                new Vector3(collider.radius, collider.radius, collider.radius / 3), 
                collider.offset))
            {
                pos.AddForce(pushPower);
                if (co != null)
                    StopCoroutine(co);
                collider.enabled = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (collider == null)
            return;
        //공격 높이
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector2(ownerPos.X + collider.offset.x, ownerPos.Z + collider.offset.y), 
            Vector2.one * collider.radius * 2);

        //공격 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2(ownerPos.X, ownerPos.Z) + new Vector2(collider.offset.x, 0), 
            new Vector2(collider.radius * 2, (collider.radius / 3) * 2));
    }
}
