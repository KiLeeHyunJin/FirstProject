using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
}

public abstract class AttackSkill : MonoBehaviour
{
    public enum AttackType
    {
        JumpAction,
        Action,
    }

    /*[SerializeField] */protected AttackController atkData;
    /*[SerializeField] */protected TransformPos pos;
    /*[SerializeField] */protected PlayerController controller;
    [SerializeField] protected float coolTime;
    [SerializeField] protected AttackType type;
    [SerializeField] bool hasCoolTime;
    [SerializeField] float[] mana;
    [SerializeField] protected AttackData[] attackData;
    protected float time;
    protected int[] animId;
    protected Animator anim;
    protected int inputCount;
    public bool Ready
    {
        get {
            if (hasCoolTime == false)
                return true;
            return time <= 0;
        }
    }

    private void Awake()
    {
        AnimationStringToIdChange();
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        atkData = GetComponentInChildren<AttackController>();
        pos = GetComponent<TransformPos>();
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        CoolTime();
    }

    void AnimationStringToIdChange()
    {
        animId = new int[attackData.Length];
        int j = 0;
        for (int i = 0; i < attackData.Length; i++)
        {
            if (attackData[i].AnimName == null)
                continue;
            animId[j++] = Animator.StringToHash(attackData[i].AnimName);
        }
    }
    void CoolTime()
    {
        if (hasCoolTime == false)
            return;
        if (time > 0)
            time -= Time.deltaTime;
    }
    protected void ResetCoolTime()
    {
        time = coolTime;
    }

    public abstract void InputKeyCount();

    protected void Attack(int count)
    {
        controller.currentSkill = this;
        int direction = 1;
        if (pos.direction == TransformPos.Direction.Left)
            direction = -1;
        atkData.SetPosition(
            new Vector2(attackData[count].offset.x * direction, attackData[count].offset.y), 
            attackData[count].size);
        atkData.SetKnockBack(new Vector2(attackData[count].power.x * direction, attackData[count].power.y));
        atkData.SetDamage(100);
    }

    public void moveMethod()
    {
        Vector2 movePos = attackData[inputCount].move;
        if (pos.direction == TransformPos.Direction.Left)
            movePos.x *= -1;
        pos.AddForce(movePos, attackData[inputCount].moveTime);
    }
    //void FirstAttack()
    //{
    //    atkData.SetData(new Vector2(4f, 5), 100);
    //}

    //void SecondAttack()
    //{
    //    atkData.SetData(new Vector2(10f, 9f), 100);

    //}
    //void ThirdAttack()
    //{
    //    atkData.SetData(new Vector2(15f, 15), 100);
    //}









    /*사용하려다가 포기
    //public abstract IEnumerator UseSkill();
    /// <summary>
    /// 대기 선 딜레이
    /// </summary>
    /// <param name="p_delay"></param>
    /// <returns></returns>
    protected IEnumerator Delay(float p_delay) 
    {
        yield return new WaitForSeconds(p_delay);
    }
    /// <summary>
    /// 스킬 발동(효과)
    /// </summary>
    /// <param name="p_isLeft"></param>
    /// <param name="attackPower"></param>
    protected virtual void ActivateSkill( bool p_isLeft,float attackPower = 1f) 
    {

    }
    /// <summary>
    /// 렌더(애니메이션 재생) 후 딜레이
    /// </summary>
    /// <param name="p_anim"></param>
    /// <param name="p_duration"></param>
    /// <param name="p_delay"></param>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected IEnumerator AnimPlay(float p_duration, float p_delay, int idx  = -1)
    {
        yield return new WaitForSeconds(p_duration - p_delay);
        if (idx < 0)
            yield break;
        anim.Play(animId[idx]);
    }*/

}
