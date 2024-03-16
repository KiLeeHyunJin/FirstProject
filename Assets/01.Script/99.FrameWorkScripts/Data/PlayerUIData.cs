using System;
using UnityEngine;

public class PlayerUIData : MonoBehaviour
{
    [SerializeField]
    StatusUI statusUI;
    PlayerData playerData;
    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }
    public InvenEntry CallSlotData(int idx) => playerData.inventory.GetSlot(idx);
    public InvenEntry[] CallInventoryData(EnumType.ItemType type) => playerData.inventory.GetInventory(type);
    public int CallMaxCount() => playerData.MaxCount;
    public void CallSwapItem(EnumType.ItemType type,int idx1, int idx2) => playerData.inventory.SwapItem(type, idx1, idx2);
    public void SetHp(float _hp) => statusUI.UpdateHp(_hp);
    public void SetMp(float _mp) => statusUI.UpdateMp(_mp);
}
