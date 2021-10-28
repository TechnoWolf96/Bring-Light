using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon:")]
    public float recharge;              // �����������
    public AttackParameters attack;     // ��������� �����
    public LayerMask layer;             // ���� ��������, �� �������� ����� ��������� �����

    protected Transform playerPos;  // ������ ������, ��������� ������
    protected Creature creature;        // ������ ��������, ��������� ������
    protected float rechargeTime;       // ������� ����� �� �����������

    protected abstract void Attack(); // �����

    protected virtual void Start()
    {
        rechargeTime = recharge;
        playerPos = transform.parent;
        creature = GetComponentInParent<Creature>();
    }

    protected virtual void FixedUpdate() // ���������� �������� ������� �� �����������
    {
        rechargeTime -= Time.deltaTime;
    }

    protected bool IsRecharged()
    {
        return rechargeTime < 0;
    }

}
