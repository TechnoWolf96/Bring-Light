using UnityEngine;

public class SquadHeal_SpellNPC : SpellNPC
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected int heal;
    [SerializeField] protected int maxPriority;
    [SerializeField] protected float checkHealRadius;
    [SerializeField] protected LayerMask healLayer;
    protected Transform targetForHeal;
    

    public override void Activate()
    {
        HealBullet healBullet = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<HealBullet>();
        healBullet.InstBullet(transform, targetForHeal.position, targetForHeal);
        healBullet.heal = heal;
    }
    public override void CalculatePriority()
    {
        if (currentRechargeTime > 0) {priority = 0; return;}
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkHealRadius, healLayer);
        float minfullHealth = 1f;
        foreach (var collider in colliders)
        {
            Creature creature = collider.GetComponent<Creature>();
            float creatureFullHealth = (float)creature.health / creature.maxHealth;

            if (creatureFullHealth < minfullHealth)
            {
                targetForHeal = creature.bodyCenter;
                minfullHealth = creatureFullHealth;
                priority = maxPriority - (maxPriority * creature.health / creature.maxHealth);
            }
        }
        
    }
}
