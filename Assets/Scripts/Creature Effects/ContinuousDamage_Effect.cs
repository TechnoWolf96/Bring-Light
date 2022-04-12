using System.Collections;
using UnityEngine;

public class ContinuousDamage_Effect : MonoBehaviour
{
    private Creature _creature;
    public int damagePerCicle;
    public float cicleTime;

    private void Start()
    {
        _creature = GetComponentInParent<Creature>();
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        while (true)
        {
            _creature.health -= damagePerCicle;
            yield return new WaitForSeconds(cicleTime);
        }
    }

}
