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
            jumpTransform.localPosition =
                new Vector3(0, jumpTransform.localPosition.y, 0);
        else
        {
            jumpTransform.localPosition = Vector3.zero;
            //ResetYPos();
        }
    }

    private void JumpCo()
    {
        if (pos.Y > 0)
            return;
        rigid.constraints =
            RigidbodyConstraints2D.FreezeRotation ;

        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        isJump = true;
        rigid.velocity = new Vector2(0, jumpPower);

        anim.Play("Jump_Up");
        while (rigid.velocity.y > 0f)
            yield return null;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Up"))
            anim.Play("Jump_Down");

        while (pos.Y > 0)
            yield return new WaitForFixedUpdate();
        ResetYPos();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Down"))
        {
            anim.Play("Jump_Land");
            yield return new WaitForSeconds(0.25f);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Land"))
            anim.Play("Idle");

        yield break;
    }

    void ResetYPos()
    {
        pos.Y = 0;
        rigid.constraints =
            RigidbodyConstraints2D.FreezeRotation |
            RigidbodyConstraints2D.FreezePositionY;
        isJump = false;
    }
}

