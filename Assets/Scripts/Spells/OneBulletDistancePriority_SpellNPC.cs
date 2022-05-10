using UnityEngine;

public class OneBulletDistancePriority_SpellNPC : OneBullet_Spell
{
    RangedSpellcasterFSM rangedSpellcaster;


    protected override void Start()
    {
        base.Start();
        rangedSpellcaster = (RangedSpellcasterFSM)spellcaster;
    }
    public override void CalculatePriority()
    {
        if (currentRechargeTime > 0) { priority = 0; return; }
        if (CheckTargetIsVisible() && Vector2.Distance
            (transform.position, spellcaster.follow.transform.position) < rangedSpellcaster.randomStopDistance)
            priority = priorityIfTargetIsVisible;
        else priority = 0;
        
    }
}
