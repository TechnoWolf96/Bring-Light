
public class RangedAttack : AbleToSeekRangedAttackPosition, IAttackWithWeapon
{
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
        // Цель видна? Да - атакуем
        if (targetIsVisible && currentWeapon.IsRecharged())
        {
            currentWeapon.BeginAttack();
            anim.SetTrigger("Attack");
        }
    }


    public virtual void AttackMoment()
    {
        currentWeapon.Attack();
    }

}
