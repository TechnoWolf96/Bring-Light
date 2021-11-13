using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct OneBullet_Parameters
{
    public float speed;                         // Скорость пули
    public AttackParameters attack;             // Параметры атаки пули
    public Transform carrier;                   // Transform носителя оружия, из которого вылетела пуля
    public LayerMask layer;                     // Слой объектов, по которому будет проходить атака
    public Transform target;                    // Куда летит пуля

}

// Пуля, поражающая одну цель
public class OneBullet : MonoBehaviour
{
    public OneBullet_Parameters bulletParameters;
    private Rigidbody2D rb;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject critDeathEffect;
    [SerializeField] private float offset;
    public virtual void InstBullet(OneBullet_Parameters bulletParameters)
    {
        rb = GetComponent<Rigidbody2D>();
        this.bulletParameters = bulletParameters;
        // Задание направления и скорости пули
        Vector2 difference = this.bulletParameters.target.position - this.bulletParameters.carrier.position;
        rb.velocity = difference.normalized * this.bulletParameters.speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) Damage(other);
        if (other.CompareTag("Wall"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
            Destroy(gameObject);
        }
    }


    protected virtual void Damage(Collider2D collider)
    {
        bool crit = bulletParameters.attack.SetCrit();
        collider.GetComponent<Creature>().GetDamage(bulletParameters.attack, bulletParameters.carrier, transform);
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity);
        else Instantiate(critDeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
    protected virtual void Explotion()
    {
        /*
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bp.radius, bp.layer);
        bool crit = bp.attack.SetCrit();
        foreach (var item in colliders)
        {
            item.GetComponent<Creature>().GetDamage(attack, carrier.transform, transform);
        }
        Transform size = Instantiate(explotionPrefab, transform.position, Quaternion.identity).GetComponent<Transform>();
        Vector2 vsize = new Vector2(size.localScale.x, size.localScale.y);
        if (crit) size.GetComponent<SpriteRenderer>().color = Color.red;
        size.localScale = vsize * radius * offset;
        Destroy(gameObject);*/
    }





}
