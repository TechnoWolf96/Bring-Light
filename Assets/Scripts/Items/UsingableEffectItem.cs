using UnityEngine;

public class UsingableEffectItem : UsingableItem
{
    [SerializeField] protected float duration = 0f;
    [SerializeField] protected GameObject effectIcon;
    [SerializeField] protected GameObject passiveEffect;

    protected GameObject currentPassiveEffect;


    public override void Use()
    {
        base.Use();
        EffectIconPanel.sigleton.AddEffect(effectIcon, duration);
        if (passiveEffect != null)
        {
            var mainParticle = passiveEffect.GetComponent<ParticleSystem>().main;
            mainParticle.duration = duration;
            Instantiate(passiveEffect, Player.singleton.transform);
        }
        currentPassiveEffect = Instantiate(passiveEffect, Player.singleton.transform);
        Timer_PassiveEffect timer = currentPassiveEffect.GetComponent<Timer_PassiveEffect>();
        timer.duration = duration;
    }

}
