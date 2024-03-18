using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetDataUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image characterIcon;
    [SerializeField] Image hpFill;
    [SerializeField] Image paddingFill;
    float per;
    public void UpdateTargetData(StateData stateData)
    {
        text.text = stateData.Name;
        per = stateData.Hp / stateData.MaxHp;
        if (per < 0)
            per = 0;
        characterIcon.sprite = stateData.icon;
        if (hpCo != null)
            StopCoroutine(hpCo);
        hpCo = StartCoroutine(ChangeHp());

        if (paddingCo != null)
            StopCoroutine(paddingCo);
        paddingCo = StartCoroutine(ChangePadding());
    }

    Coroutine hpCo = null;

    IEnumerator ChangeHp()
    {
        while(per <=hpFill.fillAmount)
        {
            hpFill.fillAmount -= Time.deltaTime * 0.5f;
            yield return new WaitForFixedUpdate();
        }
        hpFill.fillAmount = per;
    }

    Coroutine paddingCo = null;
    IEnumerator ChangePadding()
    {
        while(per < paddingFill.fillAmount)
        {
            paddingFill.fillAmount -= Time.deltaTime * 0.3f;
            yield return new WaitForFixedUpdate();
        }
        paddingFill.fillAmount = per;

        if(per == 0)
        {
            if (hpCo != null)
                StopCoroutine(hpCo);
            if (paddingCo != null)
                StopCoroutine(paddingCo);
            Debug.Log("Closed");
            gameObject.SetActive(false);
        }
    }
}
