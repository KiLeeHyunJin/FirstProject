using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public int count { get; private set; }
    public int type { get; private set;}
    public Sprite icon { get; private set;}
    public EnumType.ItemType itemType { get; private set;}
    public void SetItemData(int _count, Sprite _icon, int _type, EnumType.ItemType _itemType)
    {
        count = _count;
        icon = _icon;
        type = _type;
        itemType = _itemType;
    }
}
