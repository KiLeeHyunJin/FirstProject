using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumItem : BaseItem
{
    EnumType.ConsumeType consumeType;
    EnumType.StatType statType;
    int value;
    public ConsumItem(EnumType.ItemState stateType, InventorySystem _inventorySystem, int _idx) : 
        base(stateType, _inventorySystem, _idx)
    {
    }
    public override void SetConsumeData(EnumType.ConsumeType _consume, int _value)
    {
        consumeType = _consume;
        value = _value;
    }
    public override void Used()
    {
        base.Used();
        inventorySystem.data.playerController.AddState(consumeType, value);
        MinusItem(1);
    }
}
