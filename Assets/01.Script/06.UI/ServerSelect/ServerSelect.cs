using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerSelect : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] int selectCount;
    ServerSlot[] servers;
    private void Start()
    {
        servers = FindObjectsOfType<ServerSlot>();
        selectCount = -1;
        img.sprite = null;
        img.enabled = false;
        foreach (var server in servers)
            server.Cancle(selectCount);
    }
    public void SelectServer(int idx, Sprite bg)
    {
        if(selectCount == idx)
        {
            selectCount = -1;
            img.sprite = null;
            img.enabled = false;
        }
        else
        {
            selectCount = idx;
            img.sprite = bg;
            if(img.enabled == false)
                img.enabled = true;
            img.rectTransform.localScale = new Vector3(3, 3, 3);
            img.SetNativeSize();
        }
        foreach (var server in servers)
            server.Cancle(selectCount);
    }
}
