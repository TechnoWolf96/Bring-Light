using UnityEngine;

public class SmartRangedAttackPosition : SmartDistance
{
    [Header("Smart Ranged Attack Position:")]
    public LayerMask obstacleBulletLayer;     // Слои, которые будут считаться препятствием для пуль
    public float rayThickness;          // Толщина луча для проверки препятствий
    protected bool targetIsVisible = false;     // Видна ли цель

    public bool TargetIsVisible => targetIsVisible;

    protected override void RunFromFollow()
    {
        // Если существо оказалось слишком близко к цели, то оно должно отдалиться на расстояние minStopDistance
        // Но если препятствие заслонит цель, то оно не отступает
        RaycastHit2D info = Physics2D.CircleCast(transform.position, rayThickness / 2, follow.transform.position - transform.position,
            Vector2.Distance(follow.transform.position, transform.position), obstacleBulletLayer);
        if (info.collider == null)
            base.RunFromFollow();
    }

    protected override void Stalk()
    {
        // Если цель скрыта за препятствием - то бежим к ней, игнорируя дальнейшие условия
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
