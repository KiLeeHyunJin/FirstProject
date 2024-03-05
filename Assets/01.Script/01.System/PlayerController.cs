using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("수직 움직임 속도")]
    [SerializeField] float verticalWalkSpeed;
    [SerializeField] float verticalRunSpeed;

    [Header("수평 움직임 속도")]
    [SerializeField] float horizontalWalkSpeed;
    [SerializeField] float horizontalRunSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float horizontalSpeed;

    [Header("링크")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] CircleCollider2D circleCollider;


    Vector3 moveValue;

    JumpSystem jump;
    Animator anim;
    NormalAttack attack;
    TransformPos transformPos;

    public void Awake()
    {
        jump = GetComponent<JumpSystem>();
        transformPos = GetComponent<TransformPos>();
        transformPos.direction = TransformPos.Direction.Right;
    }

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        attack = GetComponent<NormalAttack>();
        anim.speed = .35f;
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(moveValue.magnitude != 0)
        {
            Vector2 moveVector = new Vector2(moveValue.x * horizontalSpeed, moveValue.y * verticalSpeed);
            rigid.velocity = moveVector;
            TransformPos.Direction before = transformPos.direction;

            if(moveValue.x != 0)
            {
                if (moveValue.x < 0)
                    transformPos.direction = TransformPos.Direction.Left;
                else if (moveValue.x > 0)
                    transformPos.direction = TransformPos.Direction.Right;
            }

            if(before != transformPos.direction)
            {
                renderer.flipX = !renderer.flipX;
                Vector2 colOffset = circleCollider.offset;
                colOffset.x *= -1;
                circleCollider.offset = colOffset;
            }
        }
    }
    float keyDownTime;
    public void OnMove(InputValue inputValue)
    {
        float beforeTime = keyDownTime;
        keyDownTime = Time.time;
        if (keyDownTime - beforeTime < 0.2f)
        {
            anim.Play("Run");
            horizontalSpeed = horizontalRunSpeed;
            verticalSpeed = verticalRunSpeed;
        }
        else
        {
            anim.Play("Walk");
            horizontalSpeed = horizontalWalkSpeed;
            verticalSpeed = verticalWalkSpeed;
        }

        Vector2 value = inputValue.Get<Vector2>().normalized;
        moveValue = new Vector3(value.x, value.y, 0);

        if (moveValue.sqrMagnitude == 0)
        {
            rigid.velocity = Vector2.zero;
            anim.Play("Idle");
        }
    }

    public void OnJump(InputValue inputValue)
    {
        jump.StartJump();
    }

    public void OnBaseAttack(InputValue inputValue)
    {
        attack.InputKeyCount();
    }
}
