using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriaRoomScence : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    private void Start()
    {
        if (player != null)
            player.Pose = new Vector3(pos[Manager.Scene.enterIdx].position.x, 0, pos[Manager.Scene.enterIdx].position.y);
    }
}
