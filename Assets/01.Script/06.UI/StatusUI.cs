using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    enum StateType
    {
        HP, MP
    }
    [SerializeField] Image HpBlankUI; 
    [SerializeField] Image MpBlankUI; 
    [SerializeField] Image HpUI; 
    [SerializeField] Image MpUI;
    [SerializeField] float minusSpeed;

    public void Start()
    {
        if (minusSpeed == 0)
            minusSpeed = 1;
    }
    public void UpdateHp(float value)
    {
        HpUI.fillAmount = value;
        Blinking(StateType.HP);
    }
    public void UpdateMp(float value)
    {
        MpUI.fillAmount = value;
        Blinking(StateType.MP);
    }

    void Blinking(StateType type)
    {
        if(type == StateType.HP)
        {
            if(Hpcoroutine != null)
                StopCoroutine(Hpcoroutine);
            Hpcoroutine = StartCoroutine(LerpCo(HpBlankUI, HpUI));
        }
        else
        {
            if (Mpcoroutine != null)
                StopCoroutine(Mpcoroutine);
            Mpcoroutine = StartCoroutine(LerpCo(MpBlankUI, MpUI));
        }
    }
    Coroutine Mpcoroutine = null;
    Coroutine Hpcoroutine = null;
    IEnumerator LerpCo(Image blank, Image state)
    {
        while(true)
        {
            blank.fillAmount -= Time.deltaTime * minusSpeed;
            if (blank.fillAmount <= state.fillAmount)
            {
                blank.fillAmount = state.fillAmount;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

}
