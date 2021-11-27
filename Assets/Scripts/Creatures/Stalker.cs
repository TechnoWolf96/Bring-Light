using UnityEngine;
using UnityEngine.AI;


// ����� "��������������"
public abstract class Stalker : Creature
{
    [Header("Stalker:")]
    public float distanceDetection; // ��������� ����������� ������� ��� �������������
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
            follow = Physics2D.OverlapCircle(transform.position, distanceDetection, layer)?.GetComponent<Transform>();
    }

    protected virtual void Stalk() // ������ �������� ����� ���������� � �������� �������������
    {
        FlipAndNullVelocity();
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        anim.SetTrigger("Walk");
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
        follow = attacking; // ��� ��������� ����� �������������� ����� �� ����������
    }

    protected void Flip() // ������� �������������� � ������ �������
    {
        right = !right;
        Vector2 scale = new Vector2(transform.localScale.x, transform.localScale.y);
        scale.x *= -1;
        transform.localScale = scale;
    }
    protected void FlipAndNullVelocity()    // ����������� �������� � ��������� velocity
    {
        // �.� �������� � NavAgent � � velocity ������������� ���� �� �����, �� velocity ����� ��������
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        if (follow.position.x < transform.position.x && right) Flip();
        if (follow.position.x > transform.position.x && !right) Flip();
    }


    public override void Death()
    {
        base.Death();
        navAgent.enabled = false;
    }


}
