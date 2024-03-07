using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Mover
{
    [Header("수직 움직임 속도")]
    [SerializeField] float verticalWalkSpeed;
    [SerializeField] float verticalRunSpeed;

    [Header("수평 움직임 속도")]
    [SerializeField] float horizontalWalkSpeed;
    [SerializeField] float horizontalRunSpeed;
    [SerializeField] Vector2 Speed;
    [SerializeField] Vector2 moveVector;

    TransformPos pos;
    PlayerController owner;
    SpriteRenderer renderer;

    public void Setting(TransformPos _pos, PlayerController _owner, SpriteRenderer _renderer)
    {
        pos = _pos;
        owner = _owner;
        renderer = _renderer;
    }

    public void Move(Vector2 moveValue)
    {
        if (moveValue.x != 0 || moveValue.y != 0)
        {
            moveVector = new Vector2(
                moveValue.x * Speed.x,
                moveValue.y * Speed.y);

            pos.AddForceMove(moveVector);

            if (moveValue.x != 0)
            {


            }
        }
        else
        {

        }
    }
    public void Walk()
    {
            Speed = new Vector2(horizontalWalkSpeed, verticalWalkSpeed);
    }
    public void Run()
    {
            Speed = new Vector2(horizontalRunSpeed, verticalRunSpeed);
    }
}
