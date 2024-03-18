using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class InventoryWindow :WindowUI
{
    public class InventoryDragData
    {
        public InventorySlotEntry entry;
        public Transform parent;
    }
    public PlayerUIData playerData { get; private set; }
    [SerializeField] InventorySlotEntry prefab;
    [SerializeField] RectTransform[] rects;
    [SerializeField] UnityEngine.UI.Image Layout;
    InventorySlotEntry[] slots;
    public EnumType.ItemType type { get; private set; }
    [field: SerializeField] public Canvas DragCanvas { get; private set; }
    [field : SerializeField]public CanvasScaler DragCanvasScaler { get; private set; }
    [HideInInspector] public InventoryDragData dragData;
    
    private void Start()
    {
        playerData = FindObjectOfType<PlayerUIData>();
        playerData.SetInventoryUI(this);

        DragCanvas = GetComponentInParent<Canvas>();
        DragCanvasScaler = GetComponentInParent<CanvasScaler>();
        dragData = new InventoryDragData();
        int count = playerData.CallMaxCount();
        slots = new InventorySlotEntry[count];
        for (int i = 0; i < count; i++)
        {
            InventorySlotEntry obj = Instantiate(prefab, rects[i].transform);
            slots[i] = obj;
            slots[i].SetIndex(i,this);
        }
        UpdateEntry();
    }
    public void ChangeSlot(int itemType)
    {
        if (itemType >= (int)EnumType.ItemType.Gold)
            return;

        type = (EnumType.ItemType)itemType;

        InvenEntry[] items = playerData.CallInventoryData((EnumType.ItemType)itemType);
        for (int i = 0; i < items.Length; i++)
            slots[i].SetData(items[i].icon, items[i].count);
    }
    public void UpdateEntry(int idx = -1)
    {
        if (idx < 0)
        {
            ChangeSlot((int)type);
            return;
        }

        InvenEntry entryData = playerData.CallSlotData(idx);
        slots[idx].SetData(entryData.icon, entryData.count);
    }
    public bool HandledDroppedEntry(Vector3 position)
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(Layout.rectTransform,position) == false)
            return false;
        for (int i = 0; i < rects.Length; ++i)
        {
            RectTransform t = rects[i];

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                if (slots[i] != dragData.entry)
                {
                    playerData.CallSwapItem(type, dragData.entry.idx, i);
                    UpdateEntry(dragData.entry.idx);
                    UpdateEntry(i);

                    return true;
                }
            }
        }
        return false;
    }
    public bool DequipDroppedEntry(Vector3 position)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(Layout.rectTransform, position) == false)
            return false;
        if (type != EnumType.ItemType.Equip)
            return false;
        for (int i = 0; i < rects.Length; ++i)
        {
            RectTransform t = rects[i];

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }
}
