using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ����� "��������������"
public abstract class Stalker : Creature
{
    [Header("Stalker:")]
    public float distance; // ��������� ����������� ������� ��� �������������
    public LayerMask layer; // ����, ������� ����������� �������������� (���� �������)

    protected Transform follow; // ������� ������ ��� �������������
    protected NavMeshAgent navAgent; // ����� NawMesh, ������������ �� ������ �������

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed; // �������� � NawMesh ����� �������� ��������
    }

    protected override void Update()
    {
        CheckStalk(); // ����� ������� ��� �������������
        if (!stunned && follow != null) Stalk(); // ���� ������ �� ������� � ���� �� ��� ������, �� �������� �������������
    }

    protected void CheckStalk() // ��������, ���� �� � ���� ����������� ������� ������� ����
    {
        if (follow == null) // ���� �� �� ��� ������, �� ���� ������ ��� �������������
            follow = Physics2D.OverlapCircle(transform.position, distance, layer)?.GetComponent<Transform>();
    }

    protected void Stalk() // ������ �������� ����� ����������
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        anim.SetTrigger("Walk");
    }

    protected virtual void OnDrawGizmosSelected() // ������ ������� �����������
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        navAgent.isStopped = true; // ��� ��������� �������������� �� ����� ������ �� ����������
        follow = attacking; // ��� ��������� ����� �������������� ����� �� ����������
    }

}
