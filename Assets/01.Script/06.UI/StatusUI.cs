using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] Image HpUI; 
    [SerializeField] Image MpUI;
    GameManager manager;
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    public void UpdateHp()
    {
        HpUI.fillAmount = manager.Hp;
    }
    public void UpdateMp()
    {
        MpUI.fillAmount = manager.Mp;
    }

}
