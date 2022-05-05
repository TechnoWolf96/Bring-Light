using System.Collections;
using UnityEngine;

public class ContinuousDamage_PassiveEffect : PassiveEffect
{
    [HideInInspector] public AttackParameters damagePerCicle;
    [HideInInspector] public float cicleTime;

    private void Start()
    {
        creature = GetComponentInParent<Creature>();
        StartCoroutine(ContinuousDamage());
    }

    IEnumerator ContinuousDamage()
    {
        while (true)
        {
            creature.health -= creature.CalculateRealDamage(damagePerCicle);
            yield return new WaitForSeconds(cicleTime);
        }
    }

}
