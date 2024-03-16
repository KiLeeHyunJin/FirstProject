using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;
using static UnityEditor.LightingExplorerTableColumn;

public class EquipmentSystem
{
    public EquipItem[] equips { get; private set; }
    public EquipmentSystem(PlayerData owner)
    {
        equips = new EquipItem[(int)EquipType.END];
        data = owner;
    }
    public InventorySystem inventory { get; private set; }
    PlayerData data;
    public void SetInventorySystem(InventorySystem _inventory) => inventory = _inventory;
    public void EquipItem(EquipItem item)
    {
        if (equips[(int)item.equipType] != null) //장착 장비가 있을 시 
            equips[(int)item.equipType] = item;
    }
    public void DequipItem(EquipType itemType)
    {
        int idx = (int)itemType;
        if(DequipCheck(idx))
        {
            equips[idx].Diquip();
            equips[idx] = null;
        }
    }
    bool DequipCheck(int idx)
    {
        if (equips.Length <= idx)
            return false;
        if (equips[idx] != null)
            return true;
        return false;

    }
}
