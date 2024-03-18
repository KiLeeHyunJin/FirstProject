using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSortingLayer : MonoBehaviour
{
    new SpriteRenderer renderer;
    [SerializeField] int addSortingLayer;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if(renderer != null)
        {
            renderer.sortingOrder = ((int)(transform.position.y * -10));
            renderer.sortingOrder = renderer.sortingOrder + addSortingLayer;
        }
    }

}
