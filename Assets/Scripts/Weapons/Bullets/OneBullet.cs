using UnityEngine;



// Пуля, поражающая одну цель
public class OneBullet : Bullet
{

    protected override void Collision(Collider2D other)
    {
        bool crit = bulletParameters.attack.SetCrit();
        other.GetComponent<Creature_NotRelease>()?.GetDamage(bulletParameters.attack, shooter, transform);
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
        else Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;

        Destroy(gameObject);
    }



}
