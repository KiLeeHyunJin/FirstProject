using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SkillContain
{
    public SkillStateController controller;
    public List<SkillState> attackStates;

    public SkillContain(SkillStateController controller, List<SkillState> attackStates)
    {
        this.controller = controller;
        this.attackStates = attackStates;
    }
}
public class AttackContainer : MonoBehaviour
{

    PlayerController controller;
    Dictionary<string,SkillStateController> skillController = new Dictionary<string,SkillStateController>();
    Dictionary<string,List<SkillState>> skillStates = new Dictionary<string,List<SkillState>>();

    [SerializeField] LandBasicAttackController landBasicController;
    [SerializeField] ShortAirSlashController slashController;
    [SerializeField] RunBasicAttackController runBasicAttackController;
    [SerializeField] JumpBasicAttackController jumpBasicAttackController;
    [SerializeField] ShockWaveAttackController shockWaveAttackController;
    [SerializeField] HeatWaveAttackController heatWaveAttackController;
    [SerializeField] LunaSlashController lunaSlashController;
    public void Awake()
    {
        controller = GetComponent<PlayerController>();
        AddData("ShortAir", slashController, 
            new SkillState[]
            { new ShortAirSlash0(), new ShortAirSlash1(), new ShortAirSlash2() });
        AddData("LandBasic", landBasicController,
            new SkillState[]
            { new LandBasicAttack0(), new LandBasicAttack1(), new LandBasicAttack2() });
        AddData("JumpBasic", jumpBasicAttackController,
            new SkillState[]
            { new JumpBasicAttack()});
        AddData("RunBasic", runBasicAttackController,
            new SkillState[]
            { new RunBasicAttack()});        
        AddData("ShockWave", shockWaveAttackController,
            new SkillState[]
            { new ShockWaveAttack()});
        AddData("HeatWave", heatWaveAttackController,
            new SkillState[]
            { new HeatWaveAttack0(),new HeatWaveAttack1()});
        AddData("LunaSlash", lunaSlashController,
            new SkillState[]
            { new LunaSlashAttack0(),new LunaSlashAttack1()});
    }
    void AddData(string str, SkillStateController _controller , params SkillState[] _skillStates)
    {
        skillStates.Add(str, new List<SkillState>(_skillStates));
        skillController.Add(str, _controller);
    }

    public SkillContain GetSkillData(string str)
    {
        skillController.TryGetValue(str, out SkillStateController skillStateController);
        skillStates.TryGetValue(str, out List<SkillState> skillLists);

        if (skillStateController == null || skillLists == null)
            return null;

        for (int i = 0; i < skillLists.Count; i++)
            skillStateController.SetSkillData(skillLists[i],i);

        return new SkillContain(skillStateController, skillLists);
    }

    //public AttackState GetSkill(string name)
    //{
    //    skillTree.TryGetValue(name, out var state);
    //    return state;
    //}
    //public void AddSkill(string name, KeyManager.Key key)
    //{
    //    AttackState atckState = GetSkill(name);
    //    if (atckState == null)
    //        return;
    //    controller.SetKeyDic(atckState, key);
    //}
}
