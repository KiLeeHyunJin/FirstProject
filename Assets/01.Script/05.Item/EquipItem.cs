using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : BaseItem
{
    public EnumType.EquipType equipType { get; private set; }
    public EquipItem(EnumType.ItemState stateType, InventorySystem _inventorySystem,int _idx) : 
        base(stateType, _inventorySystem, _idx)
    {
    }
    public override void Used()
    {
        base.Used();
        Equip();
    }
    public override void SetEquipData(EnumType.EquipType _equip) => equipType = _equip;
    public void Equip()
    {
        inventorySystem.equipment.EquipItem(this);
        inventorySystem.ClearSlot(idx, itemType);
        inventorySystem.UpdateSlot(idx, itemType);
    }
    public void Dequip()
    {
        inventorySystem.AddItem(id, itemType);
    }
    public override void Swap(BaseItem targetItem)
    {
        base.Swap(targetItem);
        EquipItem equip = targetItem as EquipItem;
        if(equip != null)
        {
            EnumType.EquipType targetEquipType = equip.equipType;

            equip.SetEquipData(equipType);

            equipType = targetEquipType;
        }
    }
}
