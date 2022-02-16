using UnityEngine;


public class MeleeWeapon : Weapon
{
    [Header("Melee Weapon:")]
    [SerializeField] protected AttackParameters attack; // Параметры атаки
    [SerializeField] protected float radiusAttack;
    
    

    public override void Attack()
    {
        Collider2D[] enemy_col = Physics2D.OverlapCircleAll(transform.position, radiusAttack, layer);
        bool crit = attack.CalculateCrit();
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
