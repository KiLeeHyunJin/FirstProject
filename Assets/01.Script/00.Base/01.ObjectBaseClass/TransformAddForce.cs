using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAddForce
{
    Rigidbody2D xRigid;
    Rigidbody2D yRigid;
    Transform yTransform;
    TransformPos owner;
    public TransformAddForce(Rigidbody2D _xRigid, Rigidbody2D _yRigid,TransformPos _owner)
    {
        xRigid = _xRigid;
        yRigid = _yRigid;
        owner = _owner;
        yTransform = _yRigid.transform;
    }

    public void AddForce(Vector2 power, float time) => AddForceMethod(power, time);
    public void AddForceImpuse(Vector2 power, float time) => AddForceImpuseMethod(power, time);
    Coroutine coroutine = null;

    private void AddForceMethod(Vector2 powerVector, float time)
    {
        if (coroutine != null)
            owner.OwnerStopCo(coroutine);
            coroutine = owner.OwnerCo(ForceCo(powerVector, time));
    }
    private void AddForceImpuseMethod(Vector2 powerVector, float time)
    {
        if (impulseCo != null)
            owner.OwnerStopCo(impulseCo);
        impulseCo = owner.OwnerCo(ImpulseForceCo(powerVector, time));
    }
    Coroutine impulseCo = null;
    IEnumerator ImpulseForceCo(Vector2 power, float time)
    {
        LimitYUnlock();
        yRigid.AddForce(new Vector2(0, power.y), ForceMode2D.Impulse);
        xRigid.AddForce(new Vector2(power.x,0), ForceMode2D.Impulse);
        yield return WaitTime(time);
        YAddForceReset();
        XAddForceReset();
    }

    IEnumerator ForceCo(Vector2 powerVector,float time)
    {
        LimitYUnlock();
        yRigid.velocity = new Vector2(0, powerVector.y);
        if (xRigid != null)
            xRigid.velocity = new Vector2(powerVector.x, 0);
        yield return WaitTime(time);
        YAddForceReset();
        XAddForceReset();
    }
    IEnumerator WaitTime(float time)
    {
        float timer = 0;

        while (yRigid.velocity.y > 0)
        {
            yTransform.localPosition = new Vector3(0, yTransform.localPosition.y, 0);
            yield return new WaitForFixedUpdate();
        }
        while (owner.Y > 0)
        {
            yTransform.localPosition = new Vector3(0, yTransform.localPosition.y, 0);
            yield return new WaitForFixedUpdate();
        }

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
