using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Walk : MonsterState<TauState>
{
    enum WalkType { Chase, Non }
    WalkType type;
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] Vector2 optimumRange;
    [SerializeField] Vector2 randomMovingTime;
    [SerializeField] Vector2 changeTime;
    Transform target;
    bool isTransition;
    bool reCheck;
    Vector2 randomMove;
    public override void Enter()
    {
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.WalkId);
        target = owner.sensor.target;
        if (target != null)
            type = WalkType.Chase;
        else
        {
            reCheck = false;
            type = WalkType.Non;
            if (randomMoveCo != null)
                owner.StopCoroutine(randomMoveCo);
            if (randomCo != null)
                owner.StopCoroutine(randomCo);
            randomCo = owner.StartCoroutine(RandomCo());
        }
    }
    public override void Update()
    {
        if (type == WalkType.Chase)
            Chase();
        else
            Escape();
    }
    Coroutine randomCo = null;
    IEnumerator RandomCo()
    {
        float moveTime = UnityEngine.Random.Range(randomMovingTime.x, randomMovingTime.y);
        float checkTime = 0;

        while (true)
        {
            checkTime += Time.deltaTime;
            pos.AddForceMove(randomMove.normalized * moveSpeed);
            if (checkTime > moveTime)
                break;
            yield return new WaitForFixedUpdate();
        }
        isTransition = true;
    }

    Coroutine randomMoveCo = null;
    IEnumerator RandomMove(float waitTime)
    {
        reCheck = true;
        float duringTime = waitTime;
        yield return new WaitForSeconds(duringTime);
        reCheck = false;
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
        owner.FlipCheck(pos.Velocity2D());
    }
    void Escape()
    {
        if (reCheck)
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
    void Chase()
    {
        Vector2 currentPos = new Vector2(pos.X, pos.Z);
        Vector3 targetPos = target.position;

        Vector2 distance = new Vector2(targetPos.x, targetPos.y) - currentPos;
        Vector2 direction =
            new Vector2(
            targetPos.x > currentPos.x ? 1 : -1,
            targetPos.y > currentPos.y ? 1 : -1);
        distance.x *= distance.x > 0 ? 1 : -1;
        distance.y *= distance.y > 0 ? 1 : -1;

        if (distance.x < optimumRange.x)
            direction.x = 0;
        if (distance.y < optimumRange.y)
            direction.y = 0;
        if (direction.x == 0 && direction.y == 0)
        {
            owner.FlipCheck(new Vector2(-1, 0));
            isTransition = true;
        }
        else
            pos.AddForceMove(direction.normalized * moveSpeed);

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
        {
            if (type == WalkType.Chase)
                owner.SetState = TauState.Atck1;
            else
                owner.SetState = TauState.Idle;
        }
    }
}
