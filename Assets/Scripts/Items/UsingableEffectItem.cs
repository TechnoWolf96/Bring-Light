using UnityEngine;

public class UsingableEffectItem : UsingableItem
{
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected GameObject particles;
    [SerializeField] protected GameObject effectIcon;
    [SerializeField] protected GameObject creatureEffect;

    protected GameObject currentCreatureEffect;


    public override void Use()
    {
        base.Use();
        EffectIconPanel.sigleton.AddEffect(effectIcon, duration);
        if (particles != null)
        {
            var mainParticle = particles.GetComponent<ParticleSystem>().main;
            mainParticle.duration = duration;
            Instantiate(particles, Player.singleton.transform);
        }
        currentCreatureEffect = Instantiate(creatureEffect, Player.singleton.transform);
        Effect_Timer timer = currentCreatureEffect.GetComponent<Effect_Timer>();
        timer.duration = duration;
    }

}
