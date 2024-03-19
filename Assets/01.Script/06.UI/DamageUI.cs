using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : PooledObject
{
    TextMeshProUGUI text;
    float time;
    Vector3 offset;
    Vector3 startPos;
    void Awake()
    {
        time = 1.5f;
        text = GetComponent<TextMeshProUGUI>();
        transform.forward = Camera.main.transform.forward;
    }
    public void SetTarget(Transform _target, int damage)
    {
        offset = new Vector3(0,100,0);
        transform.position = Vector3.zero;
        startPos = Camera.main.WorldToScreenPoint(_target.position);
        text.text = damage.ToString();
        if (moveCo != null)
            StopCoroutine(moveCo);
        moveCo = StartCoroutine(MoveCo());
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(WorkTime());
        if(posCo != null)
            StopCoroutine(posCo);
        posCo = StartCoroutine(PositionCo());
    }
    float fontSize;
    Coroutine posCo = null;
    IEnumerator PositionCo()
    {
        while(true)
        {
            offset += (new Vector3(30, 80f, 0) * Time.deltaTime);
            transform.position = startPos + offset;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator UpFontSizeCo()
    {
        while (text.fontSize <= 70)
        {
            fontSize += 8;
            text.fontSize = (int)fontSize;
            yield return new WaitForFixedUpdate();
        }
        fontSize = text.fontSize = 70;
    }

    Coroutine moveCo = null;
    IEnumerator MoveCo()
    {
        text.color = Color.white;
        Color color = text.color;
        fontSize = text.fontSize = 30;

        yield return UpFontSizeCo();

        while (true)
        {
            color.a -= Time.deltaTime;
            color.g -= Time.deltaTime;
            color.b -= Time.deltaTime;

            fontSize -= 1;
            if (text.fontSize <= 20)
                break;

            text.fontSize = (int)fontSize;
            text.color = color;
            
            yield return new WaitForFixedUpdate();
        }
    }

    Coroutine coroutine = null;
    IEnumerator WorkTime()
    {
        yield return new WaitForSeconds(time);
        if(moveCo != null)
            StopCoroutine(moveCo);
        StopCoroutine(posCo);
        StopCoroutine(coroutine);
        Release();
    }

}
