using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int monsterCount;
    DungeonDoor[] doors;
    private void Start()
    {
        monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;
        doors = FindObjectsOfType<DungeonDoor>();
        for (int i = 0; i < doors.Length; i++)
            doors[i].CloseDoor();
    }
    public void MinusMonster()
    {
        monsterCount--;
        if(monsterCount <= 0 )
            OpenDoor();
    }

    void OpenDoor()
    {
        for (int i = 0; i < doors.Length; i++)
            doors[i].OpenDoor();
    }
}
