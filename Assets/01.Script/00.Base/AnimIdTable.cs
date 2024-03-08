using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimIdTable
{
    static AnimIdTable instance;
    public static AnimIdTable GetInstance
    {
        get
        {
            if (instance == null)
                instance = new AnimIdTable();
            return instance;
        }
    }
    public readonly int  IdleId;
    public readonly int  AlertId;
    public readonly int  WalkId;
    public readonly int  RunId;
    public readonly int  JumpUpId;
    public readonly int  JumpDownId;
    public readonly int  JumpLandId;
    public readonly int  JumpAtckId;
    public readonly int  LowerSlashId;
    public readonly int  LunaSlashId;
    public readonly int  BigSlashId;
    public readonly int  DownId;
    public readonly int  FallingId;
    public readonly int  HeatWaveId;
    public readonly int  Hit1Id;
    public readonly int  Hit2Id;
    public readonly int  NomalAtck0Id;
    public readonly int  NomalAtck1Id;
    public readonly int  NomalAtck2Id;
    public readonly int  PowerSlashId;
    public readonly int  ReversSlashId;
    public readonly int  ShieldId;
    public readonly int  Shield_SuccesId;
    public readonly int  SitId;
    public readonly int  SlashId;
    public readonly int  StabId;
    public readonly int  SummonId;
    public readonly int  UperSlashId;
    public readonly int  WakeUpId;
    private AnimIdTable() 
    {
        IdleId = Animator.StringToHash("Idle");
        AlertId = Animator.StringToHash("Alert");
        WalkId = Animator.StringToHash("Walk");
        RunId = Animator.StringToHash("Run");
        JumpUpId = Animator.StringToHash("Jump_Up");
        JumpDownId = Animator.StringToHash("Jump_Down");
        JumpLandId = Animator.StringToHash("Jump_Land");
        JumpAtckId = Animator.StringToHash("JumpAtck");
        LowerSlashId = Animator.StringToHash("LowerSlash");
        LunaSlashId = Animator.StringToHash("LunaSlash");
        BigSlashId = Animator.StringToHash("BigSlash");
        DownId = Animator.StringToHash("Down");
        FallingId = Animator.StringToHash("Falling");
        HeatWaveId = Animator.StringToHash("HeatWave");
        Hit1Id = Animator.StringToHash("Hit1");
        Hit2Id = Animator.StringToHash("Hit2");
        NomalAtck0Id = Animator.StringToHash("NomalAtck_0");
        NomalAtck1Id = Animator.StringToHash("NomalAtck_1");
        NomalAtck2Id = Animator.StringToHash("NomalAtck_2");
        PowerSlashId = Animator.StringToHash("PowerSlash");
        ReversSlashId = Animator.StringToHash("ReversSlash");
        ShieldId = Animator.StringToHash("Shield");
        Shield_SuccesId = Animator.StringToHash("Shield_Succes");
        SitId = Animator.StringToHash("Sit");
        SlashId = Animator.StringToHash("Slash");
        StabId = Animator.StringToHash("Stab");
        SummonId = Animator.StringToHash("Summon");
        UperSlashId = Animator.StringToHash("UperSlash");
        WakeUpId = Animator.StringToHash("WakeUp");
    }

}
