using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;
using static UnityEditor.LightingExplorerTableColumn;
using static UnityEditor.Progress;

public class EquipmentSystem
{
    public EquipItem[] equips { get; private set; }
    public InventorySystem inventory { get; private set; }
    PlayerData data;

    public EquipmentSystem(PlayerData owner)
    {
        equips = new EquipItem[(int)EquipType.END];
        data = owner;
    }

    public void SetInventorySystem(InventorySystem _inventory) => inventory = _inventory;
    public Sprite GetData(EnumType.EquipType type)
    {
        if (equips[(int)type] != null)
            return equips[(int)type].icon;
        else
            return null;
    }

    public void EquipItem(EquipItem item)
    {
        if (equips[(int)item.equipType] != null)
            DequipItem(item.equipType);
        equips[(int)item.equipType] = item;

        data.uIData.UpdateEquipSlot(item.equipType);
    }
    public void DequipItem(EquipType itemType)
    {
        int idx = (int)itemType;
        if(DequipCheck(idx))
        {
            equips[idx].Diquip();
            equips[idx] = null;
            data.uIData.UpdateEquipSlot(itemType);
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
