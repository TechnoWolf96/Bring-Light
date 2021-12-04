using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Bullet_Parameters
{
    [Header("Universal bullet:")]
    public float speed;                         // Скорость пули
    public AttackParameters attack;             // Параметры атаки пули

    [Header("Explosing bullet:")]
    public float radius;                        // Радиус взрыва
    public LayerMask DamagedExplosionLayers;    // Слои, которые будут получать урон от взрыва
}



public abstract class Bullet : MonoBehaviour
{
    protected Bullet_Parameters bulletParameters;
    protected Rigidbody2D rb;
    protected Transform target;    // Куда летит пуля
    protected Transform shooter;   // Transform стрелка

    [SerializeField] protected GameObject deathEffect;        // Эффект взрыва
    [SerializeField] protected GameObject critDeathEffect;    // Эффект взрыва при крите
    [SerializeField] protected float offset;                  // Регулировка размера эффекта взрыва


    // Задание параметров пули при ее создании
    public virtual void InstBullet(Bullet_Parameters bulletParameters, Transform shooter, Transform target)
    {
        rb = GetComponent<Rigidbody2D>();
        this.bulletParameters = bulletParameters;
        this.shooter = shooter;
        this.target = target;
        // Задание направления и скорости пули
        Vector2 direction = this.target.position - this.shooter.position;
        rb.velocity = direction.normalized * this.bulletParameters.speed;

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == shooter.gameObject.layer)
            return; // Дружественный огонь запрещен
        Collision(other);
    }

    protected virtual void Collision(Collider2D other) { }



}
