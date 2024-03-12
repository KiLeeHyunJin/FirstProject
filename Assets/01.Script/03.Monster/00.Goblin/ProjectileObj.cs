using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AttackObj : MonoBehaviour
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
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        isStart = true;
    }
    public void SetData(Vector2 _power, Vector3 _pos,int _dir)
    {
        direc = new Vector2(_dir * speed, 0);
        power = _power;
        pos = _pos;
        size = Vector3.one * circleCollider.radius;
        offset = new Vector2(0, 0.5f);
        transform.position = new Vector2(_pos.x, _pos.z) + offset;
    }
    bool isStart = false;
    private void OnDrawGizmos()
    {
        float temp = isStart ? 1 : 0.5f;
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(
        new Vector2(transform.position.x, pos.z),
            new Vector2(size.x, size.y) * temp * 2);

        float realYPos =
        (pos.z + offset.y) + (size.x * temp * 0.5f);

        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, realYPos),
        new Vector2(size.x * temp * 2, size.y * temp));
    }
    private void Update()
    {
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
