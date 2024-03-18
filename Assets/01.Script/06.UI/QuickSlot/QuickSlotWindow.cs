using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotWindow : MonoBehaviour
{
    public PlayerUIData playerData { get; private set; }
    [SerializeField] RectTransform[] rects;
    [SerializeField] UnityEngine.UI.Image Layout;
    [SerializeField] QuickSlotEntry[] slots;
    public EnumType.ItemType type { get; private set; }
    [field: SerializeField] public Canvas DragCanvas { get; private set; }
    [field: SerializeField] public CanvasScaler DragCanvasScaler { get; private set; }

    private void Start()
    {
        playerData = FindObjectOfType<PlayerUIData>();
        DragCanvas = GetComponentInParent<Canvas>();
        DragCanvasScaler = GetComponentInParent<CanvasScaler>();
        slots = new QuickSlotEntry[5];
    }

    public void UpdateEntry(int idx, int quickSlotIdx)
    {
        InvenEntry entryData = playerData.CallQuickSlotData(idx);
        slots[quickSlotIdx].SetData(entryData.icon, entryData.count);
    }

    public bool HandledDroppedEntry(Vector3 position, int idx)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(Layout.rectTransform, position) == false)
            return false;
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
