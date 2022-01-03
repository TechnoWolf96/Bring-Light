using UnityEngine;


/* ����� ���������� ���������� ����� ����� ������������� � ����� ��������.
 * �������������� ��� ������������� � ������ � ������� ������, ����� �� ����� ��������� ������� ������ ��� ���������� ������� ������.
     * �������� ��������������� �� ��������� ���������� �� ������� follow, ��������� � ���������
     * ����� minStopDistance � maxStopDistance.
     * ���� ���� ������ �� �������� �� ����������, ������� maxStopDistance,
     * �� �������� �������� ����� ��������� � ������� ����, ����� ������ ��������� ����������.
     * ���� ���������� �� ������� follow ������ ��� runFromDistance, �� �������� �������� ����������
     * �� ����� �������� ������������ ���������.
     */
public class SmartDistance : Stalker
{
    [Header("Smart Distance:")]
    [Min(0)] public float maxStopDistance;           // ������������ ���������� ��������� �� ����
    [Min(0)] public float minStopDistance;           // ����������� ���������� ��������� �� ����
    [Min(0)] public float runFromDistance;           // ���������� ������ ���������

    //protected bool canGoBack;                  // ����� �� �������� ��������� ��� ����������� ����������
    protected float randomStopDistance = 0;    // ���������� ��� �������� ����������������� ��������� ��������� ���������
    protected bool distanceDefined;            // ����������, ���������� �� ��� ��������� ���������


    protected override void Start()
    {
        base.Start();
        SetRandomDistance();
    }

    


    protected void SetRandomDistance()
    {
        randomStopDistance = Random.Range(minStopDistance, maxStopDistance);
        distanceDefined = true;
        print(randomStopDistance);
    }
    protected virtual void RunFromFollow()
    {
        Vector2 newPosition = transform.position + (transform.position - follow.transform.position).normalized;
        Collider2D collider = Physics2D.OverlapCircle(newPosition, 0.7f);
        if (collider != null)
        {
            print("Stop");
            RunStop();
            return;
        }
        navAgent.isStopped = false;
        navAgent.SetDestination(newPosition);
        anim.SetBool("Walk", true);
    }
    protected virtual void RunToFollow()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.transform.position);
        anim.SetBool("Walk", true);
    }
    protected virtual void RunStop()
    {
        // �.� ���� ����������, ��� ������ �� runToDistance ��������� ��������� ����������� ������
        distanceDefined = false;
        navAgent.isStopped = true;
        anim.SetBool("Walk", false);
    }

    protected override void Stalk()
    {
        // �.� �������� � NavAgent � � velocity ������������� ���� �� �����, �� velocity ����� ��������
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        // ��� ������ �������� ����������� ���������� ��������� - ������ �������� ��������� ���������
        if (!distanceDefined && Vector2.Distance(follow.transform.position, transform.position) > maxStopDistance)
            SetRandomDistance();

        // ���� �� �������� ������ ��������� - ����������
        if (distanceDefined && Vector2.Distance(follow.transform.position, transform.position) > randomStopDistance)
        {
            RunToFollow();
            return;
        }
        // ���� �������� �������� ������������� ���������, �� �� ��������� ����� runFromDistance - ���������������
        if (Vector2.Distance(follow.transform.position, transform.position) > runFromDistance-0.1f)
        {
            RunStop();
            return;
        } 
        // ���� �������� ��������� ������� ������ � ����, �� ��� ������ ���������� �� ���������� minStopDistance
        if (Vector2.Distance(follow.transform.position, transform.position) < runFromDistance)
        {
            RunFromFollow();
            return;
        }
            
        




    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxStopDistance);
        Gizmos.DrawWireSphere(transform.position, minStopDistance);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, runFromDistance);
    }

   
}
