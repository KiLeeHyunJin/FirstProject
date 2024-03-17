using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] ItemObjectable[] itemObjectables;
    Dictionary<EnumType.ItemType, Hashtable> itemDic;
    [field : SerializeField] public int MaxCount { get; private set;}
    public InventorySystem inventory;
    public EquipmentSystem equipment;
    public PlayerUIData uIData { get; private set; }
    public PlayerController playerController { get; private set; }
    private void Awake()
    {
        CreateItemDic();
        inventory = new InventorySystem(MaxCount, this);
        equipment = new EquipmentSystem(this);
        inventory.SetEquipSystem(equipment);
        equipment.SetInventorySystem(inventory);
    }
    public void SetUIData(PlayerUIData playerUI) => uIData = playerUI; 
    public void SetPlayer(PlayerController player) =>playerController = player;
    void CreateItemDic()
    {
        itemDic = new Dictionary<EnumType.ItemType, Hashtable>();

        for (int i = 0; i < (int)EnumType.ItemType.Gold; i++)
            itemDic.Add((EnumType.ItemType)i, new Hashtable());

        for (int i = 0; i < itemObjectables.Length; i++)
        {
            if (itemObjectables[i] == null)
                continue;

            itemDic[itemObjectables[i].Type].Add(itemObjectables[i].Id, itemObjectables[i]);
        }
    }
    public ItemObjectable GetItem(int id, EnumType.ItemType type)
    {
        itemDic.TryGetValue(type, out Hashtable hashtable);
        if (hashtable == null)
            return null;
        if (hashtable.ContainsKey(id) == false)
            return null;
        return (ItemObjectable)hashtable[id];
    }


}
