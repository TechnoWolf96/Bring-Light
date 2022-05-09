using System.Collections;
using UnityEngine;

public class Regeneration_PassiveEffect : PassiveEffect
{
    [HideInInspector] public int healPerCicle;
    [HideInInspector] public float cicleTime;

    private void Start()
    {
        creature = GetComponentInParent<Creature>();
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        while (true)
        {
            creature.health += healPerCicle;
            yield return new WaitForSeconds(cicleTime);
        }
    }

}
