using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Weapon:")]
    public AttackParameters attack; // Параметры атаки
    public float radiusAttack;


    public override void Attack()
    {
        Collider2D[] enemy_col = Physics2D.OverlapCircleAll(transform.position, radiusAttack, layer);
        bool crit = attack.SetCrit();
        foreach (var item in enemy_col)
        {
            item.GetComponent<Creature>().GetDamage(attack, creaturePos);
        }
        if (crit) print("Crit!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);
    }



}
