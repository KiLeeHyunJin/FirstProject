using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] int selectIdx;
    [SerializeField] CharacterIcon[] characters;
    [SerializeField] Button startButton;
 
    private void Start()
    {
        OnClickCancel();
    }

    public void OnClick(int idx)
    {
        AllIdle();

        if (IndexCheck(idx) == false)
            return;
        if (idx == selectIdx)
        {
            OnClickCancel();
            return;
        }
        characters[idx].OnClick();
        selectIdx = idx;

        if(startButton.interactable == false)
            startButton.interactable = true;
    }
    void OnClickCancel()
    {
        AllIdle();
        selectIdx = -1;
        startButton.interactable = false;
    }

    public void OnEnter(int idx)
    {
        if (selectIdx == idx)
            return;

        if (IndexCheck(idx) == false)
            return;

        characters[idx].OnEnter();
    }
    public void OnExit(int idx)
    {
        if (selectIdx == idx)
            return;

        if (IndexCheck(idx) == false)
            return;

        characters[idx].OnExit();
    }

    bool IndexCheck(int idx)
    {
        if (idx >= characters.Length)
            return false;
        else if (characters[idx] == null)
            return false;
        return true;
    }

    void AllIdle()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
                continue;
            characters[i].OnExit();
        }
    }
}
