using System.Collections;
using UnityEngine;

public class Regeneration_PassiveEffect : PassiveEffect
{
    public int healPerCicle { get; set; }
    public float cicleTime { get; set; }

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
