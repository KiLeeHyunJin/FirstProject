using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.LightingExplorerTableColumn;

public class InventoryDragData
{
    public InventorySlotEntry entry;
    public Transform parent;
}
public class InventoryWindow : MonoBehaviour//WindowUI
{
    PlayerUIData playerData;
    [SerializeField] InventorySlotEntry prefab;
    [SerializeField] RectTransform[] rects;
    InventorySlotEntry[] slots;
    EnumType.ItemType type;
    [field: SerializeField] public Canvas DragCanvas { get; private set; }
    [field : SerializeField]public CanvasScaler DragCanvasScaler { get; private set; }
    [HideInInspector] public InventoryDragData dragData;
    private void Start()
    {
        playerData = FindObjectOfType<PlayerUIData>();
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
        //UpdateEntry();
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

                    Debug.Log("교환");
                    return true;
                }
            }
        }
        Debug.Log("실패");
        return false;
    }
}
