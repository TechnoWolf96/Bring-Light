using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float speed;
    protected AttackParameters attack;
    protected Rigidbody2D rb;
    protected Transform carrier;                  // Transform носителя оружия, из которого вылетела пуля
    protected LayerMask layer;                    // Слой объектов, по которому будет проходить атака

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // attack.damage = (int)(attack.damage*carrier.GetComponent<Creature>().xDamageGain);
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.velocity = difference.normalized * speed;
    }
    


}
