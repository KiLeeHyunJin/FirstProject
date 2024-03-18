using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotWindow : MonoBehaviour
{
    public PlayerUIData playerData { get; private set; }
    [SerializeField] QuickSlotEntry prefab;
    [SerializeField] RectTransform[] rects;
    [SerializeField] UnityEngine.UI.Image Layout;
    QuickSlotEntry[] slots;
    public EnumType.ItemType type { get; private set; }
    [field: SerializeField] public Canvas DragCanvas { get; private set; }
    [field: SerializeField] public CanvasScaler DragCanvasScaler { get; private set; }

    private void Start()
    {
        playerData = FindObjectOfType<PlayerUIData>();
        //playerData.SetInventoryUI(this);

        DragCanvas = GetComponentInParent<Canvas>();
        DragCanvasScaler = GetComponentInParent<CanvasScaler>();
        playerData.CheckPlayerData();
        int count = playerData.CallMaxCount();
        slots = new QuickSlotEntry[5];
        UpdateEntry();
    }
    public void ChangeSlot(int slot)
    {
        if (slot >= 5)
            return;

        type = (EnumType.ItemType)slot;

        InvenEntry[] items = playerData.CallInventoryData((EnumType.ItemType)slot);
        for (int i = 0; i < 5; i++)
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
        if (RectTransformUtility.RectangleContainsScreenPoint(Layout.rectTransform, position) == false)
            return false;
        for (int i = 0; i < rects.Length; ++i)
        {
            RectTransform t = rects[i];

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {

            }
        }
        return false;
    }

}
