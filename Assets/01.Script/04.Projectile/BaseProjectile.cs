using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] int layer;
    [SerializeField] float speed;
    [SerializeField] DamageUI damageUI;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform hitbox;
    List<Attackable> target;
    AudioClip audioClip;
    Vector3 size;
    Vector2 power;
    Vector2 offset;
    Vector2 direc;
    Vector3 pos;

    int damage;
    float stunTime;
    float pushTime;
    float time;
    AttackEffectType attackType;

    bool isRepeater = false;
    float destroyTime;

    CircleCollider2D circleCollider;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        target = new List<Attackable>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
         if(canvas == null)
            canvas = FindObjectOfType<Canvas>();
    }
    public void SetDirection(int _dir)
    {
        target.Clear();
        if (_dir > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
        direc = new Vector2(_dir * speed, 0);
    }
    public void SetAudioClip(AudioClip clip)
    {
        audioClip = clip;
    }

    public void SetPosition(Vector3 _pos, Vector3 Size, Vector2 _offset, Vector2 _posOffset)
    {
        if(Size.sqrMagnitude == 0)
            size = Vector3.one * circleCollider.radius;
        else
            size = Size * 0.5f;
        this.offset = _offset;
        this.pos = _pos;
        transform.position = new Vector2(_pos.x, _pos.z) + _posOffset;
    }
    public void SetAttackData(Vector2 _power,int _damage, AttackEffectType _attackType, float _stunTime, float _pushTime)
    {
        damage = _damage;
        power = _power;
        attackType = _attackType;
        stunTime = _stunTime;
        pushTime = _pushTime;
    }
    public void SetState(bool repeatState, float timing,float _destroyTime = 0)
    {
        if (repeatState == false && _destroyTime == 0)
            _destroyTime = 10;
        destroyTime = _destroyTime;
        isRepeater = repeatState;
        time = timing;
        gameObject.SetActive(true);
        if (disableCoroutine != null)
            StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCo());
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(RepeatAtckCo());
    }

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

    Coroutine attackCoroutine = null;
    IEnumerator RepeatAtckCo()
    {
        while (true)
        {
            if (target != null)
            {
                foreach (var element in target)
                {
                    Vector3 currentPos = new Vector3(transform.position.x, 0, pos.z);
                    if (element.ICollision(size, currentPos, offset))
                    {
                        if(element.IGetDamage(damage, attackType))
                        {
                            element.ISetKnockback(power, pushTime);
                            PooledObject pooledObject = Manager.Pool.GetPool(damageUI, currentPos, Quaternion.identity);
                            DamageUI text = pooledObject as DamageUI;
                            if (text != null)
                            {
                                text.SetTarget(element.gameObject.transform, damage);
                                text.transform.SetParent(canvas.transform);
                            }
                            if (isRepeater == false)
                            {
                                OffState();
                                if (disableCoroutine != null)
                                    StopCoroutine(disableCoroutine);
                                yield break;
                            }
                        }
                        
                    }
                }
            }
            yield return new WaitForSeconds(time);
        }
    }
    Coroutine disableCoroutine = null;
    IEnumerator DisableCo()
    {
        yield return new WaitForSeconds(destroyTime);
        OffState();
    }
    void OffState()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        rigid.velocity = direc;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layer)
        {
            Attackable attackable = collision.GetComponent<Attackable>();
            if (attackable)
                target.Add(attackable);
        }
    }
}
