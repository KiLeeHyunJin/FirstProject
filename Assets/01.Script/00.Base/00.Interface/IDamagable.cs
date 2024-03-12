using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void IGetDamage(int damage, AttackEffectType effectType);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="power"> �˹鷮</param>
    /// <param name="pos"> ĳ���� ��ġ </param>
    /// <param name="size"> ���� ����</param>
    /// <param name="offset"> ���� ����</param>
    void ISetKnockback(Vector3 power, Vector3 pos, Vector3 size, Vector2 offset, float pushTime);
    bool ICollision(Vector2 size, Vector3 pos, Vector2 offset);
    Vector2 IGetPos();
    StandingState IGetStandType();
}
