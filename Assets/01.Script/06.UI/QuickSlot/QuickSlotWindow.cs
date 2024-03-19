using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotWindow : MonoBehaviour
{
    public PlayerUIData playerData { get; private set; }
    [SerializeField] RectTransform[] rects;
    [SerializeField] QuickSlotEntry[] slots;
    public EnumType.ItemType type { get; private set; }
    [field: SerializeField] public Canvas DragCanvas { get; private set; }
    [field: SerializeField] public CanvasScaler DragCanvasScaler { get; private set; }

    private void Start()
    {
        playerData = FindObjectOfType<PlayerUIData>();
        DragCanvas = GetComponentInParent<Canvas>();
        DragCanvasScaler = GetComponentInParent<CanvasScaler>();
    }

    public void UpdateEntry(int idx, int quickSlotIdx)
    {
        InvenEntry entryData = playerData.CallQuickSlotData(idx);
        if (entryData == null)
            return;
        slots[quickSlotIdx].SetData(entryData.icon, entryData.count);
    }
    public void ClearSlot(int quickSlotIdx) => slots[quickSlotIdx].SetData(null, 0);

    public bool HandledDroppedEntry(Vector3 position, int idx)
    {
        for (int i = 0; i < rects.Length; ++i)
        {
            RectTransform t = rects[i];
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                playerData.CallSetQuick(idx, i);
                return true;
            }
        }
        return false;
    }

}
