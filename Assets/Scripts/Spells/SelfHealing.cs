using UnityEngine;

public class SelfHealing : NPCSpell
{
    [Header("Self healing:")]
    public int heal;
    public GameObject particles;
    protected Creature creature;


    protected override void ApplyVisualEffects()
    {
        base.ApplyVisualEffects();
        Instantiate(particles, transform);
    }


    protected override void Start()
    {
        base.Start();
        creature = GetComponentInParent<Creature>();
    }

    protected override void Cast()
    {
        base.Cast();
        creature.health += heal;
        if (creature.health > creature.maxHealth)
            creature.health = creature.maxHealth;
    }
}
