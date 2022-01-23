

public class RangedAttack : SmartRangedAttackPosition, IAttackWithWeapon
{
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
        if (TargetIsVisible) return true;
        return false;
    }

    public virtual void Attack()
    {
        weapon.Attack();
    }

}
