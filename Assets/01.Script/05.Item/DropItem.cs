using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : PooledObject
{
    [field:SerializeField] public int id { get; private set; }
    [field:SerializeField] public Sprite icon { get; private set; }
    [field:SerializeField] public EnumType.ItemType type { get; private set; }
    public void SetItem(int _id, EnumType.ItemType _type, Sprite _icon)
    {
        id = _id;
        icon = _icon;
        type = _type;
        GetComponent<SpriteRenderer>().sprite = icon;
    }
}
