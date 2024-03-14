using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct AttackCount
{
    [Range(0, 1)]
    public float AttackTime;
    [Range(0,2)]
    public float Percent;
    public Vector2 power;
    public float pushTime;
    public float stunTime;
    public bool gather;
    public AttackEffectType effectType;
}
[Serializable]
public struct MoveData
{
    [Range(0, 1)]
    public float moveTime;
    public Vector2 move;
    public float movingTime;
}
[Serializable]
public struct AttackSize
{
    public Vector2 offset;
    public Vector3 size;

}

[Serializable]
public struct AttackData
{
    public string AnimName;
    public float soundPlayeTime;
    public AudioClip soundClip;
    [Range(0, 1)]
    public float delay;
    public int damage;
    public int mana;
    public bool chainAnim;
    public bool charging;
    public AttackSize attackSize;
    public MoveData[] move;
    public AttackCount[] attackCounts;
}