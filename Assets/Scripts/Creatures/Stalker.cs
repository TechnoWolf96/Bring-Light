using UnityEngine;
using UnityEngine.AI;


// ����� "��������������"
public class Stalker : Creature
{
    [Header("Stalker:")]
    public float distanceDetection; // ��������� ����������� ������� ��� �������������
    public LayerMask detectionableLayer; // ����, ������� ����������� �������������� (���� �������)

    [HideInInspector] public Creature follow; // ������� ������ ��� �������������
    [HideInInspector] public NavMeshAgent navAgent; // ����� NawMesh, ������������ �� ������ �������

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed; // �������� � NawMesh ����� �������� ��������
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    protected override void Update()
    {
        base.Update();
        CheckStalk(); // ����� ������� ��� �������������
        if (follow != null)
        {
            Stalk(); // ���� ������ �� ������� � ���� �� ��� ������, �� �������� �������������
            LookAt(follow.transform.position);
        }
    }

    protected void CheckStalk() // ��������, ���� �� � ���� ����������� ������� ������� ����
    {
        if (follow == null) // ���� �� �� ��� ������, �� ���� ������ ��� �������������
        {
            Creature newFollow = Physics2D.OverlapCircle(transform.position, distanceDetection, detectionableLayer)?.GetComponent<Creature>();
            if (newFollow != null) SetFollow(newFollow);
            else follow = null;
        }
            
    }
    protected void SetFollow(Creature newTarget)
    {
        follow = newTarget;
    }

    protected virtual void Stalk() // ������ �������� ����� ���������� � �������� �������������
    {
        // �.� �������� � NavAgent � � velocity ������������� ���� �� �����, �� velocity ����� ��������
        //if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        anim.SetBool("Walk", true);
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.transform.position);
    }

    protected virtual void OnDrawGizmosSelected() // ������ ������� �����������
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceDetection);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        if (attacking.position != transform.position)   // ���� ��������� �� �� ���, ��:
            SetFollow(attacking.GetComponent<Creature>()); // ��� ��������� ����� �������������� ����� �� ����������
    }


    public override void Death()
    {
        navAgent.enabled = false;
        base.Death();
    }
}
