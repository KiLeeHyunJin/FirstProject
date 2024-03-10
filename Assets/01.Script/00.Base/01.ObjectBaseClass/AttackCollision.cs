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
    /// ������Ʈ���� �浹
    /// </summary>
    /// <param name="_ownerPos"> ĳ���� ��ġ</param>
    /// <param name="_transformPos"> ������ ������ </param>
    /// <param name="_size">���� ����</param>
    /// <param name="_offsetPos">���� ����</param>
    /// <returns></returns>
    public bool CheckAttackCollision(Vector3 _ownerPos,Vector3 _size , Vector2 _offsetPos)
    {
        targetPos.Pos = new Vector3(_ownerPos.x, _ownerPos.y + _offsetPos.y, _ownerPos.z);
        targetPos.Range = _size;
        targetPos.Offset = new Vector2(_offsetPos.x,0);

        return Check();
    }
    /// <summary>
    /// ĳ���� ���� �浹
    /// </summary>
    /// <param name="_targetPos"> ���� ��ġ</param>
    /// <param name="_targetSize"> ũ��</param>
    /// <param name="_offset"> ����϶��� ���� ��ġ</param>
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
    /// ���� �浹
    /// </summary>
    /// <returns></returns>
    private bool SideCollision()
    {
        float ownerX = ownerPos.X + ownerPos.Offset.x;
        float targetX = targetPos.Pos.x + targetPos.Offset.x;
        //���� ���� �� >= Ÿ�� ���� ��
        //���� ���� �� <= ��� ���� ��
        if (ownerX + ownerPos.Size.x >= targetX - targetPos.Range.x &&
            ownerX + ownerPos.Size.x <= targetX + targetPos.Range.x) //����
        {
            return TopDownCollision();
        }
            //���� ���� �� <= ��� ���� ��
            //���� ���� �� >= ��� ���� ��
        else if
            (ownerX - ownerPos.Size.x <= targetX + targetPos.Range.x &&
             ownerX - ownerPos.Size.x >= targetX - targetPos.Range.x) //����
        {
            return TopDownCollision();
        }
        //Ÿ���� ���� ������ �����ִ� ���
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
            //���� �ϴ� �� >= ��� �ϴ� ��
            //���� �ϴ� �� <= ��� ��� ��
            //������ �ϴ��� Ÿ�ٿ� �����ִ� ���
        if (ownerZ - ownerPos.Size.z >= targetZ - targetPos.Range.z  &&
            ownerZ - ownerPos.Size.z <= targetZ + targetPos.Range.z ) 
        {
            return true;
        }
            //���� ��� �� <= ��� ��� ��
            //���� ��� �� >= ��� ��� ��
            //������ ����� Ÿ�ٿ� �����ִ� ���
        else if (
            ownerZ + ownerPos.Size.z <= targetZ + targetPos.Range.z  &&
            ownerZ + ownerPos.Size.z >= targetZ - targetPos.Range.z )
        {
            return true;
        }
        //Ÿ�� ����� ���� ���� �ȿ� �����ִ� ���
        else if (
            targetZ + targetPos.Range.z >= ownerZ - ownerPos.Size.z &&
            targetZ + targetPos.Range.z <= ownerZ + ownerPos.Size.z)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// ���� �浹
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
