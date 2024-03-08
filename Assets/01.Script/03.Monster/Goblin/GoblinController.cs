using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController<T> : BaseController<T> where T : Enum
{
    [SerializeField] LayerMask layerMask;
    public Transform target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerMask)
            target = collision.transform;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerMask)
            target = null;
    }
}
