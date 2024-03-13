using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectController 
{
    public void ISetDamage(int damage, AttackEffectType effectType, float stunTime);
    public void ISetType();
    public StandingState IGetStandingType();
}
