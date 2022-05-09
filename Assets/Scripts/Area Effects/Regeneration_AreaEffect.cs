using UnityEngine;

public class Regeneration_AreaEffect : AreaEffect
{
    [SerializeField] private int _healPerCicle;
    [SerializeField] private float _cicleTime;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (instEffect == null) return;

        var regenEffect = instEffect.GetComponent<Regeneration_PassiveEffect>();
        regenEffect.cicleTime = _cicleTime;
        regenEffect.healPerCicle = _healPerCicle;

        instEffect = null;
    }

}
