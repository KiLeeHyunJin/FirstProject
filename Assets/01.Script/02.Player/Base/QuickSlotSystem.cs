using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumType;

public class QuickSlotSystem : MonoBehaviour
{
    public ConsumItem[] consume { get; private set; }
    int[] invenIdx;
    public InventorySystem inventory { get; private set; }
    PlayerData data;

    public QuickSlotSystem(PlayerData owner)
    {
        data = owner;
    }
    public void UseKey(KeyManager.SlotKey key)
    {
        if (consume[(int)key].stateType == ItemState.Fill)
            inventory.UseItem(ItemType.Consume, invenIdx[(int)key]);
    }
    public void SetInventorySystem(InventorySystem _inventory)
    {
        inventory = _inventory;
        consume = new ConsumItem[5];
        invenIdx = new int[5];
        //for (int i = 0; i < consume.Length; i++)
        //    consume[i] = new ConsumItem(ItemState.Blank, inventory, i);
    }
    public Sprite GetData(KeyManager.SlotKey key)
    {
        if (consume[(int)key].stateType == ItemState.Fill)
            return consume[(int)key].icon;
        else
            return null;
    }

    public void SetItem(ConsumItem item, int idx, KeyManager.SlotKey key)
    {
        if (consume[(int)key].stateType != ItemState.Blank)
            //consume[(int)key].Clear();
            consume[(int)key] = null;
        invenIdx[(int)key] = idx;
        consume[(int)key] = item;
        //consume[(int)key].SetItemData(item.count, item.id, item.itemType, item.icon);
        //consume[(int)key].SetConsumeData(item.consumeType, item.value);
        //업데이트 
       // data.uIData.UpdateEquipSlot(item);
    }

}
