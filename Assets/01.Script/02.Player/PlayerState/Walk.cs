using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Walk : BaseState<PlayerController.State>
{
    public override void Enter(InputAction.CallbackContext context) 
    {
        Vector2 moveValue = context.ReadValue<Vector2>();

        // 여기에서 필요한 동작을 수행
        Debug.Log("Move Input Value: " + moveValue);
    }
    public override void Exit() 
    { 

    }
    public override void Update() 
    {
        //Debug.Log("진입 완료");
    }
    public override void Transition() 
    { 

    }
}
