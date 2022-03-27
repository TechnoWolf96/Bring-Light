using UnityEngine;

public class ThroughBullet : Bullet
{
    protected override void Collision(Collider2D other)
    {
        bool crit = attack.CalculateCrit();

        if (other.TryGetComponent(out Creature creature))
        {
            creature.GetDamage(attack, shotPoint.parent, transform);
        }
        else
        {
            if (other.TryGetComponent(out IDestructable destruct))
                destruct.GetDamage(attack, shotPoint.parent, transform);
            Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
            Destroy(gameObject);
        }
            
    }

}
