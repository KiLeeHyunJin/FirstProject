using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LandBasicAttackController : SkillStateController
{
    protected override void EnterState()
    {
        owner.SetState = PlayerState.LandAtck0;
    }

}
