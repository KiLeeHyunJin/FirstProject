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

        // ���⿡�� �ʿ��� ������ ����
        Debug.Log("Move Input Value: " + moveValue);
    }
    public override void Exit() 
    { 

    }
    public override void Update() 
    {
        //Debug.Log("���� �Ϸ�");
    }
    public override void Transition() 
    { 

    }
}
