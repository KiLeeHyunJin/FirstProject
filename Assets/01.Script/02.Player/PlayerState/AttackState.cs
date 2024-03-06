using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[Serializable]
public abstract class AttackState : BaseState<PlayerController.State>
{
    public struct AttackData
    {
        public string AnimName;
        [Range(0, 1)]
        public float delay;
        public Vector2 offset;
        public Vector3 size;
        public Vector2 power;
        public Vector2 move;
        public float moveTime;
        public float mana;
    }
    public enum AttackPlaceType
    {   JumpAction, Action, }
    public enum AttackActiveType
    {   Sky,Run,Land,   }
    [SerializeField] AttackPlaceType placeType;
    [SerializeField] protected float coolTime;
    [SerializeField] protected AttackActiveType activeType;
    [SerializeField] bool hasCoolTime;
    [SerializeField] float[] mana;
    [SerializeField] protected AttackData[] attackData;
    protected float time;
    protected int[] animId;
    protected int inputCount;
    public bool Ready
    {
        get
        {
            if (hasCoolTime == false)
                return true;
            return time <= 0;
        }
    }
    protected void ResetCoolTime()
    {
        time = coolTime;
        if(coolTimeCo != null)
            owner.StopCoroutine(coolTimeCo);
        coolTimeCo = owner.StartCoroutine(CoolTimeCountDownCo());
    }
    Coroutine coolTimeCo = null;
    IEnumerator CoolTimeCountDownCo()
    {
        while(time < 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }

    public override void StartedInputAction(InputAction.CallbackContext context) 
    {
        EnterCheck();
        if (isEnter == false)
            return;
        Attack();
    }
    public override void PerformedInputAction(InputAction.CallbackContext context)
    {}

    protected abstract void AttackEffect();


    void Attack()
    {
        if (animId.Length > 1)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackData[inputCount].AnimName)) //현재 재생중인 클립 이름 확인
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackData[inputCount].delay) // 입력 제한 시간 확인
                {
                    inputCount++;
                    if (animId.Length <= inputCount) //배열을 벗어난지 확인
                    {
                        inputCount = 0;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                inputCount = 0;
            }
            anim.Play(animId[inputCount]);
            if (animCo != null)
                owner.StopCoroutine(animCo);
            animCo = owner.StartCoroutine(AnimationInputSensor(inputCount));
            AttackDataSetting(inputCount);
        }
        else
        {
            anim.Play(animId[0]);
            animCo = owner.StartCoroutine(AnimationInputSensor(0));
        }
    }

    protected void AttackDataSetting(int count)
    {
        owner.currentSkill = this;
        int direction = 
            pos.direction == TransformPos.Direction.Left ? -1 : 1;

        attack.SetPosition(
            new Vector2(
                attackData[count].offset.x * direction, 
                attackData[count].offset.y),
            attackData[count].size);

        attack.SetKnockBack(
            new Vector2(
                attackData[count].power.x * direction, 
                attackData[count].power.y));

        attack.SetDamage(100);
    }

    protected Coroutine animCo = null;
    protected IEnumerator AnimationInputSensor(int input)
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackData[inputCount].AnimName))
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    break;
            yield return new WaitForFixedUpdate();
        }
        owner.SetState = PlayerController.State.Idle;
    }

    public void moveMethod()
    {
        Vector2 movePos = attackData[inputCount].move;
        if (pos.direction == TransformPos.Direction.Left)
            movePos.x *= -1;
        pos.AddForce(movePos, attackData[inputCount].moveTime);
    }
}
