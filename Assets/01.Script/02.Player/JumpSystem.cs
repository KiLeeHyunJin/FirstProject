using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.UI.Image;

public class JumpSystem : MonoBehaviour
{
    TransformPos pos;
    [SerializeField] Rigidbody2D rigid;
    Animator anim;
    [SerializeField] float jumpPower = 5;
    [SerializeField] bool isJump;
    Transform jumpTransform;
    private void Awake()
    {
        pos = GetComponent<TransformPos>();
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        jumpTransform = rigid.gameObject.transform;
        rigid.constraints = 
            RigidbodyConstraints2D.FreezeRotation | 
            RigidbodyConstraints2D.FreezePositionY;
    }

    public void StartJump() => JumpCo();

    public void FixedUpdate()
    {
        if (isJump)
            rigid.transform.localPosition =
                new Vector3(0, rigid.transform.localPosition.y, 0);
        else
            rigid.transform.localPosition = Vector3.zero;
    }

    private void JumpCo()
    {
        if (pos.Y > 0)
            return;
        rigid.constraints =
            RigidbodyConstraints2D.FreezeRotation;

        
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        isJump = true;
        rigid.velocity = new Vector2(0, jumpPower);

        anim.SetTrigger("JumpStart");
        while (rigid.velocity.y > 0f)
            yield return null;

        anim.SetTrigger("JumpDown");
        while(pos.Y > 0)
            yield return new WaitForFixedUpdate();

        pos.Y = 0;
        rigid.constraints = 
            RigidbodyConstraints2D.FreezeRotation | 
            RigidbodyConstraints2D.FreezePositionY;
        isJump = false;
        yield break;
    }


}
