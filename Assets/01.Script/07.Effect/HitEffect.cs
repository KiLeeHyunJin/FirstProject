using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : PooledObject
{
    Animator anim;
    new SpriteRenderer renderer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        AnimationClip[] animatorClips = anim.runtimeAnimatorController.animationClips;
        if (animatorClips == null)
            gameObject.SetActive(false);
        if (animatorClips.Length == 0)
            gameObject.SetActive(false);
        int idx = UnityEngine.Random.Range(0, animatorClips.Length);
        anim.Play(animatorClips[idx].name);
    }
    public void SetSortingLayer(int layerNum) => renderer.sortingOrder = layerNum;

    public void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.1f)
            Release();
    }
}
