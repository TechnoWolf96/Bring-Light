using UnityEngine;

public class ContinuousDamage_AreaEffect : AreaEffect
{
    [SerializeField] private AttackParameters _damagePerCicle;
    [SerializeField] private float _cicleTime;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (instEffect == null) return;

        var continDamageEffect = instEffect.GetComponent<ContinuousDamage_PassiveEffect>();
        continDamageEffect.cicleTime = _cicleTime;
        continDamageEffect.damagePerCicle = _damagePerCicle;

        instEffect = null;
    }

}
