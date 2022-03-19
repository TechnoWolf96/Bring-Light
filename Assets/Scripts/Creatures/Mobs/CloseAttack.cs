using UnityEngine;

public class CloseAttack : Stalker, IAttackWithWeapon
{
    [Header("Close Attack:")]
    [SerializeField] protected float radiusTriggerAttack;
    protected Weapon currentWeapon;

    protected override void Start()
    {
        base.Start();
        currentWeapon = GetComponentInChildren<Weapon>();
        anim.runtimeAnimatorController = currentWeapon.animController;
    }

    protected override void StateUpdate()
    {
        base.StateUpdate();
        if (CheckAttack())
        {
            anim.SetTrigger("Attack");
        }
    }

    protected virtual bool CheckAttack()
    {
        if (follow == null) return false;
        Collider2D coll = Physics2D.OverlapCircle(transform.position, radiusTriggerAttack, detectionableLayer);
        return coll != null;
    }

    public virtual void AttackMoment()
    {
        currentWeapon.Attack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusTriggerAttack);
    }


}
