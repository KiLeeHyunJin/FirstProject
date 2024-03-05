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
}
