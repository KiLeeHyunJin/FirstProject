using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TauController;

public class MonsterController<T> : BaseController<T> where T : Enum
{
    [Serializable]
    public struct AtckData
    {
        public float Atck_CoolTime;
        public Vector2 optimumRange;
        public Vector3 AttackSize;
        public Vector2 AttackOffset;
        public Vector2 AttackPower;
        [Range(0,1)]
        public float[] AttackTimming;
        public float[] pushTime;
        public AttackEffectType AttackEffect;
    }
    public enum AtckEnum
    {
        Atck1, Atck2, Atck3
    }
    public AtckEnum AtckType { get; private set; }
    [field : SerializeField]public LayerMask layerMask { get; private set; }
    public AtckData GetAtckData(int idx) {  return Atck_Data[idx]; }
    bool[] actkCoolTime;

    public Vector2 moveSpeed;
    public TargetSensor sensor;
    [SerializeField] AtckData[] Atck_Data;

    protected override void Start()
    {
        actkCoolTime = new bool[3]{ true,true,true};
         sensor = sensor == null ? GetComponentInChildren<TargetSensor>() : sensor;
    }
    protected void SetStateClass(MonsterState<T> state)
    {
        state.SetStateMachine(fsm);
        state.Setting(anim, transformPos, renderer);
        state.SetController(this);
    }
    public void ResetCoolTime(AtckEnum idx)
    {
        actkCoolTime[(int)idx] = false;
        StartCoroutine(CoolDownCo((int)idx));
    }
    IEnumerator CoolDownCo(int idx)
    {
        float checkTime = Atck_Data[idx].Atck_CoolTime;
        yield return new WaitForSeconds(checkTime);
        actkCoolTime[idx] = true;
        yield break;
    }
    public bool CheckCoolTime(AtckEnum idx)
    {
        bool check = actkCoolTime[(int)idx];
        if (check)
            AtckType = idx;
        return check;
    }
}
