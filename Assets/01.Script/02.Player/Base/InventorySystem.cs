using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;
using static UnityEditor.LightingExplorerTableColumn;
public class InvenEntry
{
    public Sprite icon;
    public int count;
}
public class InventorySystem
{
    BaseItem[,] inventory;
    InvenEntry[] getArray;
    PlayerData data;
    EnumType.ItemType currentUIType;
    public InvenEntry[] GetInventory(EnumType.ItemType type)
    {
        currentUIType = type;
        for (int i = 0; i < getArray.Length; i++)
        {
            getArray[i].icon = null;//inventory[(int)type, i].ico;
            getArray[i].count = 0;
        }
        return getArray;
    }
    public InvenEntry GetSlot(int idx)
    {
        return new InvenEntry() { 
            icon = inventory[(int)currentUIType,idx].icon, 
            count = 0 };
    }
    public EquipmentSystem equipment { get; private set; }
    public InventorySystem(int maxCount, PlayerData owner)
    {
        data = owner;
        inventory = new BaseItem[(int)ItemType.Gold, maxCount];
        getArray = new InvenEntry[maxCount];
        for (int i = 0; i < getArray.Length; i++)
            getArray[i] = new InvenEntry();

        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                inventory[i, j] = new BaseItem(EnumType.ItemState.Blank,this,j);
            }
        }
    }
    public void SetEquipSystem(EquipmentSystem _equipmentSystem) => equipment = _equipmentSystem;

    public void AddItem(int id, ItemType type, int count = 1)
    {
        if(type != ItemType.Equip)
        {
            int idx = SearchItem(id, type);
            if (idx >= 0)
            {
                inventory[(int)type, idx].AddItem(count);
                return;
            }
        }
        int addIdx = SearchBlackSlot(type);
        ItemObjectable item = data.GetItem(id, type);
        if(addIdx >= 0)
        {
            inventory[(int)type, addIdx].SetItemData(count, id, type, addIdx, item.Icon);
        }
    }

    int SearchBlackSlot(ItemType type)
    {
        for (int i = 0; i < inventory.GetLength(1); i++)
        {
            if (inventory[(int)type,i].stateType == ItemState.Blank)
                return i;
        }
        return -1;
    }

    public void ClearSlot(int idx, ItemType type) => inventory[(int)type,idx].Clear();

    public bool RemoveItem(int idx, ItemType type,int count = 1)
    {
        if (inventory.GetLength(1) <= idx || 0 > idx)
            return false;
        if(inventory[(int)type, idx].count >= count)
            return inventory[(int)type, idx].MinusItem(count);
        return false;
    }

    int SearchItem(int id, ItemType type)
    {
        for (int i = 0; i < inventory.GetLength(1); i++)
        {
            if (inventory[(int)type, i].stateType == ItemState.Fill)
            {
                if (inventory[(int)type, i].id == id)
                    return i;
            }
        }
        return -1;
    }

    public void SwapItem(ItemType type, int idx1, int idx2)
    {
        if (inventory[(int)type, idx1].stateType == ItemState.Blank || inventory[(int)type, idx2].stateType == ItemState.Blank)
        {
            if (inventory[(int)type, idx1] == null)
            {
                inventory[(int)type, idx1] = inventory[(int)type, idx2];
                inventory[(int)type, idx2].Clear();
            }
            else
            {
                inventory[(int)type, idx2] = inventory[(int)type, idx1];
                inventory[(int)type, idx1].Clear();
            }
        }
        else
        {
            BaseItem temp = inventory[(int)type, idx1];
            inventory[(int)type, idx1] = inventory[(int)type, idx2];
            inventory[(int)type, idx2] = temp;
        }

    }
}
