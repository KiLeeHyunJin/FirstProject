using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateData
{
    public string Name;
    public int Hp;
    public float MaxHp;
    public Sprite icon;
}
public interface IBaseState
{
    public StateData IGetState();
}
