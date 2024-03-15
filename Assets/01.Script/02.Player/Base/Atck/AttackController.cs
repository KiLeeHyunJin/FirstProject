using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] PooledObject bloodEffect;
    [SerializeField] int effectCount;
    AttackEffectType effectType;
    [SerializeField] int maxAttack;
    [SerializeField] TransformPos pos;
    [SerializeField] LayerMask layerMask;

    Collider2D[] colliders;

    [SerializeField] Vector2 offset;
    [SerializeField] Vector3 size;

    Vector2 knockbackPower;
    float per;
    float stunTime;
    float pushTime;
    int damage;
    int targetCount;

    bool isStart;
    bool gather;

    AudioClip audioSwingClip;
    AudioClip audioHitClip;

    public void Awake()
    {
        if(maxAttack < 1)
            maxAttack = 15;
        colliders = new Collider2D[maxAttack];
        isStart = true;
        offset = Vector3.zero;
        size = Vector3.zero;
    }
    private void Start()
    {
        if(bloodEffect != null)
            Manager.Pool.CreatePool(bloodEffect, effectCount, effectCount);
    }
    public void SetPosition(Vector2 _position, Vector3 _size, bool _gather = false)
    {
        offset = _position;
        size = _size * 0.5f;
        gather = _gather;
    }
    public void SetSoundClip(AudioClip swingClip, AudioClip hitClip)
    {
        audioSwingClip = swingClip;
        audioHitClip = hitClip;
    }
    public void SetKnockBack(Vector3 power, AttackEffectType _effectType, float _pushTime, float _stunTime)
    {
        knockbackPower = power;
        effectType = _effectType;
        pushTime = _pushTime;
        stunTime = _stunTime;
    }

    public void SetDamage(float _perDamage, int _damage)
    {
        per = _perDamage;
        damage = _damage;
    }

    public void OnAttackEnable() => GetCollisionObject();

    private void GetCollisionObject()
    {
        float checkLength = size.x > size.y ? size.x : size.y;

        targetCount = Physics2D.OverlapCircleNonAlloc(
            new Vector2(pos.X + offset.x, pos.Z + pos.Y + offset.y)
            , checkLength, colliders,layerMask);

        Debug.Log($"count : {targetCount}");
        if(targetCount > 0)
            StartCoroutine(AttackCo());
        else
        {
            if (audioSwingClip != null)
                Manager.Sound.PlaySFX(audioSwingClip);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmoSize = size;
        float checkLength = gizmoSize.x > gizmoSize.y ? gizmoSize.x : gizmoSize.y;
        float temp = isStart ? 1: 0.5f;
        //범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector2(pos.X, pos.Z) + new Vector2(offset.x , 0),
            new Vector2(size.x, size.z) * temp * 2);

        //높이
        //Gizmos.color = Color.yellow;
        float realYPos =
            (pos.Z  + pos.Y) + (gizmoSize.y * temp * 0.5f);
        //Gizmos.DrawWireSphere(new Vector2(pos.X , realYPos) + offset, checkLength * temp);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(pos.X , realYPos) + offset, 
            new Vector2(size.x * temp * 2, size.y * temp));

    }
    IEnumerator AttackCo()
    {
        int value = (int)(damage * per);
        Vector3 returnKnockback = knockbackPower;
        Vector3 returnPos = pos.Pose;
        Vector3 returnSize = size;
        Vector2 returnOffset = offset;
        int hitTaget = 0;
        int dir = pos.direction == TransformPos.Direction.Left ? -1 : 1;
        for (int i = 0; i < targetCount; i++)
        {
            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            if (damagable != null)
            {
                if (damagable.IGetDamage(value, effectType))
                {
                    if(damagable.ICollision(returnSize, returnPos, returnOffset))
                    {
                        if (gather)
                        {
                            Vector3 currentPos = damagable.IGetPos();
                            Vector2 sour = (
                                    (new Vector2(pos.X, pos.Z) + offset) - new Vector2(currentPos.x, currentPos.z)
                                ).normalized;
                            Vector3 temp = new Vector3(sour.x, returnKnockback.y, sour.y);
                            returnKnockback = temp;
                        }

                        damagable.ISetKnockback(returnKnockback,stunTime,pushTime);
                        if (audioHitClip != null)
                            Manager.Sound.PlaySFX(audioHitClip);

                        PooledObject hitEffect = Manager.Pool.GetPool(
                            bloodEffect, 
                            new Vector3(
                                damagable.IGetPos().x + damagable.IGetOffset().x * dir, 
                                damagable.IGetPos().y + damagable.IGetOffset().y +
                                damagable.IGetPos().z,0), 
                            Quaternion.identity);
                        HitEffect effect = hitEffect as HitEffect;
                        if (effect != null)
                            effect.SetSortingLayer(damagable.IGetRenderLayerNum() + 1);
                        hitTaget++;
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(0.005f, 0.02f));
        }
        if(hitTaget == 0)
            if (audioSwingClip != null)
                Manager.Sound.PlaySFX(audioSwingClip);
    }

    private void SetttingReset()
    {
        per = 0;
        targetCount = 0;
        damage = 0;
        offset = Vector2.zero;
        size = Vector3.zero;
        knockbackPower = Vector2.zero;
    }


}
