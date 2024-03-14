using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLimit : MonoBehaviour
{
    int count;
    SceneChangeTrigger[] sceneChanges;
    void Start()
    {
        for (int i = 0; i < sceneChanges.Length; i++)
        {
            if (sceneChanges[i] == null)
                continue;
            sceneChanges[i].gameObject.SetActive(false);
        }
        count = GameObject.FindGameObjectsWithTag("Monster").Length;
    }
    public void MinusCount()
    {
        count--;
        if(count <= 0)
        {
            for (int i = 0; i < sceneChanges.Length; i++)
            {
                if (sceneChanges[i] == null)
                    continue;
                sceneChanges[i].gameObject.SetActive(false);
            }
        }
    }
}
