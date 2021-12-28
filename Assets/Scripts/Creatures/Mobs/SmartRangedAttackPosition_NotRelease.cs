using UnityEngine;


public class SmartRangedAttackPosition_NotRelease : SmartDistance_NotRelease
{
    [Header("Smart Ranged Attack Position:")]
    public LayerMask obstacleLayer;
    public float rayThickness;          // ������� ���� ��� �������� �����������

    protected virtual void Attack() { }

    protected override void RunFromFollow()
    {
        // ���� �������� ��������� ������� ������ � ����, �� ��� ������ ���������� �� ���������� minStopDistance
        // �� ���� ����������� �������� ����, �� ��� �� ���������
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness/2, follow.position - transform.position,
            Vector2.Distance(follow.position, transform.position), obstacleLayer);
        if (info.collider == null)
            base.RunFromFollow();
            
    }

    protected override void Stalk()
    {
        // ���� ���� ������ �� ������������ - �� ����� � ���, ��������� ���������� �������
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness / 2, follow.position - transform.position,
            Vector2.Distance(follow.position, transform.position), obstacleLayer);

        if (info.collider != null)
            RunToFollow();

        else
        {
            base.Stalk();
            Attack();     // ����� ���� - �������� �� ���
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
