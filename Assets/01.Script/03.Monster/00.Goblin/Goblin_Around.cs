using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Around : MonsterState<GoblinState>
{
    [Header("상태 유지 시간")]
    [SerializeField] Vector2 randomMovingTime;
    [Header("방향 재설정 시간")]
    [SerializeField] Vector2 changeTime;
    bool reCheck;
    bool isTransition;
    Vector2 randomMove;
    public override void Enter()
    {
        isTransition = false;
        reCheck = true;
        if (randomMoveCo != null)
            owner.StopCoroutine(randomMoveCo);
        if (randomCo != null)
            owner.StopCoroutine(randomCo);
        randomCo = owner.StartCoroutine(RandomCo());
    }
    public override void Update()
    {
        if (reCheck == false)
            return;

        float duringTime = UnityEngine.Random.Range(changeTime.x, changeTime.y);

        int horizontal = UnityEngine.Random.Range(0, 2) == 1 ? 1 : 0;
        int horizonDirec = UnityEngine.Random.Range(0, 2) == 1 ? 1 : -1;

        int vertical = UnityEngine.Random.Range(0, 2) == 1 ? 1 : 0;
        int verticalDirec = UnityEngine.Random.Range(0, 2) == 1 ? 1 : -1;
        randomMove = new Vector2(horizontal * horizonDirec, vertical * verticalDirec);
        if (randomMoveCo != null)
            owner.StopCoroutine(randomMoveCo);
        randomMoveCo = owner.StartCoroutine(RandomMove(duringTime));
    }
    Coroutine randomCo = null;
    IEnumerator RandomCo()
    {
        float moveTime = UnityEngine.Random.Range(randomMovingTime.x, randomMovingTime.y);
        float checkTime = 0;

        while (true)
        {
            checkTime += Time.deltaTime;
            pos.AddForceMove(randomMove.normalized * owner.moveSpeed);
            if (checkTime > moveTime)
                break;
            yield return new WaitForFixedUpdate();
        }
        isTransition = true;
    }

    Coroutine randomMoveCo = null;
    IEnumerator RandomMove(float waitTime)
    {
        reCheck = false;
        float duringTime = waitTime;
        yield return new WaitForSeconds(duringTime);
        reCheck = true;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        pos.Synchro();
        owner.FlipCheck(pos.Velocity2D());
    }
    public override void Exit()
    {
        base.Exit();
        pos.ForceZero(KeyCode.X);
        if (randomMoveCo != null)
            owner.StopCoroutine(randomMoveCo);
        if (randomCo != null)
            owner.StopCoroutine(randomCo);
    }
    public override void Transition()
    {
        if (isTransition)
            owner.SetState = GoblinState.Idle;
    }
}
