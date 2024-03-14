using System;
using UnityEngine;

public class PlayerUIData : MonoBehaviour
{
    [SerializeField]
    StatusUI statusUI;

    public void SetHp(float _hp) => statusUI.UpdateHp(_hp);
    public void SetMp(float _mp) => statusUI.UpdateMp(_mp);
}
