using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransformAddForce
{
    public enum YState
    {
        Up,Down,None
    }
    Rigidbody2D xRigid;
    Rigidbody2D yRigid;
    Transform yTransform;
    TransformPos owner;
    [field:SerializeField]public YState Ystate { get; private set; }
    public TransformAddForce(Rigidbody2D _xRigid, Rigidbody2D _yRigid,TransformPos _owner)
    {
        xRigid = _xRigid;
        yRigid = _yRigid;
        owner = _owner;
        yTransform = _yRigid.transform;
        Ystate = YState.None;
    }
    public Vector3 GetVelocity
    {
        get
        {
            return new Vector3(xRigid.velocity.x, yRigid.velocity.y, xRigid.velocity.y);
        }
    }
    public Vector2 GetVelocity2
    {
        get
        {
            return xRigid.velocity;
        }
    }
    public void ForceZero(KeyCode pos) => ForceReset(pos);

    private void ForceReset(KeyCode pos)
    {
        if(pos == KeyCode.X)
            xRigid.velocity = Vector2.zero;
        else if(pos == KeyCode.Y)
            yRigid.velocity = Vector2.zero;
        else
        {
            xRigid.velocity = Vector2.zero;
            yRigid.velocity = Vector2.zero;
        }
            
    }
    public void AddForce(Vector3 power, float time) => AddForceMethod(power, time);
    Coroutine coroutine = null;

    private void AddForceMethod(Vector3 powerVector, float time)
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
            coroutine = owner.StartCoroutine(ForceCo(powerVector, time));
    }
    public void AddForceImpuse(Vector3 power, float time) => AddForceImpuseMethod(power, time);
    public void AddForceWalk(Vector2 power) => MoveForce(power);
    private void AddForceImpuseMethod(Vector3 powerVector, float time)
    {
        if (impulseCo != null)
            owner.StopCoroutine(impulseCo);
        impulseCo = owner.StartCoroutine(ImpulseForceCo(powerVector, time));
    }
    Coroutine impulseCo = null;
    IEnumerator ImpulseForceCo(Vector3 power, float time)
    {
        if(power.y != 0)
        {
            LimitYUnlock();
            yRigid.AddForce(new Vector2(0, power.y), ForceMode2D.Impulse);
            Ystate  = power.y > 0 ? YState.Up : YState.Down;
        }
        if (xRigid != null)
            xRigid.AddForce(new Vector2(power.x,power.z), ForceMode2D.Impulse);
        yield return WaitTime(time);
        XAddForceReset();
    }
    void MoveForce(Vector2 power)
    {
        if (xRigid != null)
        {
            Vector2 check = new Vector2(power.x, power.y);
            if (check.x == 0)
                check.x = xRigid.velocity.x;
            if (check.y == 0)
                check.y = xRigid.velocity.y;
            xRigid.velocity = check;
        }
    }
    IEnumerator ForceCo(Vector3 power, float time)
    {
        if (power.y != 0)
        {
            LimitYUnlock();
            yRigid.velocity = new Vector2(0, power.y);
            Ystate = power.y > 0 ? YState.Up : YState.Down;
        }
        MoveForce(new Vector2(power.x,power.z));
        yield return WaitTime(time);
        XAddForceReset();
    }
    IEnumerator WaitTime(float time)
    {
        float timer = 0;

        while (yRigid.velocity.y > 0)
        {
            if(Ystate != YState.None)
                Ystate = YState.Up;
            yTransform.localPosition = new Vector3(0, yTransform.localPosition.y, 0);
            yield return new WaitForFixedUpdate();
        }
        while (owner.Y > 0)
        {
            if (Ystate != YState.None)
                Ystate = YState.Down;
            //Debug.Log($"Y value : {owner.Y}");
            yTransform.localPosition = new Vector3(0, yTransform.localPosition.y, 0);
            yield return new WaitForFixedUpdate();
        }
        YAddForceReset();
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    void LimitYUnlock()
    {
        if (yRigid == null)
            return;
        yRigid.constraints =
            RigidbodyConstraints2D.FreezePositionX |
            RigidbodyConstraints2D.FreezeRotation;
    }

    void YAddForceReset()
    {
        if (yRigid == null)
            return;
        Ystate = YState.None;
        yRigid.velocity = Vector2.zero;
        yRigid.constraints =
            RigidbodyConstraints2D.FreezePositionX |
            RigidbodyConstraints2D.FreezePositionY |
            RigidbodyConstraints2D.FreezeRotation;
        owner.Y = 0;
    }
    void XAddForceReset()
    {
        if (xRigid == null)
            return;
        xRigid.velocity = Vector2.zero;
    }
}
