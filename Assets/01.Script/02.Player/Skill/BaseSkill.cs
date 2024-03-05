using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    [SerializeField] AttackPos atkData;
    [SerializeField] TransformPos pos;
    [Header("�ִϸ��̼� �̸�")]
    [SerializeField] string[] AnimName;
    [Header("�Է� ���� �ð�")]
    [Range(0, 1)]
    [SerializeField] float[] delay;
    [Header("��ų �ʱ�ȭ ��Ÿ��")]
    [SerializeField] float coolTime;
    [Header("��ų ��Ÿ�� ����")]
    [SerializeField] bool hasCoolTime;
    [SerializeField] float[] mana;
    float time;
    int[] animId;
    Animator anim;
    int inputCount;
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
    }
    private void Update()
    {
        CoolTime();
    }

    void AnimationStringToIdChange()
    {
        animId = new int[AnimName.Length];
        int j = 0;
        for (int i = 0; i < AnimName.Length; i++)
        {
            if (AnimName[i] == null)
                continue;
            animId[j++] = Animator.StringToHash(AnimName[i]);
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

    public void InputKeyCount()
    {
        if (animId.Length > 1)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName[inputCount]))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= delay[inputCount])
                {
                    inputCount++;
                    if (animId.Length <= inputCount)
                    {
                        inputCount = 0;
                    }
                }
                else
                    return;
            }
            else
            {
                inputCount = 0;
            }
            anim.Play(animId[inputCount]);
            if (animCo != null)
                StopCoroutine(animCo);
            animCo = StartCoroutine(AnimationInputSensor(inputCount));
            StartCoroutine(moveCo());
            Attack(inputCount);
        }
        else
        {
            anim.Play(animId[0]);
            animCo = StartCoroutine(AnimationInputSensor(0));
        }
    }
    Coroutine animCo = null;
    IEnumerator AnimationInputSensor(int input)
    {
        int idx = input;
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName[idx]))
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    break;
            yield return new WaitForFixedUpdate();
        }
        anim.Play("Idle");
    }
    void Attack(int count)
    {
        Vector2 pushPower = Vector2.zero;

        switch (count)
        {
            case 0:
                pushPower = new Vector2(1.5f, 3);
                break;
            case 1:
                pushPower = new Vector2(2.2f, 4);
                break;
            case 2:
                pushPower = new Vector2(2, 6);
                break;
        }
        if (pos.direction == TransformPos.Direction.Left)
            pushPower.x *= -1;
        atkData.SetData(pushPower, 100);
    }
    IEnumerator moveCo()
    {
        yield return new WaitForSeconds(0.15f);
        Vector2 movePos = new Vector2(4, 0);
        if (pos.direction == TransformPos.Direction.Left)
            movePos.x *= -1;
        pos.AddForce(movePos, 0.18f);
        if (pos.direction == TransformPos.Direction.Left)
            movePos.x *= -1;
        yield return new WaitForSeconds(0.15f);
        atkData.OnEnableCollider();
    }
    void FirstAttack()
    {
        atkData.SetData(new Vector2(4f, 5), 100);
    }

    void SecondAttack()
    {
        atkData.SetData(new Vector2(10f, 9f), 100);

    }
    void ThirdAttack()
    {
        atkData.SetData(new Vector2(15f, 15), 100);
    }









    /*����Ϸ��ٰ� ����
    //public abstract IEnumerator UseSkill();
    /// <summary>
    /// ��� �� ������
    /// </summary>
    /// <param name="p_delay"></param>
    /// <returns></returns>
    protected IEnumerator Delay(float p_delay) 
    {
        yield return new WaitForSeconds(p_delay);
    }
    /// <summary>
    /// ��ų �ߵ�(ȿ��)
    /// </summary>
    /// <param name="p_isLeft"></param>
    /// <param name="attackPower"></param>
    protected virtual void ActivateSkill( bool p_isLeft,float attackPower = 1f) 
    {

    }
    /// <summary>
    /// ����(�ִϸ��̼� ���) �� ������
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
