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
    /// <param name="power"> 넉백량</param>
    /// <param name="pos"> 캐릭터 위치 </param>
    /// <param name="size"> 공격 범위</param>
    /// <param name="offset"> 오차 범위</param>
    void ISetKnockback(Vector3 power,float stunTime, float pushTime);
    bool ICollision(Vector3 size, Vector3 pos, Vector2 offset);
    int IGetRenderLayerNum();
    Vector3 IGetPos();
    public Vector2 IGetOffset();
    StandingState IGetStandType();
}
