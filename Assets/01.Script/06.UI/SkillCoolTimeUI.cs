using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    [SerializeField] PlayerUIData uiData;
    [SerializeField] Image[] skillSlot;
    [SerializeField] TextMeshProUGUI[] texts;
    // Start is called before the first frame update
    void Start()
    {
        uiData = GetComponentInParent<PlayerUIData>();  
    }
    public void UpdateSkillSlot(int idx, float per, int time)
    {
        skillSlot[idx].fillAmount = per;
        texts[idx].text = time.ToString();
        if (per <= 0)
            texts[idx].text = "";
    }
}
