using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPos : MonoBehaviour
{
    public enum Direction { Left, Right }
    public Direction direction { get; set; }


    [SerializeField] Transform xPosTarget;
    [SerializeField] Transform yPosTarget;
    [SerializeField] Transform hitBox;

    public AttackCollision attackCheck { get; private set; }
    [SerializeField]TransformAddForce AddForceClass;
    public Vector3 Size { get; private set; }
    public Vector2 Offset { get; private set; }
    public float Heigth { get; private set; }
    public void Start()
    {
        AddForceClass = new TransformAddForce(xPosTarget.GetComponent<Rigidbody2D>(),yPosTarget.GetComponent<Rigidbody2D>(), this);
        attackCheck = new AttackCollision(this);
        CapsuleCollider2D capsule = yPosTarget.GetComponent<CapsuleCollider2D>();
        Heigth = capsule != null ? capsule.size.y : -1;
        Size = hitBox != null ? new Vector3(hitBox.localScale.x / 2, Heigth, hitBox.localScale.y / 2) : Vector2.zero;
        Offset = hitBox != null ? new Vector2(hitBox.transform.localPosition.x, hitBox.transform.localPosition.y): Vector2.zero;
    }

    public Vector3 Velocity() => AddForceClass.GetVelocity();
    public void ForceZero(KeyCode pos) => AddForceClass.ForceZero(pos);
    public void AddForce(Vector3 power, float moveTime = 0) => AddForceClass.AddForce(power, moveTime);  
    public void AddForceImpuse(Vector3 power, float moveTime = 0) => AddForceClass.AddForceImpuse(power, moveTime);
    public TransformAddForce.YState yState() => AddForceClass.Ystate;
    public Vector3 Pose 
    { get 
        {
            return
                new Vector3(
                    xPosTarget.transform.position.x,
                    yPosTarget.transform.localPosition.y,
                    xPosTarget.transform.position.y); 
        }
        set
        {
            yPosTarget.transform.localPosition = new Vector3(0,value.y,0);
            xPosTarget.transform.position = new Vector3(value.x, value.z, 0);
        }
    }
    public float X
    {
        get { return xPosTarget.transform.position.x; }
        set{ xPosTarget.transform.position = new Vector3(value, xPosTarget.transform.position.y,0); }
    }
    public float Y
    {
        get { return yPosTarget.transform.localPosition.y; }
        set{ yPosTarget.transform.localPosition = new Vector3(0, value, 0); }
    }
    public float Z
    {
        get { return xPosTarget.transform.position.y; }
        set { xPosTarget.transform.position = new Vector3(xPosTarget.transform.position.x, value, 0); }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(X,Z) + Offset, new Vector2(Size.x,Size.z) * 2);
    }

}
