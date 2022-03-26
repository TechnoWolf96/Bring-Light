using UnityEngine;

public class MeleeWeaponPlayer : PlayerWeapon
{
    [SerializeField] protected AttackParameters attack;
    [SerializeField] protected float radiusAttack;

    public override void Attack()
    {
        Collider2D[] enemy_col = Physics2D.OverlapCircleAll(transform.position, radiusAttack, layerAttack);
        foreach (var item in enemy_col)
        {
            item.GetComponent<IDestructable>()?.GetDamage(attack, owner.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);

    }

}
