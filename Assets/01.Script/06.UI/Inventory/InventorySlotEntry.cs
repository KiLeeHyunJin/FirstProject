using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotEntry : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPointerDownHandler
{
    Image Icon;
    TextMeshProUGUI text;
    InventoryWindow owner;
    public int idx { get; private set; }

    private void Awake()
    {
        Icon = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
    }
    public void SetIndex(int _idx, InventoryWindow _owner)
    {
        idx = _idx;
        owner = _owner;
    }
    public void SetData(Sprite icon, int count)
    {
        if(icon == null)
        {
            Icon.enabled = false;
            return;
        }
        else
        {
            if(Icon.enabled == false)
                Icon.enabled = true;
            Icon.sprite = icon;
        }
        if (text == null)
            return;
        if (count == 1)
            text.enabled = false;
        else 
        {
            if (text.enabled == false)
                text.enabled = true;
            text.text = count.ToString();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    float time;
    public void OnPointerClick(PointerEventData eventData)
    {
        float before = time;
        time = Time.time;
        if(time - before <= 0.35f)
            owner.playerData.CallUsedItem(owner.type, idx);

        //ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerClickHandler);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (owner.dragData == null)
            owner.dragData = new InventoryWindow.InventoryDragData();

        owner.dragData.entry = this;
        owner.dragData.parent = (RectTransform)transform.parent;

        transform.SetParent(owner.DragCanvas.transform, true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = transform.localPosition + UnscaleEventDelta(eventData.delta);
    }

    protected Vector3 UnscaleEventDelta(Vector3 vec)
    {
        Vector2 referenceResolution = owner.DragCanvasScaler.referenceResolution;
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        float widthRatio = currentResolution.x / referenceResolution.x;
        float heightRatio = currentResolution.y / referenceResolution.y;
        float ratio = Mathf.Lerp(widthRatio, heightRatio, owner.DragCanvasScaler.matchWidthOrHeight);

        return vec / ratio;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(owner.HandledDroppedEntry(eventData.position))
        {

        }
        else if (owner.type == EnumType.ItemType.Equip)
        {
            EnumType.EquipType equip = owner.playerData.CallEquipType(idx);

            if (equip != EnumType.EquipType.END)
            {
                if (owner.playerData.equipmentWindow.HandleDroppedEntryPosition(equip, eventData.position))
                {
                    owner.playerData.CallUsedItem(owner.type, idx);
                }
            }
        }
        else if(owner.type == EnumType.ItemType.Consume)
        {
            if(owner.playerData.quickSlotWindow.HandledDroppedEntry(eventData.position, idx))
            {

            }
        }

        RectTransform t = transform as RectTransform;
        transform.SetParent(owner.dragData.parent, true);
        t.offsetMin = t.offsetMax = Vector2.zero;
    }


}
