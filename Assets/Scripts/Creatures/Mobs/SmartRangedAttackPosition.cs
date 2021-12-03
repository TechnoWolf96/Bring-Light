using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmartRangedAttackPosition : SmartDistance
{
    [Header("Smart Ranged Attack Position:")]
    public LayerMask obstacleLayer;
    public float rayThickness;          // Толщина луча для проверки препятствий

    protected virtual void RangedAttack() { }

    protected override void RunFromFollow()
    {
        // Если существо оказалось слишком близко к цели, то оно должно отдалиться на расстояние minStopDistance
        // Но если препятствие заслонит цель, то оно не отступает
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness/2, follow.position - transform.position,
            Vector2.Distance(follow.position, transform.position), obstacleLayer);
        if (info.collider == null)
            base.RunFromFollow();
            
    }

    protected override void Stalk()
    {
        // Если цель скрыта за препятствием - то бежим к ней, игнорируя дальнейшие условия
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness / 2, follow.position - transform.position,
            Vector2.Distance(follow.position, transform.position), obstacleLayer);

        if (info.collider != null)
            RunToFollow();

        else
        {
            base.Stalk();
            RangedAttack();     // Видим цель - стреляем по ней
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
