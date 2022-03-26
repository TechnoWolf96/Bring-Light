using UnityEngine;

public class SelfHealingSpell : SpellNPC
{
    [SerializeField] int heal;
    [SerializeField] int maxPriority;
    [SerializeField] GameObject particles;

    protected Creature spellcaster;
    protected ParticleSystem instParticles;
    protected HealthBar HealthBar;

    public override void BeginCast()
    {
        instParticles = Instantiate(particles, transform).GetComponent<ParticleSystem>();
        spellcaster.anim.SetFloat("SpeedCast", speedCast);
        currentRechargeTime = rechargeTime;
    }
        


    private void Start()
    {
        HealthBar = GetComponentInParent<HealthBar>();
        spellcaster = GetComponentInParent<Creature>();  
    }

    public override void Activate() => spellcaster.health += heal;

    public override void CalculatePriority()
    {
        if (currentRechargeTime > 0) { priority = 0; return; }
        priority = maxPriority - (spellcaster.health * maxPriority / spellcaster.maxHealth);
    }

    public override void BreakCast() => instParticles.Stop();
}
