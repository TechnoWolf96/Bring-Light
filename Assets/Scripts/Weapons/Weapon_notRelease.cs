using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_notRelease : MonoBehaviour
{
    [Header("Weapon:")]
    public float recharge;              // �����������


    protected Transform playerPos;      // ������� ������, ��������� ������
    protected Creature_NotRelease creature;        // ������ �������� ������, ��������� ������
    protected float rechargeTime;       // ������� ����� �� �����������

    protected abstract void Attack(); // �����

    protected virtual void Start()
    {
        rechargeTime = recharge;
        playerPos = transform.parent;
        creature = GetComponentInParent<Creature_NotRelease>();
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
