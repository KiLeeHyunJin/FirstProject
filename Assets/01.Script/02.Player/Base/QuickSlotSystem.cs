using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;
using static UnityEditor.Progress;

public class QuickSlotSystem
{
    public ConsumItem[] consume { get; private set; }
    int[] invenIdx;
    public InventorySystem inventory { get; private set; }
    PlayerData data;

    public QuickSlotSystem(PlayerData owner)
    {
        data = owner;
        consume = new ConsumItem[5];
        invenIdx = new int[5];
        for (int i = 0; i < invenIdx.Length; i++)
        {
            invenIdx[i] = -1;
        }
    }
    public void UseKey(KeyManager.SlotKey key)
    {
        if (consume[(int)key] == null)
            return;
        if (consume[(int)key].stateType == ItemState.Fill)
            inventory.UseItem(ItemType.Consume, invenIdx[(int)key]);
        if (consume[(int)key].count <= 0)
        {
            //consume[(int)key] = null;
            consume[(int)key].Used();
            data.uIData.UpdateQuickSlot(invenIdx[(int)key], (int)key);
            data.uIData.UpdateSlot(EnumType.ItemType.Consume, invenIdx[(int)key]);
            if (consume[(int)key].stateType == ItemState.Blank)
                Clear((int)key);
        }
    }
    public void SetInventorySystem(InventorySystem _inventory)
    {
        inventory = _inventory;
    }

    public void CallSetItem(int idx, int saveIdx)
    {
        ConsumItem consum  = inventory.GetConsume(idx) as ConsumItem;  
        if(consum != null)
        {
            InsertItemCheck(idx);
            consume[saveIdx] = consum;
            invenIdx[saveIdx] = idx;
            data.uIData.UpdateQuickSlot(idx, saveIdx);
        }
    }
    void InsertItemCheck(int idx)
    {
        for (int i = 0; i < invenIdx.Length; i++)
        {
            if (invenIdx[i] == idx)
            {
                Clear(i);
            }
        }
    }
    void Clear(int idx)
    {
        consume[idx] = null;
        invenIdx[idx] = -1;
        data.uIData.UpdateClearQuickSlot(idx);
        return;
    }
    void SetItem(ConsumItem item, int idx, KeyManager.SlotKey key)
    {
        consume[(int)key] = null;
        invenIdx[(int)key] = idx;
        consume[(int)key] = item;
        //consume[(int)key].SetItemData(item.count, item.id, item.itemType, item.icon);
        //consume[(int)key].SetConsumeData(item.consumeType, item.value);
        //업데이트 
       // data.uIData.UpdateEquipSlot(item);
    }

}
