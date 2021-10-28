using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Bullet
{   
    public GameObject explotionPrefab;       // Префаб взрыва
    public float offset;                     // Регулировка величины спрайта взрыва

    private float radius;                    // Радиус взрыва
    

   

    public void InstBullet(AttackParameters attack, float speed, Transform carrier, 
        LayerMask layer, float radius)
    {
        this.attack = attack;
        this.carrier = carrier;
        this.speed = speed;
        this.layer = layer;
        this.radius = radius;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall")) Explotion();
    }

    private void Explotion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        foreach (var item in colliders)
        {
            item.GetComponent<Creature>().GetDamage(attack, carrier.transform, transform);
        }
        Transform size = Instantiate(explotionPrefab, transform.position, Quaternion.identity).GetComponent<Transform>();
        Vector2 vsize = new Vector2(size.localScale.x, size.localScale.y);
        size.localScale = vsize*radius*offset;
        Destroy(gameObject);
    }

}
