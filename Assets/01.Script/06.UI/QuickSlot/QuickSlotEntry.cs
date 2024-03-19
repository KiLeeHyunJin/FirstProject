using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotEntry : MonoBehaviour
{
    Image Icon;
    TextMeshProUGUI text;
    QuickSlotWindow owner;
    public int idx { get; private set; }

    private void Awake()
    {
        Icon = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
    }
    public void SetIndex(int _idx, QuickSlotWindow _owner)
    {
        idx = _idx;
        owner = _owner;
    }
    public void SetData(Sprite icon, int count)
    {
        if (icon == null)
        {
            Icon.enabled = false;
            return;
        }
        else
        {
            if (Icon.enabled == false)
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
}
