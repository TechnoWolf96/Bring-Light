

public class RangedAttack : SmartRangedAttackPosition, IAttackWithWeapon
{
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
        if (TargetIsVisible) return true;
        return false;
    }

    public virtual void Attack()
    {
        weapon.Attack();
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (weapon != null) Destroy(weapon.gameObject);
        weapon = newWeapon;
        anim.runtimeAnimatorController = weapon.animController;
    }

}
