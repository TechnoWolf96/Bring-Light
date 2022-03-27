using UnityEngine;

public class SelfHealingSpell : SpellNPC
{
    [SerializeField] int heal;
    [SerializeField] int maxPriority;
    protected HealthBar HealthBar;

    protected override void Start()
    {
        base.Start();
        HealthBar = GetComponentInParent<HealthBar>(); 
    }

    public override void Activate() => spellcaster.health += heal;

    public override void CalculatePriority()
    {
        if (currentRechargeTime > 0) { priority = 0; return; }
        priority = maxPriority - (spellcaster.health * maxPriority / spellcaster.maxHealth);
    }
}
