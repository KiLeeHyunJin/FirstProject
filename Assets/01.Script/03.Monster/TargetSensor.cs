using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSensor : MonoBehaviour
{ 
    int targetLayer;
    [field: SerializeField]
    public Transform target { get; private set; } 
    protected void Awake()
    {
        targetLayer = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == targetLayer)
            target = collision.transform;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == targetLayer)
        {
            if (target == collision.transform)
            {
                target = null;
            }
        }

    }
}
