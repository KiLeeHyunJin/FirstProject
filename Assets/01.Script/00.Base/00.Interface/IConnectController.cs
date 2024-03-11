using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectController 
{
    public void ISetDamage(int damage, AttackEffectType effectType);
    public void ISetType();
    public StandingState IGetStandingType();
}
