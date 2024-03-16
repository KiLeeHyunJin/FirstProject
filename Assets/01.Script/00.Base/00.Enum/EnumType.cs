using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumType : MonoBehaviour
{
    public enum ItemType { Equip,Consume,Gold,}
    public enum StatType { HP,MP}
    public enum ItemState { Fill, Blank}
    public enum EquipType {  Top, Bottom, Shoulder, Belt,Shoes, Emblem,Weapon,Pandent,Bracelet,Ring,END
    }
    public enum ConsumeType {Hp, Mp }
}
