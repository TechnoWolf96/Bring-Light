using UnityEngine;

public class OneBullet_Spell : SpellNPC
{
    [SerializeField] protected int priorityIfTargetIsVisible;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject particles;

    protected ParticleSystem instParticles;
    protected SpellcasterFSM spellcaster;

    protected void Start()
    {
        spellcaster = GetComponentInParent<SpellcasterFSM>();
    }

    public override void Activate()
    {
        if (spellcaster.follow == null) return;
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform, spellcaster.followBodyCenter.position);
    }

    public override void CalculatePriority()
    {
        if (currentRechargeTime > 0) { priority = 0; return; }
        if (spellcaster.anim.GetBool("CanAttack")) priority = priorityIfTargetIsVisible;
        else priority = 0;
    }

    public override void BeginCast()
    {
        if (particles != null)
            instParticles = Instantiate(particles, transform).GetComponent<ParticleSystem>();
        spellcaster.anim.SetFloat("SpeedCast", speedCast);
        currentRechargeTime = rechargeTime;
    }
        

    public override void BreakCast() => instParticles?.Stop();
}
