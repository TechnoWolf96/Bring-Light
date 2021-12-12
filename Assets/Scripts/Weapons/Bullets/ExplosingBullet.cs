using UnityEngine;



public class ExplosingBullet : Bullet
{
    protected override void Collision(Collider2D other)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bulletParameters.radius, bulletParameters.DamagedExplosionLayers);
        bool crit = bulletParameters.attack.SetCrit();
        foreach (var item in colliders)
        {
            item.GetComponent<Creature_NotRelease>().GetDamage(bulletParameters.attack, shooter, transform);
        }
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= bulletParameters.radius * offset;
        else Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= bulletParameters.radius * offset;

        Destroy(gameObject);
    }

}
