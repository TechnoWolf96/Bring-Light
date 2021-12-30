using UnityEngine;

public class SelfHealingSpell : NPCSpell
{
    [Header("Self healing:")]
    public int heal;
    public GameObject particles;
    public int maxPriority;

    protected Creature creature;
    protected HealthBar HealthBar;

    protected ParticleSystem instParticles;

    public override void BeginCast()
    {
        instParticles = Instantiate(particles, transform).GetComponent<ParticleSystem>();
    }
    public override void StopCast()
    {
        //instParticles?.Stop();
    }

    protected virtual void Start()
    {
        creature = GetComponentInParent<Creature>();
        HealthBar = GetComponentInParent<HealthBar>();
    }

    public override void Activate()
    {
        creature.health += heal;
        if (creature.health > creature.maxHealth)
            creature.health = creature.maxHealth;
        HealthBar.ShowBar();
    }

    public override void CalculatePriority()
    {
        priority = maxPriority - (creature.health * maxPriority / creature.maxHealth);
    }
}
