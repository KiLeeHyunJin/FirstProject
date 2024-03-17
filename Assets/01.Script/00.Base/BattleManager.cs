using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int monsterCount;

    private void Start()
    {
        monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;
    }
    public void MinusMonster()
    {
        monsterCount--;
        if(monsterCount <= 0 )
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {

    }
}
