using System.Collections;
using UnityEngine;

public class Effect_Regeneration : MonoBehaviour
{
    private Creature _creature;
    public int healPerCicle { get; set; }
    public float cicleTime { get; set; }

    private void Start()
    {
        _creature = GetComponentInParent<Creature>();
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        while (true)
        {
            _creature.health += healPerCicle;
            yield return new WaitForSeconds(cicleTime);
        }
    }

}
