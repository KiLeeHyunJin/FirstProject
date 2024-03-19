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
    [field: SerializeField] public int DefaultLayer { get; private set; }
    [field: SerializeField] public int SlotKeyLayer { get; private set; }
    public QuickKey[] QuickKeys { get { return QuickKeyArray; } }
    public DefaultKey[] DefaultKeys { get { return DefaultArray; } }
    public SlotKey[] SlotKeys { get { return SlotKeyArray; } }
    public enum SlotKey
    {
        Q, W, E, R, T
    }
    public enum QuickKey
    {
        A, S, D, F, G, Non
    }
    public enum DefaultKey
    {
        Move, C, X, Non
    }
    SlotKey[] SlotKeyArray = new SlotKey[] { SlotKey.Q, SlotKey.W, SlotKey.E, SlotKey.R, SlotKey.T };
    QuickKey[] QuickKeyArray = new QuickKey[] { QuickKey.A, QuickKey.S, QuickKey.D, QuickKey.F, QuickKey.G };
    DefaultKey[] DefaultArray = new DefaultKey[] { DefaultKey.Move, DefaultKey.C, DefaultKey.X };
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
    void OnQ(InputValue value)
    {
        if (value.isPressed)
            SlotKeyLayer |= 1 << (int)SlotKey.Q;
    }

    void OnW(InputValue value)
    {
        if (value.isPressed)
            SlotKeyLayer |= 1 << (int)SlotKey.W;
    }

    void OnE(InputValue value)
    {
        if (value.isPressed)
            SlotKeyLayer |= 1 << (int)SlotKey.E;
    }

    void OnR(InputValue value)
    {
        if (value.isPressed)
            SlotKeyLayer |= 1 << (int)SlotKey.R;
    }

    void OnT(InputValue value)
    {
        if (value.isPressed)
            SlotKeyLayer |= 1 << (int)SlotKey.T;
    }

    void OnX(InputValue value)
    {
        if (value.isPressed)
            DefaultLayer |= 1 << (int)DefaultKey.X;
    }
    void OnC(InputValue value)
    {
        if (value.isPressed)
            DefaultLayer |= 1 << (int)DefaultKey.C;
    }
    void OnA(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)QuickKey.A;
    }
    void OnS(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)QuickKey.S;
    }
    void OnD(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)QuickKey.D;
    }
    void OnF(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)QuickKey.F;
    }
    void OnG(InputValue value)
    {
        if (value.isPressed)
            Layer |= 1 << (int)QuickKey.G;
    }
    public void OffMoveLayer() => OffLayer(DefaultKey.Move);
    public void OnMoveLayer() => DefaultLayer |= 1 << (int)DefaultKey.Move;
    public void OffLayer(DefaultKey checkKey)
    {
        int offLayer = 1 << (int)checkKey;
        DefaultLayer &= ~offLayer;
    }

    public void ResetLayer()
    {
        Layer = 0;
        DefaultLayer &= (1 << (int)DefaultKey.Move);
        SlotKeyLayer = 0;
    }
    public void OnLayer(QuickKey checkKey) => Layer |= 1 << (int)Layer;
    public bool ContainLayer(QuickKey checkKey) => ContainCheck(Layer, 1 << (int)checkKey);
    public bool ContainLayer(DefaultKey checkKey) => ContainCheck(DefaultLayer, 1 << (int)checkKey);
    public bool ContainLayer(SlotKey checkKey) => ContainCheck(SlotKeyLayer, 1 << (int)checkKey);
    bool ContainCheck(int layer , int  checkLayer)
    {
        checkLayer &= layer & checkLayer;
        if (checkLayer > 0)
            return true;
        return false;
    }
    public bool[] CheckLayers(params QuickKey[] checkKeys)
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
