using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] KeyCode[] keySet;
    [SerializeField] KeyCode[] defaultKey;
    [SerializeField] KeyCode[] moveKey;
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        InputActionMap allKeysMap = inputActionAsset.FindActionMap("Player");
        StringBuilder sb = new StringBuilder();
        allKeysMap.Disable();

        InputAction action;
        action = allKeysMap.FindAction("Move");
        SetStateAction(action, PlayerController.State.Walk);

        action = allKeysMap.FindAction("Jump");
        SetStateAction(action, PlayerController.State.Jump);

        //foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (temp == (int)key)
        //        continue;

        //    CheckKeyName(key, ref sb);

        //    Debug.Log($"<Keyboard>/{sb}");

        //    action = allKeysMap.AddAction(name: sb.ToString(), type: InputActionType.Button, binding: $"<Keyboard>/{sb}");
        //    action.started += OnKeyPressed;

        //    temp = (int)key;
        //}

        allKeysMap.Enable();

        //allKeysMap.Disable();
        //InputAction jumpAction = allKeysMap.FindAction(KeyCode.DownArrow.ToString());
        //if (jumpAction != null)
        //    jumpAction.RemoveAction();

        //action = allKeysMap.AddAction(KeyCode.DownArrow.ToString(), type: InputActionType.PassThrough,
        //binding: $"<Keyboard>/{KeyCode.DownArrow.ToString()}");
        //action.started += OnKeyPressed;
        //allKeysMap.Enable();
    }

    void SetStateAction(InputAction input, PlayerController.State state)
    {
        Action<InputAction.CallbackContext> action = null;
        action = player.fsm.GetEnterMethod(state);
        if(action != null)
            input.started += action;

        //if(player.fsm.IsValueType(state))
        //{
            action = player.fsm.GetEventMethod(state);
            if (action != null)
                input.performed += action;
        //}

        action = player.fsm.GetExitMethod(state);
        if (action != null)
            input.canceled += action;
    }


    void CheckKeyName(KeyCode key, ref StringBuilder sb)
    {
        sb.Clear();

        if (KeyCode.Alpha0 <= key && key <= KeyCode.Alpha9)
            sb.Append(((int)key - (int)KeyCode.Alpha0).ToString());

        else if (key == KeyCode.LeftControl)
            sb.Append("leftCtrl");

        else if (key == KeyCode.RightControl)
            sb.Append("rightCtrl");

        else if (key == KeyCode.KeypadEnter)
            sb.Append("enter");

        else
            sb.Append(key);
    }
}
