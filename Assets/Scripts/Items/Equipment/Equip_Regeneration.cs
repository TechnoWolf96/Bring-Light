using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Regeneration : EquipmentItem
{
    [SerializeField] protected int regeneration;
    [SerializeField] protected float cicleTime;

    public override void PutOff()
    {
        print("Off");
        StopCoroutine("Regeneration");
    }

    public override void PutOn()
    {
        StartCoroutine("Regeneration");
    }

    IEnumerator Regeneration()
    {
        while(true)
        {
            player.health += regeneration;
            yield return new WaitForSeconds(cicleTime);
        }
        
    }



}
