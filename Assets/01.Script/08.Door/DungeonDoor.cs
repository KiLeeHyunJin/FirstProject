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
    IEnumerator OpenFlash()
    {
        while(true)
        {
            if (flashRender.sprite == null)
                flashRender.sprite = Flash;
            else
                flashRender.sprite = null;
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDestroy()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
    }
}
