using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : BaseItem
{
    public EquipItem(EnumType.ItemState stateType, EnumType.EquipType _equipType, InventorySystem _inventorySystem,int _idx) : 
        base(stateType, _inventorySystem, _idx)
    {
        this.equipType = _equipType;
    }
    public EnumType.EquipType equipType { get; private set; }
    public override void Used()
    {
        base.Used();
    }
    public void Equip()
    {
        inventorySystem.equipment.EquipItem(this);
        inventorySystem.ClearSlot(idx, itemType);
    }
    public void Diquip()
    {
        inventorySystem.AddItem(id, itemType);
    }
}
