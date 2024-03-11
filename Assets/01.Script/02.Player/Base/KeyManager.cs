using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using static KeyManager;

public class KeyManager : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [field: SerializeField] public int Layer { get; private set; }
    public enum Key
    {
        Move, C, X,
        A, S, D, F, G, Q, W, E, R, T,
    }
    Key[] QuickKeys = new Key[] { Key.A, Key.S, Key.D, Key.F, Key.G, Key.Q, Key.W, Key.E, Key.R, Key.T };
    public Key[] QuickKey { get { return QuickKeys; } }
    private void Awake()
    {
    }
    private void Start()
    {
        //    InputActionMap allKeysMap = inputActionAsset.FindActionMap("Player");
        //    StringBuilder sb = new StringBuilder();
        //    InputAction action = null;
        //    allKeysMap.Disable();

        //for (int i = 1; i < Enum.GetValues(typeof(Key)).Length; i++)
        //{
        //    sb.Append(((Key)i).ToString());
        //    Debug.Log($"<Keyboard>/{sb}");
        //    action = allKeysMap.AddAction(name: sb.ToString(), type: InputActionType.Button, binding: $"<Keyboard>/{sb}");
        //    action.started += (InputAction.CallbackContext context) => { Layer |= 1 << i; };
        //    sb.Clear();
        //}

        //action = allKeysMap.AddAction(name: "N", type: InputActionType.Button, binding: $"<Keyboard>/n");
        //action.started += Call;

        //allKeysMap.Enable();
    }



    void OnX(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)Key.X;
    }
    void OnC(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)Key.C;
    }
    void OnA(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)Key.A;
    }    
    void OnS(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)Key.S;
    }
    public void OnMoveLayer()
    {
        Layer |= 1 << (int)Key.Move;
    }
    public void OffMoveLayer()
    {
        OffLayer(Key.Move);
    }

    public void Call(InputAction.CallbackContext context)
    { Layer |= 1 << 5; }

public void ResetLayer() => Layer &= (1 << (int)Key.Move);
    public void OnLayer(Key checkKey)
    {
        int addLayer = 1 << (int)Layer;
        Layer |= addLayer;
    }
    public bool ContainLayer(Key checkKey)
    {
        int checkLayer = 1 << (int)checkKey;
        checkLayer &= Layer & checkLayer;
        if (checkLayer > 0)
            return true;
        return false;
    }
    public void OffLayer(Key checkKey)
    {
        int offLayer = 1 << (int)checkKey;
        Layer &= ~offLayer;
    }
    public bool[] CheckLayers(params Key[] checkKeys)
    {
        bool[] checkLayers = new bool[checkKeys.Length];
        for (int i = 0; i < checkKeys.Length; i++)
            checkLayers[i] = ContainLayer(checkKeys[i]);
        return checkLayers;
    }













    /*
    void Test()
    {
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
            action = player.fsm.GetEventMethod(state);
            if (action != null)
                input.performed += action;
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
    */
}
