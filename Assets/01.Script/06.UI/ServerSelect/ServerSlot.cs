using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class ServerSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Sprite enableIcon;
    [SerializeField] Sprite disableIcon;
    [SerializeField] Sprite bg;
    [field: SerializeField] public int idx { get; private set; }
    ServerSelect select;
    Image img;
    bool click;
    private void Start()
    {
        img = GetComponent<Image>();
        select = FindObjectOfType<ServerSelect>();
        click = false;
        img.sprite = disableIcon;
    }

    void EnterData()
    {
        if (select == null)
            return;
        click = true;
        select.SelectServer(idx, bg);
    }
    public void Cancle(int _idx)
    {
        if (idx == _idx)
            return;
        click = false;
        img.sprite = disableIcon;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        EnterData();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (click)
            return;
        img.sprite = disableIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = enableIcon;
    }
}
