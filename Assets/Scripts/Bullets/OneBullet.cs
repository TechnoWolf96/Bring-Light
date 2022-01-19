using UnityEngine;



// Пуля, поражающая одну цель
public class OneBullet : Bullet
{

    protected override void Collision(Collider2D other)
    {
        bool crit = bulletParameters.attack.SetCrit();
        other.GetComponent<Creature>()?.GetDamage(bulletParameters.attack, shooter, transform);
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
        else CritEffect();

        Destroy(gameObject);
    }

    protected override void CritEffect()
    {
        Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
    }
}
