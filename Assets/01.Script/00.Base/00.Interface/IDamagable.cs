using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void IGetDamage(float damage);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="power"> �˹鷮</param>
    /// <param name="pos"> ĳ���� ��ġ </param>
    /// <param name="size"> ���� ����</param>
    /// <param name="offset"> ���� ����</param>
    void ISetKnockback(Vector2 power, Vector3 pos, Vector3 size, Vector2 offset);
}
