using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUIData : MonoBehaviour
{
    [SerializeField]
    StatusUI statusUI;
    PlayerData playerData;
    [SerializeField]
    TargetDataUI targetDataUI;
    [SerializeField]
    public SkillCoolTimeUI coolTimeUI;
    [field: SerializeField] public InventoryWindow inventoryWindow { get; private set; }
    [field: SerializeField] public EquipmentWindow equipmentWindow { get; private set; }
    [field :SerializeField] public QuickSlotWindow quickSlotWindow { get; private set; }
    [SerializeField] ComboUI comboUI;
    [SerializeField] float targetUIUpdateTime;
    int comboCount;
    private void Start()
    {
        if (playerData == null)
            playerData = FindObjectOfType<PlayerData>();
        playerData.SetUIData(this);
        coolTimeUI = GetComponentInChildren<SkillCoolTimeUI>();
        comboCount = 0;
        //inventoryWindow = GetComponentInChildren<InventoryWindow>();
        //equipmentWindow = GetComponentInChildren<EquipmentWindow>();

    }
    //public void SetInventoryUI(InventoryWindow inventory) => inventoryWindow = inventory;
    //public void SetEquipUI(EquipmentWindow equipment) => equipmentWindow = equipment;
    public void AddComboCount()
    {
        comboUI.SetCount(++comboCount);
    }
    public void ResetComboCount()
    {
        comboUI.SetCount(comboCount = 0);
    }
    public int CallMaxCount() => playerData.MaxCount;
    public void UpdateSlot(EnumType.ItemType type, int idx)
    {
        if (inventoryWindow.gameObject.activeSelf == false)
            return;
        if (inventoryWindow.type == type)
            inventoryWindow.UpdateEntry(idx);
    }
    public void CheckPlayerData()
    {
        if (playerData == null)
            playerData = FindObjectOfType<PlayerData>();
    }
    public void UpdateTargetData(StateData targetData)
    {
        if(targetUI != null)
            StopCoroutine(targetUI);

        targetUI = StartCoroutine(UpdateTargetUICo());

        if (targetDataUI.gameObject.activeSelf == false)
            targetDataUI.gameObject.SetActive(true);

        targetDataUI.UpdateTargetData(targetData);
    }
    public void UpdateEquipSlot(EnumType.EquipType type) => equipmentWindow.UpdateEntry(type);

    public void UpdateQuickSlot(int idx, int quickSlotIdx) => quickSlotWindow.UpdateEntry(idx, quickSlotIdx);
    public void CallSetQuick(int idx, int saveIdx) => playerData.quickSlot.CallSetItem(idx, saveIdx);
    public void CallUsedItem(EnumType.ItemType type, int idx) => playerData.inventory.UseItem(type, idx);
    public void CallSwapItem(EnumType.ItemType type,int idx1, int idx2) => playerData.inventory.SwapItem(type, idx1, idx2);
    public void CallDequipItem(int idx) => playerData.equipment.DequipItem((EnumType.EquipType)idx);
    public EnumType.EquipType CallEquipType(int idx) => playerData.inventory.GetEquipType(idx);
    public Sprite CallEquipData(EnumType.EquipType type) => playerData.equipment.GetData(type);
    public InvenEntry CallSlotData(int idx) => playerData.inventory.GetSlot(idx);
    public InvenEntry[] CallInventoryData(EnumType.ItemType type) => playerData.inventory.GetInventory(type);
    public InvenEntry CallQuickSlotData(int idx) => playerData.inventory.GetConsumeData(idx);
    public void SetHp(float _hp) => statusUI.UpdateHp(_hp);
    public void SetMp(float _mp) => statusUI.UpdateMp(_mp);

    void OnI(InputValue value)
    {
        if (inventoryWindow.gameObject.activeSelf == false)
            inventoryWindow.gameObject.SetActive(true);
        else
            inventoryWindow.gameObject.SetActive(false);
    }
    Coroutine targetUI = null;
    IEnumerator UpdateTargetUICo()
    {
        yield return new WaitForSeconds(targetUIUpdateTime);
    }
}
