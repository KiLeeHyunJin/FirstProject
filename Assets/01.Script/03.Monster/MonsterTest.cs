using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    [SerializeField] TransformPos transformPos;
    [SerializeField] Animator anim;

    private void FixedUpdate()
    {
        if (transformPos.Y <= 0)
            anim.Play("Idle");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Attackable attackable = collision.gameObject.GetComponent<Attackable>();
            if (attackable == null)
                return;

            attackable.ISetKnockback(new Vector2(2, 3), transformPos.Pose,transformPos.Size, transformPos.Offset);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(
            new Vector2
            (transformPos.Pose.x + transformPos.Offset.x, transformPos.Pose.z + transformPos.Offset.y) - 
            (new Vector2
            (transformPos.Size.x, transformPos.Size.z) / 2),
            new Vector2(transformPos.Size.x, transformPos.Size.z) * 2);
    }
}
