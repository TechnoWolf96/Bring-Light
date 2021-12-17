using UnityEngine;
using UnityEngine.AI;


// ����� "��������������"
public abstract class Stalker : Creature
{
    [Header("Stalker:")]
    public float distanceDetection; // ��������� ����������� ������� ��� �������������
    public LayerMask detectionableLayer; // ����, ������� ����������� �������������� (���� �������)

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
        if (isDeath) return;
        CheckStalk(); // ����� ������� ��� �������������
        if (!isStunned && follow != null) Stalk(); // ���� ������ �� ������� � ���� �� ��� ������, �� �������� �������������
    }

    protected void CheckStalk() // ��������, ���� �� � ���� ����������� ������� ������� ����
    {
        if (follow == null) // ���� �� �� ��� ������, �� ���� ������ ��� �������������
            follow = Physics2D.OverlapCircle(transform.position, distanceDetection, detectionableLayer)?.GetComponent<Transform>();
    }

    protected virtual void Stalk() // ������ �������� ����� ���������� � �������� �������������
    {
        // �.� �������� � NavAgent � � velocity ������������� ���� �� �����, �� velocity ����� ��������
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;

        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        Vector2 directionMovement = (follow.position - transform.position).normalized;
        anim.SetFloat("HorizontalMovement", directionMovement.x);
        anim.SetFloat("HorizontalMovement", directionMovement.y);
        anim.SetBool("Walk", true);
    }

    protected virtual void OnDrawGizmosSelected() // ������ ������� �����������
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceDetection);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        navAgent.isStopped = true; // ��� ��������� �������������� �� ����� ������ �� ����������
        if (attacking.position != transform.position)   // ���� ��������� �� �� ���, ��:
            follow = attacking; // ��� ��������� ����� �������������� ����� �� ����������
    }



    public override void Death()
    {
        base.Death();
        navAgent.enabled = false;
    }


}
