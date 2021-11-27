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

    protected bool canGoBack = true;                  // ����� �� �������� ��������� ��� ����������� ����������
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
        Vector2 newPosition = SearchPositionForGoBack();
        navAgent.isStopped = false;
        navAgent.SetDestination(newPosition);
        anim.SetTrigger("Walk");
    }
    protected virtual void RunToFollow()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        anim.SetTrigger("Walk");
    }
    protected virtual void RunStop()
    {
        // �.� ���� ����������, ��� ������ �� runToDistance ��������� ��������� ����������� ������
        distanceDefined = false;
        navAgent.isStopped = true;
        anim.SetTrigger("Stop");
    }

    protected override void Stalk()
    {
        FlipAndNullVelocity();
        // ���� �� �������� ������ ��������� - ����������
        if (distanceDefined && Vector2.Distance(follow.position, transform.position) > randomStopDistance)
            RunToFollow();
        // ���� �������� �������� ������������� ���������, �� �� ��������� ����� runFromDistance - ���������������
        if (Vector2.Distance(follow.position, transform.position) < randomStopDistance &&
            Vector2.Distance(follow.position, transform.position) > runFromDistance)
            RunStop();
        // ���� �������� ��������� ������� ������ � ����, �� ��� ������ ���������� �� ���������� minStopDistance
        if (Vector2.Distance(follow.position, transform.position) < runFromDistance)
            RunFromFollow();
        // ��� ������ �������� ����������� ���������� ��������� - ������ �������� ��������� ���������
        if (!distanceDefined && Vector2.Distance(follow.position, transform.position) > maxStopDistance)
            SetRandomDistance();

            


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

    private Vector3 SearchPositionForGoBack()
    {
        float length = runFromDistance - Vector2.Distance(follow.position, transform.position);
        Vector2 back = (transform.position - follow.position).normalized * length; 
        return (Vector2)transform.position + back;
    }
}
