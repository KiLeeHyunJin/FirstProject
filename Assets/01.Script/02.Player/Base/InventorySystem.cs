using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;
using static UnityEditor.LightingExplorerTableColumn;
public class InvenEntry
{
    public EnumType.ItemType Type;
    public Sprite icon;
    public int count;
}
public class InventorySystem
{
    BaseItem[,] inventory;
    InvenEntry[] getArray;
    public PlayerData data;
    EnumType.ItemType currentUIType;
    public BaseItem GetConsume(int idx)
    { return inventory[(int)ItemType.Consume,idx]; }
    public InvenEntry[] GetInventory(EnumType.ItemType type)
    {
        currentUIType = type;
        for (int i = 0; i < getArray.Length; i++)
        {
            if(inventory[(int)type, i].stateType == ItemState.Fill)
            {
                getArray[i].icon = inventory[(int)type, i].icon;
                getArray[i].count = inventory[(int)type, i].count;
            }
            else
            {
                getArray[i].icon = null;
                getArray[i].count = -1;
            }
        }
        return getArray;
    }
    public InvenEntry GetConsumeData(int idx)
    {
        InvenEntry answer = new InvenEntry() { icon = null, count = 0 };
        if (inventory[(int)EnumType.ItemType.Consume, idx].stateType == ItemState.Fill)
        {
            answer.icon = inventory[(int)EnumType.ItemType.Consume, idx].icon;
            answer.count = inventory[(int)EnumType.ItemType.Consume, idx].count;
        }
        return answer;
    }
    public EnumType.EquipType GetEquipType(int idx)
    {
        if(inventory[(int)ItemType.Equip, idx].stateType == ItemState.Fill)
        {
            EquipItem item = inventory[(int)ItemType.Equip, idx] as EquipItem;
            if (item != null)
                return item.equipType;
        }
        return EquipType.END;
    }
    public InvenEntry GetSlot(int idx)
    {
        InvenEntry answer = new InvenEntry() { icon = null, count = 0 };
        if (inventory[(int)currentUIType, idx].stateType == ItemState.Fill)
        {
            answer.icon = inventory[(int)currentUIType, idx].icon;
            answer.count = inventory[(int)currentUIType, idx].count;
            answer.Type = inventory[(int)currentUIType, idx].itemType;
        }
        return answer;
    }
    public EquipmentSystem equipment { get; private set; }
    public InventorySystem(int maxCount, PlayerData owner)
    {
        data = owner;
        inventory = new BaseItem[(int)ItemType.Gold, maxCount];
        getArray = new InvenEntry[maxCount];
        for (int i = 0; i < getArray.Length; i++)
            getArray[i] = new InvenEntry();

        for (int i = 0; i < (int)ItemType.Gold; i++)
        {
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                BaseItem item = null;
                switch ((EnumType.ItemType)i)
                {
                    case ItemType.Equip:
                        item = new EquipItem(EnumType.ItemState.Blank, this, j);
                        break;
                    case ItemType.Consume:
                        item = new ConsumItem(EnumType.ItemState.Blank, this, j);
                        break;
                    case ItemType.Gold:
                        break;
                }
                inventory[i, j] = item;
            }
               
        }
    }
    public void SetEquipSystem(EquipmentSystem _equipmentSystem) => equipment = _equipmentSystem;
    public void UseItem(EnumType.ItemType type, int idx)
    {
        if (idx >= inventory.GetLength(1))
            return;

        if(inventory[(int)type, idx].stateType == ItemState.Fill)
        {
            if (inventory[(int)type, idx].count > 0)
            {
                if (type == ItemType.Equip)
                {
                    EquipItem equip = inventory[(int)type, idx] as EquipItem;
                    if (equip != null)
                    {
                        EquipType equipType = equip.equipType;
                        inventory[(int)type, idx].Used();
                        data.uIData.CallEquipData(equipType);
                    }
                }
                else
                {
                    inventory[(int)type, idx].Used();
                    data.uIData.UpdateSlot(type, idx);
                }
            }
        }
    }
    public void UpdateSlot(int idx, EnumType.ItemType type) => data.uIData.UpdateSlot(type, idx);

    public void AddItem(int id, ItemType type, int count = 1)
    {
        if(type != ItemType.Equip)
        {
            int idx = SearchItem(id, type);
            if (idx >= 0)
            {
                inventory[(int)type, idx].AddItem(count);
                UpdateSlot(idx, type);
                return;
            }
        }
        int addIdx = SearchBlackSlot(type);
        ItemObjectable item = data.GetItem(id, type);
        if (item == null)
            return;

        if (addIdx >= 0)
        {
            inventory[(int)type, addIdx].SetItemData(count, id, type, item.Icon);

            if(item.Type == ItemType.Equip)
                inventory[(int)type, addIdx].SetEquipData(item.Equip);
            else if(item.Type == ItemType.Consume)
                inventory[(int)type, addIdx].SetConsumeData(item.AddStat, item.Value);

            UpdateSlot(addIdx, type);
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
        {
            bool result  = inventory[(int)type, idx].MinusItem(count);
            UpdateSlot(idx, type);
            return result;
        }
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
        Swap(inventory[(int)type, idx1], inventory[(int)type, idx2]);
        UpdateSlot(idx1, type);
        UpdateSlot(idx2, type);
    }
    void Swap(BaseItem idx1,BaseItem idx2)
    {
    //    switch (currentUIType)
    //    {
    //        case ItemType.Equip:
    //            EquipItem equipItem1 = idx1 as EquipItem;
    //            EquipItem equipItem2 = idx2 as EquipItem;
    //            equipItem1.Swap(equipItem2);
    //            break;
    //        case ItemType.Consume:
    //            ConsumItem consumItem1 = idx1 as ConsumItem;
    //            ConsumItem consumItem2 = idx2 as ConsumItem;
    //            consumItem1.Swap(consumItem2);
    //            break;
    //        case ItemType.Gold:
    //            break;
    //    }
        idx1.Swap(idx2);

        if (idx1.count <= 0)
        {
            idx1.Clear();
        }
        if (idx2.count <= 0)
        {
            idx2.Clear();
        }
    }
}
