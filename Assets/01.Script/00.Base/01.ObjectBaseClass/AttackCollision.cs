using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision
{
    public struct TargetPos
    {
        public Vector3 Pos;
        public Vector3 Range;
        public Vector2 Offset;
    }

    TargetPos targetPos;
    TransformPos ownerPos;
    public AttackCollision(TransformPos _owner)
    {
        ownerPos = _owner;
    }
    /// <summary>
    /// 오브젝트와의 충돌
    /// </summary>
    /// <param name="_ownerPos"> 캐릭터 위치</param>
    /// <param name="_transformPos"> 공격의 오프셋 </param>
    /// <param name="_size">공격 범위</param>
    /// <param name="_offsetPos">오차 범위</param>
    /// <returns></returns>
    public bool CheckAttackCollision(Vector3 _ownerPos,Vector3 _size , Vector2 _offsetPos)
    {
        targetPos.Pos = new Vector3(_ownerPos.x, _ownerPos.y + _offsetPos.y, _ownerPos.z);
        targetPos.Range = _size;
        targetPos.Offset = new Vector2(_offsetPos.x,0);

        return Check();
    }
    /// <summary>
    /// 캐릭터 간의 충돌
    /// </summary>
    /// <param name="_targetPos"> 원점 위치</param>
    /// <param name="_targetSize"> 크기</param>
    /// <param name="_offset"> 평면일때의 오차 위치</param>
    /// <returns></returns>
    public bool CheckCollision(Vector3 _targetPos, Vector3 _targetSize, Vector2 _offset = new Vector2())
    {
        targetPos.Pos = _targetPos;
        targetPos.Range = _targetSize;
        targetPos.Offset = _offset;

        return Check();
    }
    private bool Check()
    {
        if (SideCollision() == false)
            return false;
        if (VerticalCollision() == false)
            return false;
        return true;
    }
    /// <summary>
    /// 범위 충돌
    /// </summary>
    /// <returns></returns>
    private bool SideCollision()
    {
        float ownerX = ownerPos.X + ownerPos.Offset.x;
        float targetX = targetPos.Pos.x + targetPos.Offset.x;
        //오너 우측 변 >= 타겟 좌측 변
        //오너 우측 변 <= 상대 우측 변
        if (ownerX + ownerPos.Size.x >= targetX - targetPos.Range.x &&
            ownerX + ownerPos.Size.x <= targetX + targetPos.Range.x) //우측
        {
            return TopDownCollision();
        }
            //오너 좌측 변 <= 상대 우측 변
            //오너 좌측 변 >= 상대 좌측 변
        else if
            (ownerX - ownerPos.Size.x <= targetX + targetPos.Range.x &&
             ownerX - ownerPos.Size.x >= targetX - targetPos.Range.x) //좌측
        {
            return TopDownCollision();
        }
        //타겟이 오너 영역에 속해있는 경우
        else if
            (
            targetX + targetPos.Range.x >= ownerX - ownerPos.Size.x &&
            targetX + targetPos.Range.x <= ownerX + ownerPos.Size.x)
        {
            return TopDownCollision();
        }
        return false;
    }

    bool TopDownCollision()
    {
        float ownerZ = ownerPos.Z + ownerPos.Offset.y;
        float targetZ = targetPos.Pos.z + ownerPos.Offset.y;
            //오너 하단 변 >= 상대 하단 변
            //오너 하단 변 <= 상대 상단 변
            //오너의 하단이 타겟에 속해있는 경우
        if (ownerZ - ownerPos.Size.z >= targetZ - targetPos.Range.z  &&
            ownerZ - ownerPos.Size.z <= targetZ + targetPos.Range.z ) 
        {
            return true;
        }
            //오너 상단 변 <= 상대 상단 변
            //오너 상단 변 >= 상대 상단 변
            //오너의 상단이 타겟에 속해있는 경우
        else if (
            ownerZ + ownerPos.Size.z <= targetZ + targetPos.Range.z  &&
            ownerZ + ownerPos.Size.z >= targetZ - targetPos.Range.z )
        {
            return true;
        }
        //타겟 상단이 오너 영역 안에 속해있는 경우
        else if (
            targetZ + targetPos.Range.z >= ownerZ - ownerPos.Size.z &&
            targetZ + targetPos.Range.z <= ownerZ + ownerPos.Size.z)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 높이 충돌
    /// </summary>
    /// <returns></returns>
    private bool VerticalCollision() 
    {
        if (ownerPos.Y + ownerPos.Size.y >= targetPos.Pos.y + targetPos.Range.y &&
            ownerPos.Y <= targetPos.Pos.y + targetPos.Range.y)
            return true;
        else if (ownerPos.Y <= targetPos.Pos.y + targetPos.Range.y &&
            ownerPos.Y + ownerPos.Size.y >= targetPos.Pos.y)
            return true;
        return false;
    }

}
