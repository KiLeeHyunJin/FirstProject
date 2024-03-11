using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContainer : MonoBehaviour
{
    PlayerController controller;

    Dictionary<string, AttackState> skillTree = new Dictionary<string, AttackState>();
    [SerializeField] ShockWave shockWave;
    [SerializeField] ShortAirSlash airSlash;
    public void Awake()
    {
        controller = GetComponent<PlayerController>();
        skillTree.Add("shockDown", shockWave);
        skillTree.Add("airSlash", airSlash);
    }
    public AttackState GetSkill(string name)
    {
        skillTree.TryGetValue(name, out var state);
        return state;
    }
    public void AddSkill(string name, KeyManager.Key key)
    {
        AttackState atckState = GetSkill(name);
        if (atckState == null)
            return;
        controller.SetKeyDic(atckState, key);
        
    }
}
