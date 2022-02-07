using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Regeneration : EquipmentItem
{
    [SerializeField] protected int healPerCicle;
    [SerializeField] protected float cicleTime;
    [SerializeField] protected GameObject creatureEffect;

    protected GameObject currentCreatureEffect;

    public override void PutOff()
    {
        Destroy(currentCreatureEffect);
    }

    public override void PutOn()
    {
        currentCreatureEffect = Instantiate(creatureEffect, Player.singleton.transform);
        Effect_Regeneration regeneration = currentCreatureEffect.GetComponent<Effect_Regeneration>();
        regeneration.cicleTime = cicleTime;
        regeneration.healPerCicle = healPerCicle;
    }



}
