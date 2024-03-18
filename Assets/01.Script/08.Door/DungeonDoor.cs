using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField] Sprite Flash;
    [SerializeField] SpriteRenderer closeRender;
    [SerializeField] SpriteRenderer flashRender;

    public void OpenDoor()
    {
        if(flashRender.gameObject.activeSelf == false)
            flashRender.gameObject.SetActive(true);
        closeRender.gameObject.SetActive(false);
        coroutine = StartCoroutine(OpenFlash());
    }
    public void CloseDoor()
    {
        closeRender.gameObject.SetActive(true);
        flashRender.gameObject.SetActive(false);
    }
    Coroutine coroutine = null;
    bool flashOn = true;
    IEnumerator OpenFlash()
    {
        Color color = flashRender.color;
        while(true)
        {
            color.a += flashOn ? Time.deltaTime : -Time.deltaTime;
            if (flashOn)
            {
                if(color.a >= 1)
                    flashOn = false;
            }
            else
            {
                if(color.a <= 0)
                    flashOn = true;
            }
            flashRender.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDestroy()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
    }
}
