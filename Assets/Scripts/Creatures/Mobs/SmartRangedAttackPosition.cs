using UnityEngine;

public class SmartRangedAttackPosition : SmartDistance
{
    [Header("Smart Ranged Attack Position:")]
    public LayerMask obstacleBulletLayer;     // ����, ������� ����� ��������� ������������ ��� ����
    public float rayThickness;          // ������� ���� ��� �������� �����������
    protected bool targetIsVisible = false;     // ����� �� ����

    public bool TargetIsVisible => targetIsVisible;

    protected override void RunFromFollow()
    {
        // ���� �������� ��������� ������� ������ � ����, �� ��� ������ ���������� �� ���������� minStopDistance
        // �� ���� ����������� �������� ����, �� ��� �� ���������
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness / 2, follow.transform.position - transform.position,
            Vector2.Distance(follow.transform.position, transform.position), obstacleBulletLayer);
        if (info.collider == null)
            base.RunFromFollow();
    }

    protected override void Stalk()
    {
        // ���� ���� ������ �� ������������ - �� ����� � ���, ��������� ���������� �������
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness / 2, follow.transform.position - transform.position,
            Vector2.Distance(follow.transform.position, transform.position), obstacleBulletLayer);

        if (info.collider != null)
        {
            RunToFollow();
            targetIsVisible = false;
        }


        else
        {
            base.Stalk();
            targetIsVisible = true;
        }

    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector3 size = new Vector3(distanceDetection, rayThickness, rayThickness);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);
    }


}
