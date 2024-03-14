using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElvenGaurdScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
            player.Pose = new Vector3(pos[enterIdx].position.x,0, pos[enterIdx].position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}