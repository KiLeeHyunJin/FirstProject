using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void IGetDamage(float damage);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="power"> 넉백량</param>
    /// <param name="pos"> 캐릭터 위치 </param>
    /// <param name="size"> 공격 범위</param>
    /// <param name="offset"> 오차 범위</param>
    void ISetKnockback(Vector2 power, Vector3 pos, Vector3 size, Vector2 offset);
}
