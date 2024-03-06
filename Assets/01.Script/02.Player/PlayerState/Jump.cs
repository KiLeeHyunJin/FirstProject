using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SerializeField]
public class Jump : BaseState<PlayerController.State>
{
    [SerializeField] float jumpPower = 5;
    [SerializeField] bool isJump;
    public Jump(Animator _anim, TransformPos _transform, AttackController _atckCon, SpriteRenderer _renderer) : base(_anim, _transform, _atckCon, _renderer)
    {

    }

    public override void StartedInputAction(InputAction.CallbackContext context) 
    { 

    }
    public override void CanceledInputAction(InputAction.CallbackContext context) 
    { 

    }
    public override void PerformedInputAction(InputAction.CallbackContext context) 
    { 

    }
    public override void Enter() 
    { 

    }
    public override void Exit() 
    { 

    }
    public override void Update() 
    { 

    }

}
