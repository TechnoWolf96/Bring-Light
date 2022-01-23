using UnityEngine;

public class CloseAttack : Stalker, IAttackWithWeapon
{
    [Header("Close Attack:")]
    public float radiusTriggerAttack;
    protected Weapon weapon;

    protected override void Start()
    {
        base.Start();
        weapon = GetComponentInChildren<Weapon>();
        anim.runtimeAnimatorController = weapon.animController;
    }


    protected override void Update()
    {
        base.Update();
        if (CheckAttack() && weapon.IsRecharged())
        {
            weapon.BeginAttack();
            anim.SetTrigger("Attack");
        }
            
    }



    protected virtual bool CheckAttack()
    {
        if (follow == null) return false;
        Collider2D coll = Physics2D.OverlapCircle(transform.position, radiusTriggerAttack, detectionableLayer);
        if (coll != null) return true;
        return false;
    }



    public virtual void Attack()
    {
        weapon.Attack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusTriggerAttack);
    }


}
