using UnityEngine;

public class ExplosingBullet : Bullet
{
    [SerializeField] protected float _explotionRadius;
    public float explotionRadius { get => _explotionRadius; set => _explotionRadius = value; }

    [SerializeField] protected LayerMask _damagableLayers;
    public LayerMask damagableLayers { get => _damagableLayers; set => _damagableLayers = value; }


    protected override void Collision(Collider2D other)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explotionRadius, damagableLayers);
        bool crit = attack.CalculateCrit();
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= explotionRadius * offset;
        else Crit();
        foreach (var item in colliders)
        {
            if (shotPoint.GetComponentInParent<Transform>().gameObject.layer != item.gameObject.layer)
                item.GetComponent<IDestructable>()?.GetDamage(attack, shotPoint.parent, transform);
        }
        Destroy(gameObject);
    }


}
