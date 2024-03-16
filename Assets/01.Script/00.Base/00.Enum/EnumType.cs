using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumType : MonoBehaviour
{
    public enum ItemType { Equip,Consume,Gold,}
    public enum StatType { HP,MP}
    public enum ItemState { Fill, Blank}
    public enum EquipType { Helmet, Shoulder, Top, Bottom, Shoes, Weapon, END
    }
    public enum ConsumeType {Hp, Mp }
}
