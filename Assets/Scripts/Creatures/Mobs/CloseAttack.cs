using UnityEngine;

public class CloseAttack : Stalker, IAttackWithWeapon
{
    [Header("Close Attack:")]
    public float radiusTriggerAttack;
    protected Weapon weapon;

    protected override void Start()
    {
        base.Start();
        ChangeWeapon(GetComponentInChildren<Weapon>());
    }


    protected override void Update()
    {
        base.Update();
        if (isDeath) return;
        if (CheckAttack() && weapon.IsRecharged())
        {
            weapon.RechargeAgain();
            anim.SetTrigger("Attack");
        }
            
    }



    protected virtual bool CheckAttack()
    {
        if (follow == null) return false;
        Collider2D coll = Physics2D.OverlapCircle(standPosition.position, radiusTriggerAttack, detectionableLayer);
        if (coll != null) return true;
        return false;
    }


    public void Test()
    {
        print("Work Test");
    }

    public virtual void Attack()
    {
        print("Work Attack");
        weapon.Attack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(standPosition.position, radiusTriggerAttack);
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (weapon != null) Destroy(weapon.gameObject);
        weapon = newWeapon;
        anim.runtimeAnimatorController = weapon.animController;
    }


}
