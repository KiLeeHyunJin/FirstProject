using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    [SerializeField] Image frame;
    [SerializeField] Image highlight;
    [SerializeField] Image tile;
    [SerializeField] Sprite[] tiles;
    Animator anim;
    Vector3 idlePos;
    Vector3 walkPos;
    Vector3 alertPos;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        idlePos = transform.position;
        alertPos = idlePos + new Vector3(0.2f, 0, 0);
        walkPos = idlePos - new Vector3(0.45f, 0, 0);
    }
    public void OnClick()
    {
        if (anim == null)
            return;
        anim.Play("Walk");
        transform.position = walkPos;
        OnClickImg();
    }

    public void OnEnter()
    {
        if (anim == null)
            return;
        anim.Play("Alert");
        transform.position = alertPos;
        OnEnterImg();
    }
    public void OnExit()
    {
        if (anim == null)
            return;
        anim.Play("Idle");
        transform.position = idlePos;
        OnExitImg();
    }
    void OnExitImg()
    {
        frame.enabled = false;
        highlight.enabled = false;
        tile.sprite = tiles[1];
    }
    void OnEnterImg()
    {
        highlight.enabled=true;
    }
    void OnClickImg()
    {
        frame.enabled = true;
        tile.sprite = tiles[0];
    }
}
