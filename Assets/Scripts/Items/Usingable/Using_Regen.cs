using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Using_Regen : UsingableEffectItem
{
    [SerializeField] protected int regeneration;
    [SerializeField] protected float cicleTime;


    public override void Use()
    {
        base.Use();
        StartCoroutine("Regeneration");
    }

    IEnumerator Regeneration()
    {
        while (active)
        {
            Player.singleton.health += regeneration;
            yield return new WaitForSeconds(cicleTime);
        }
    }







}
