using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCSpell : MonoBehaviour
{
    [Header("NPC spell:")]
    public int priority;  // ���� ���������� ������ ����� ���������� (������������� ��������� �� 1 �� 10)
    public float speedCast;                 // �������� �������� ���������� ����������

    protected Creature creature;

    protected virtual void Start()
    {
        creature = GetComponentInParent<Creature>();
    }


    // ���������� ��� ������ ������������ ����������
    public abstract void BeginCast();

    // ���������� � ������ ��������� ������������ ���������� (��� ��������� �����)
    public abstract void StopCast();

    // �������� �������� ������ �� ���������� (������ ����� ���������� � �������� Spellcast)
    public abstract void Activate();

    // ������ �������, �� �������� ����������� ��������� ����������
    public abstract void CalculatePriority();

}
