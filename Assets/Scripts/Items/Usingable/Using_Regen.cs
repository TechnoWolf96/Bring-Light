using UnityEngine;

public class Using_Regen : UsingableEffectItem
{
    [SerializeField] protected int healPerCicle;
    [SerializeField] protected float cicleTime;


    public override void Use()
    {
        base.Use();
        Effect_Regeneration effect = currentCreatureEffect.GetComponent<Effect_Regeneration>();
        effect.healPerCicle = healPerCicle;
        effect.cicleTime = cicleTime;

    }








}
