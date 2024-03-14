using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    public void EnterGame()
    {
        Manager.Scene.LoadScene("SeriaRoom");
    }
}
