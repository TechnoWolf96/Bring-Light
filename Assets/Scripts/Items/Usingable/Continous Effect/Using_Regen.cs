using UnityEngine;

public class Using_Regen : UsingableEffectItem
{
    [SerializeField] protected int healPerCicle;
    [SerializeField] protected float cicleTime;


    public override void Use()
    {
        base.Use();
        Regeneration_PassiveEffect effect = currentPassiveEffect.GetComponent<Regeneration_PassiveEffect>();
        effect.healPerCicle = healPerCicle;
        effect.cicleTime = cicleTime;

    }








}
