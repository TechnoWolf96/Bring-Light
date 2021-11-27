using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class SmartRangedAttack : SmartDistance
{
    [Header("Smart Ranged Attack:")]
    public LayerMask obstacleLayer;
    public float rayThickness;          // ������� ���� ��� �������� �����������

    protected virtual void RangedAttack() { }

    protected override void Update()
    {
        base.Update();
        Debug.DrawRay(transform.position, follow.position - transform.position);
    }

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

        else base.Stalk();

    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector3 size = new Vector3(distanceDetection, rayThickness, rayThickness);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);
    }


}
