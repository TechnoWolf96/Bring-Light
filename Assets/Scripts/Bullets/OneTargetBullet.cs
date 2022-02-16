using UnityEngine;


// Пуля, поражающая одну цель
public class OneTargetBullet : Bullet
{
    protected override void Collision(Collider2D other)
    {
        bool crit = attack.CalculateCrit();
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
        else Crit();
        other.GetComponent<IDestructable>()?.GetDamage(attack, shotPoint.parent, transform);
        Destroy(gameObject);
    }

}
