using System.Collections;
using UnityEngine;

public class UsingableEffectItem : UsingableItem
{
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected GameObject particles;
    [SerializeField] protected GameObject effectIcon;

    protected float timeToFinish;
    protected bool active = false;

    protected const float timeUntilDestroyParticles = 3f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        timeToFinish -= Time.deltaTime;
    }

    protected override void Update()
    {
        base.Update();
        if (timeToFinish < 0f && active) active = false;
    }


    public override void Use()
    {
        base.Use();
        active = true;
        timeToFinish = duration;
        EffectIconPanel.sigleton.AddEffect(effectIcon, duration);
        var mainParticle = Instantiate(particles, Player.singleton.transform).GetComponent<ParticleSystem>().main;
        mainParticle.duration = duration;
    }

}
