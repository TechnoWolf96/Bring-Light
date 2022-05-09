using UnityEngine;

public class HealDamage_Bullet : Bullet
{
    public int heal;
    [SerializeField] private LayerMask _healLayer;
    [SerializeField] private LayerMask _damageLayer;

    protected override void Collision(Collider2D other)
    {
        if (other.CompareTag("Player")) return;
        if (Library.CompareLayer(other.gameObject.layer, _healLayer) )
        {
            
            other.GetComponent<Creature>().health += heal;
            bool crit = attack.CalculateCrit();
            if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
            else Crit();
            Destroy(gameObject);
        }
        else if (Library.CompareLayer(other.gameObject.layer, _damageLayer))
        {
            bool crit = attack.CalculateCrit();
            if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
            else Crit();
            other.GetComponent<IDestructable>()?.GetDamage(attack, shotPoint.parent, transform);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
            Destroy(gameObject);
        }

    }




}
