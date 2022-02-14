using UnityEngine;

public class AbleToSeekRangedAttackPosition : SmartDistance
{
    [Header("Able to seek ranged attack position:")]
    [SerializeField] protected float maxCloseness;
    [SerializeField] protected LayerMask obstacleBulletLayer;
    public bool targetIsVisible { get; protected set; }

    protected override void StateUpdate()
    {
        if (follow == null) { FindNewTarget(); return; }
        CheckTargetIsVisible();
        LookAt(follow.position);

        // Цель видна? Нет - преследуем до максимальной близости или пока не увидим цель
        if (!targetIsVisible) Stalk();

        // Цель видна? Да - выполняем ранее реализованные в предке действия
        else base.StateUpdate();
    }
    protected override void Stalk()
    {
        if (Vector2.Distance(transform.position, follow.position) > maxCloseness) base.Stalk();
        else StalkStop();
    }
    protected void CheckTargetIsVisible()
    {
        if (follow == null) targetIsVisible = false;
        RaycastHit2D info = Physics2D.Raycast(bodyCenter.position, followBodyCenter.position - bodyCenter.position,
            Vector2.Distance(followBodyCenter.position, bodyCenter.position), obstacleBulletLayer);
        if (info.collider == null) { targetIsVisible = true; }
        else { targetIsVisible = false; }
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxCloseness);
    }


}
