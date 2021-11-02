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
    protected bool right = true;

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    protected override void Update()
    {
        base.Update();
        navAgent.speed = speed; // �������� � NawMesh ����� �������� ��������
        if (death) return;
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
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        if (follow.position.x < transform.position.x && right) Flip();
        if (follow.position.x > transform.position.x && !right) Flip();
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

    private void Flip() // ������� �������������� � ������ �������
    {
        right = !right;
        Vector2 scale = new Vector2(transform.localScale.x, transform.localScale.y);
        scale.x *= -1;
        transform.localScale = scale;
    }

    public override void Death()
    {
        base.Death();
        navAgent.isStopped = true;
    }


}
