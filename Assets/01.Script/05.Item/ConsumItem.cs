using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumItem : BaseItem
{
    public ConsumItem(EnumType.ItemState stateType, EnumType.ConsumeType _consumeType, InventorySystem _inventorySystem, int _idx) : 
        base(stateType, _inventorySystem, _idx)
    {
        consumeType = _consumeType;
    }
    EnumType.ConsumeType consumeType;

    public override void Used()
    {
        base.Used();
    }
}
