using UnityEngine;
using static EnumType;

public class EquipmentSystem
{
    public EquipItem[] equips { get; private set; }
    public InventorySystem inventory { get; private set; }
    PlayerData data;

    public EquipmentSystem(PlayerData owner)
    {
        data = owner;
    }

    public void SetInventorySystem(InventorySystem _inventory)
    {
        inventory = _inventory;
        equips = new EquipItem[(int)EquipType.END];

        for (int i = 0; i < equips.Length; i++)
            equips[i] = new EquipItem(ItemState.Blank, inventory, i);
    }
    public Sprite GetData(EnumType.EquipType type)
    {
        if (equips[(int)type] != null)
            return equips[(int)type].icon;
        else
            return null;
    }

    public void EquipItem(EquipItem item)
    {
        if (equips[(int)item.equipType].stateType != ItemState.Blank)
            DequipItem(item.equipType);

        equips[(int)item.equipType].SetItemData(1, item.id, item.itemType, item.icon);
        equips[(int)item.equipType].SetEquipData(item.equipType);

        data.uIData.UpdateEquipSlot(item.equipType);
    }
    public void DequipItem(EquipType itemType)
    {
        int idx = (int)itemType;
        if (DequipCheck(idx))
        {
            equips[idx].Dequip();
            equips[idx].Clear();
            data.uIData.UpdateEquipSlot(itemType);
        }
    }
    bool DequipCheck(int idx)
    {
        if (equips.Length <= idx)
            return false;
        if (equips[idx].stateType == ItemState.Fill)
            return true;
        return false;

    }
}
