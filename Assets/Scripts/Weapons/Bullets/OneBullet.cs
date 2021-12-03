using UnityEngine;


[System.Serializable]
public struct OneBullet_Parameters
{
    public float speed;                         // Скорость пули
    public AttackParameters attack;             // Параметры атаки пули
    [HideInInspector] public Transform carrier; // Transform носителя оружия, из которого вылетела пуля
    public LayerMask collisionLayer;             // Слой объектов, с которыми может сталкиваться пуля
    [HideInInspector] public Transform target;  // Куда летит пуля
}

// Пуля, поражающая одну цель
public class OneBullet : MonoBehaviour
{
    private OneBullet_Parameters bulletParameters;
    private Rigidbody2D rb;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject critDeathEffect;
    [SerializeField] private float offset;
    [SerializeField] private float radiusDetect;   // Радиус поставить равным радиусу коллайдера
    public virtual void InstBullet(OneBullet_Parameters bulletParameters)
    {
        rb = GetComponent<Rigidbody2D>();
        this.bulletParameters = bulletParameters;
        this.bulletParameters.collisionLayer = bulletParameters.collisionLayer;
        // Задание направления и скорости пули
        Vector2 direction = this.bulletParameters.target.position - this.bulletParameters.carrier.position;
        rb.velocity = direction.normalized * this.bulletParameters.speed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, radiusDetect, bulletParameters.collisionLayer);
        if (collision != null)
        {
            Damage(collision);
            Destroy(gameObject);
        }
    }


    protected virtual void Damage(Collider2D collider)
    {
        bool crit = bulletParameters.attack.SetCrit();
        collider.GetComponent<Creature>()?.GetDamage(bulletParameters.attack, bulletParameters.carrier, transform);
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
        else Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
    }


    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDetect);
    }



}
