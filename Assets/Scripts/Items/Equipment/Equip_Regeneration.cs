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
        Regeneration_Effect regeneration = currentCreatureEffect.GetComponent<Regeneration_Effect>();
        regeneration.cicleTime = cicleTime;
        regeneration.healPerCicle = healPerCicle;
    }



}
