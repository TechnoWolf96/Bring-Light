using UnityEngine;

public class Using_Regen : UsingableEffectItem
{
    [SerializeField] protected int healPerCicle;
    [SerializeField] protected float cicleTime;


    public override void Use()
    {
        base.Use();
        Regeneration_Effect effect = currentCreatureEffect.GetComponent<Regeneration_Effect>();
        effect.healPerCicle = healPerCicle;
        effect.cicleTime = cicleTime;

    }








}
