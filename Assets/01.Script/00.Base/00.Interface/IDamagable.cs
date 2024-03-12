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
    /// <param name="power"> 넉백량</param>
    /// <param name="pos"> 캐릭터 위치 </param>
    /// <param name="size"> 공격 범위</param>
    /// <param name="offset"> 오차 범위</param>
    void ISetKnockback(Vector3 power, Vector3 pos, Vector3 size, Vector2 offset, float pushTime);
    bool ICollision(Vector2 size, Vector3 pos, Vector2 offset);
    Vector2 IGetPos();
    StandingState IGetStandType();
}
