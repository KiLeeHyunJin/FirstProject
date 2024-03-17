using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class ItemObjectable : ScriptableObject
{
    [SerializeField]
    private int id;
    public int Id { get { return id; } }

    [SerializeField]
    private string itemName;
    public string Name { get { return itemName; } }

    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get { return icon; } }

    [SerializeField]
    private EnumType.ItemType type;
    public EnumType.ItemType Type { get { return type; } }
    [SerializeField]
    private EnumType.EquipType equipType;
    public EnumType.EquipType Equip { get { return equipType; } }

    [SerializeField]
    private int  count;
    public int Count { get { return count; } }

    [SerializeField]
    private ConsumeType addStat;
    public ConsumeType AddStat { get { return addStat; } }

    [SerializeField]
    private int value;
    public int Value { get { return value; } }
}
