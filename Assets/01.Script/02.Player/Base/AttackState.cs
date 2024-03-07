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
    [Serializable]
    public struct AttackCount
    {
        [Range(0, 1)]
        public float AttackTime;
        public float Percent;
        public Vector2 power;
    }
    [Serializable]
    public struct MoveData
    {
        public Vector2 move;
        public float moveTime;
    }
    [Serializable]
    public struct AttackSize
    {
        public Vector2 offset;
        public Vector3 size;
        
    }

    [Serializable]
    public struct AttackData
    {
        public string AnimName;
        [Range(0, 1)]
        public float delay;
        public float damage;
        public float mana;
        public AttackSize attackSize;
        public MoveData move;
        public AttackCount[] attackCounts;
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
    protected bool isTransition;
    public bool Ready
    {
        get
        {
            if (hasCoolTime == false)
                return true;
            return time <= 0;
        }
    }
    public void SettingAttack()
    {
        animId = new int[attackData.Length];
        for (int i = 0; i < attackData.Length; i++)
        {
            animId[i] = Animator.StringToHash(attackData[i].AnimName);
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

    protected abstract void AttackEffect();

    int count;
    public override void Update()
    {
        if (inputCount >= attackData.Length)
            return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackData[inputCount].AnimName))
        {
            if (count > attackData[inputCount].attackCounts.Length - 1)
                return;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackData[inputCount].attackCounts[count].AttackTime)
            {
                owner.currentSkill = this;
                int direction =
                    pos.direction == TransformPos.Direction.Left ? -1 : 1;

                attack.SetPosition(
                    new Vector2(
                        attackData[inputCount].attackSize.offset.x * direction,
                        attackData[inputCount].attackSize.offset.y),
                    attackData[inputCount].attackSize.size);

                attack.SetKnockBack(
                    new Vector2(
                        attackData[inputCount].attackCounts[count].power.x * direction,
                        attackData[inputCount].attackCounts[count].power.y));

                attack.SetDamage(attackData[inputCount].attackCounts[count].Percent, attackData[inputCount].damage);
                attack.OnAttackEnable();
                count++;
            }
        }
    }

    protected void Attack()
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
            count = 0;
            anim.Play(animId[inputCount]);
            if (animCo != null)
                owner.StopCoroutine(animCo);
            animCo = owner.StartCoroutine(AnimationInputSensor(inputCount));
            
        }
        else
        {
            inputCount = 0;
            count = 0;
            anim.Play(animId[0]);
            animCo = owner.StartCoroutine(AnimationInputSensor(0));
        }
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
        isTransition = true;
    }
    public override void Exit()
    {
        inputCount = 0;
        count = 0;
        if (animCo != null)
            owner.StopCoroutine(animCo);
    }

    public void moveMethod()
    {
        Vector2 movePos = attackData[inputCount].move.move;
        if (pos.direction == TransformPos.Direction.Left)
            movePos.x *= -1;
        pos.AddForce(movePos, attackData[inputCount].move.moveTime);
    }
}
