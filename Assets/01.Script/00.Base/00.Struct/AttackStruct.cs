using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStruct : MonoBehaviour
{
}
public struct AttackData
{
    public string AnimName;
    [Range(0, 1)]
    public float delay;
    public Vector2 offset;
    public Vector3 size;
    public Vector2 power;
    public Vector2 move;
    public float moveTime;
    public float mana;
    public float damage;
    public int attackCount;
}
