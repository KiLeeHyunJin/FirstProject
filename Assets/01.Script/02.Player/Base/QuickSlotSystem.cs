using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;
using static UnityEditor.Progress;

public class QuickSlotSystem : MonoBehaviour
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
    }
    public void UseKey(KeyManager.SlotKey key)
    {
        if (consume[(int)key].stateType == ItemState.Fill)
            inventory.UseItem(ItemType.Consume, invenIdx[(int)key]);
        if (consume[(int)key].count <= 0)
        {
            consume[(int)key] = null;
            data.uIData.UpdateQuickSlot(invenIdx[(int)key], (int)key);
            invenIdx[(int)key] = -1;
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
            consume[saveIdx] = consum;
            invenIdx[saveIdx] = idx;
            data.uIData.UpdateQuickSlot(idx, saveIdx);
        }
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
