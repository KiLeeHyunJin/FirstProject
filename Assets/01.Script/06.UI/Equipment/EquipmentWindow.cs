using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentWindow : MonoBehaviour
{
    public class InventoryDragData
    {
        public EquipSlotEntry entry;
        public Transform parent;
    }
    PlayerUIData playerData;

    [SerializeField] RectTransform[] rects;
    [SerializeField] EquipSlotEntry[] equips;

    [field: SerializeField] public Canvas DragCanvas { get; private set; }
    [field: SerializeField] public CanvasScaler DragCanvasScaler { get; private set; }
    [HideInInspector] public InventoryDragData dragData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = FindObjectOfType<PlayerUIData>();
        playerData.SetEquipUI(this);
        DragCanvas = GetComponentInParent<Canvas>();
        DragCanvasScaler = GetComponentInParent<CanvasScaler>();
        dragData = new InventoryDragData();
        for (int i = 0; i < (int)EnumType.EquipType.END; i++)
        {
            equips[i].SetIndex(i,this);
        }
    }
    public void UpdateEntry(EnumType.EquipType type)
    {
        Sprite icon = playerData.CallEquipData(type);
        equips[(int)type].SetData(icon);
    }

}
