using UnityEngine;

public class Equip_Regeneration : EquipmentItem
{
    [SerializeField] protected int healPerCicle;
    [SerializeField] protected float cicleTime;
    [SerializeField] protected GameObject passiveEffect;

    protected GameObject currentPassiveEffect;

    public override void PutOff()
    {
        Destroy(currentPassiveEffect);
    }

    public override void PutOn()
    {
        currentPassiveEffect = Instantiate(passiveEffect, Player.singleton.transform);
        Regeneration_PassiveEffect regeneration = currentPassiveEffect.GetComponent<Regeneration_PassiveEffect>();
        regeneration.cicleTime = cicleTime;
        regeneration.healPerCicle = healPerCicle;
    }



}
