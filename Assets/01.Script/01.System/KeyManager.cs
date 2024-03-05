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
        int temp = 0;
        InputAction action = null;
        allKeysMap.Disable();
        InputAction moveAction = allKeysMap.FindAction("Move");
        Action<InputAction.CallbackContext> action1 = player.fsm.GetEnterMethod(PlayerController.State.Walk);
        if (action1 == null)
            Debug.Log("참조 실패");
        else
            moveAction.performed += action1;
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

        //action = allKeysMap.AddAction(KeyCode.DownArrow.ToString(), type: InputActionType.PassThrough, binding: $"<Keyboard>/{KeyCode.DownArrow.ToString()}");
        //action.started += OnKeyPressed;
        //allKeysMap.Enable();
    }


    private void OnKeyPressed(InputAction.CallbackContext context)
    {
        Debug.Log($"Key name : {context.control.name}");
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
