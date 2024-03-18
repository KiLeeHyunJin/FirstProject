using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    protected override void Start()
    {
        base.Start();
    }
        public void EnterGame()
    {
        Manager.Scene.LoadScene("SeriaRoom");
    }
}
