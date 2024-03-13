using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ProjectileObj : MonoBehaviour
{
    [SerializeField] float speed;
    Attackable target;
    int damage;
    Vector3 size;
    Vector2 power;
    Vector2 offset;
    Vector3 pos;
    CircleCollider2D circleCollider;
    Rigidbody2D rigid;
    Vector2 direc;
    [SerializeField] float pushTime;
    [SerializeField] int layer;
    [SerializeField] bool isDestroy;
    [SerializeField] AttackEffectType attackType;
    float destroyTime;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        isStart = true;
        gameObject.SetActive(false);
    }
    public void SetData(Vector2 _power, Vector3 _pos,int _dir)
    {
        gameObject.SetActive(true);
        target = null;
        if (_dir > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
        direc = new Vector2(_dir * speed, 0);
        power = _power;
        pos = _pos;
        size = Vector3.one * circleCollider.radius;
        offset = new Vector2(0, 0.5f);
        transform.position = new Vector2(_pos.x, _pos.z) + offset;
    }
    public void SetData(float _destroyTime, Vector3 _pos, Vector3 Size, Vector2 _offset,AttackEffectType _attackType)
    {
        destroyTime = _destroyTime;
        size = Size;
        this.offset = _offset;
        this.pos = _pos;
        attackType = _attackType;
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(DestroyCo());
        isRepeater = true;
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackRepeatCo());
    }
    bool isRepeater = false;
    bool isStart = false;
    private void OnDrawGizmos()
    {
        float temp = 0.5f;
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(
        new Vector2(transform.position.x, pos.z),
            new Vector2(size.x, size.z) * temp * 2);

        float realYPos =
        (pos.z + offset.y) + (size.x * temp * 0.5f);

        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, realYPos),
        new Vector2(size.x * temp * 2, size.y * temp));
    }
    private void Update()
    {
        if (isRepeater)
            return;
        if (target == null)
            return;
        Vector3 currentPos = new Vector3(transform.position.x, 0, pos.z);
        if (target.ICollision(size, currentPos, offset))
        {
            target.IGetDamage(damage, attackType);
            target.ISetKnockback(power, currentPos, size, offset, pushTime);
            if(isDestroy)
                Destroy(gameObject);
        }
    }
    Coroutine attackCoroutine = null;
    IEnumerator AttackRepeatCo()
    {
        while(true)
        {
            if(target != null)
            {
                Vector3 currentPos = new Vector3(transform.position.x, 0, pos.z);
                if (target.ICollision(size, currentPos, offset))
                {
                    target.IGetDamage(damage, attackType);
                    target.ISetKnockback(power, currentPos, size, offset, pushTime);
                    if (isDestroy)
                        Destroy(gameObject);
                }
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
    Coroutine coroutine = null;
    IEnumerator DestroyCo()
    {
        yield return new WaitForSeconds(destroyTime);
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        gameObject.SetActive (false);
    }
    private void FixedUpdate()
    {
        rigid.velocity = direc;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layer)
        {
            target = collision.GetComponent<Attackable>();
        }
    }
}
