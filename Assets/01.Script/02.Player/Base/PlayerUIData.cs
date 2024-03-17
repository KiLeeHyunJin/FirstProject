using System;
using UnityEngine;

public class PlayerUIData : MonoBehaviour
{
    [SerializeField]
    StatusUI statusUI;
    PlayerData playerData;
    public InventoryWindow inventoryWindow { get; private set; }
    public EquipmentWindow equipmentWindow { get; private set; }
    private void Start()
    {
        if(playerData == null)
            playerData = FindObjectOfType<PlayerData>();
        playerData.SetUIData(this);
    }
    public void SetInventoryUI(InventoryWindow inventory) => inventoryWindow = inventory;
    public void SetEquipUI(EquipmentWindow equipment) => equipmentWindow = equipment;

    public int CallMaxCount()
    {
        if (playerData == null)
            playerData = FindObjectOfType<PlayerData>();
        return playerData.MaxCount;
    }

    public void UpdateSlot(EnumType.ItemType type, int idx)
    {
        if (inventoryWindow.type == type)
            inventoryWindow.UpdateEntry(idx);
    }
    public void CallUsedItem(EnumType.ItemType type, int idx) => playerData.inventory.UseItem(type, idx);
    public void CallSwapItem(EnumType.ItemType type,int idx1, int idx2) => playerData.inventory.SwapItem(type, idx1, idx2);
    public void UpdateEquipSlot(EnumType.EquipType type) => equipmentWindow.UpdateEntry(type);
    public Sprite CallEquipData(EnumType.EquipType type) => playerData.equipment.GetData(type);
    public InvenEntry CallSlotData(int idx) => playerData.inventory.GetSlot(idx);
    public InvenEntry[] CallInventoryData(EnumType.ItemType type) => playerData.inventory.GetInventory(type);
    public void CallDequipItem(int idx) => playerData.equipment.DequipItem((EnumType.EquipType)idx);

    public void SetHp(float _hp) => statusUI.UpdateHp(_hp);
    public void SetMp(float _mp) => statusUI.UpdateMp(_mp);
}
