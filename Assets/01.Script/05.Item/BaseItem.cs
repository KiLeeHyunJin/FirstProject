using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem
{
    public BaseItem(EnumType.ItemState stateType, InventorySystem _inventorySystem, int _idx)
    {
        this.stateType = stateType;
        inventorySystem = 
        inventorySystem = _inventorySystem;
        idx = _idx;
    }

    protected InventorySystem inventorySystem;
    protected int idx;
    public int count { get; private set; }
    public int id { get; private set; }
    public Sprite icon { get; private set; }
    public EnumType.ItemType itemType { get; private set;}
    public EnumType.ItemState stateType { get; private set; }
    public void SetItemData(int _count, int _id, EnumType.ItemType _itemType, Sprite _icon)
    {
        stateType = EnumType.ItemState.Fill;
        count = _count;
        id = _id;
        itemType = _itemType;
        icon = _icon;

    }
    public virtual void SetEquipData(EnumType.EquipType _equip = EnumType.EquipType.Top)
    {
    }

    public virtual void SetConsumeData(EnumType.ConsumeType _consume, int _value)
    {
    }

    public void AddItem(int _count) => count += _count;
    public bool MinusItem(int _count)
    {
        if (_count > count)
            return false;
        count -= _count;
        if (count <= 0)
            stateType = EnumType.ItemState.Blank;
        return true;
    }
    public void Clear()
    {
        stateType = EnumType.ItemState.Blank;
        count = 0;
        icon = null;
        
    }
    public virtual void Used()
    {

    }
}
