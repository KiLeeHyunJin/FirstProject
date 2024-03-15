using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    bool IGetDamage(int damage, AttackEffectType effectType);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="power"> �˹鷮</param>
    /// <param name="pos"> ĳ���� ��ġ </param>
    /// <param name="size"> ���� ����</param>
    /// <param name="offset"> ���� ����</param>
    void ISetKnockback(Vector3 power,float stunTime, float pushTime);
    bool ICollision(Vector3 size, Vector3 pos, Vector2 offset);
    int IGetRenderLayerNum();
    Vector3 IGetPos();
    public Vector2 IGetOffset();
    StandingState IGetStandType();
}
