using UnityEngine.EventSystems;

public class WindowUI : BaseUI, IDragHandler, IPointerClickHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.Translate(eventData.delta);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Manager.UI.SelectWindowUI(this);
    }

    public void Close()
    {
        Manager.UI.CloseWindowUI(this);
    }

}
