using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlotEntry : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
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

        if (count == 1)
            text.enabled = false;
        else 
        {
            if (text.enabled == false)
                text.enabled = true;
            text.text = count.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //.OnFirstLayer();
        this.transform.SetAsLastSibling();
    }
    Vector2 startPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (owner.dragData == null)
            owner.dragData = new InventoryDragData();
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
        bool isSucces = owner.HandledDroppedEntry(eventData.position);
        RectTransform t = transform as RectTransform;
        transform.SetParent(owner.dragData.parent, true);
        t.offsetMin = t.offsetMax = Vector2.zero;
    }


}
