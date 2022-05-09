using UnityEngine;

public interface IBullet_Parameters{}

public abstract class Bullet : MonoBehaviour
{
    public delegate void OnCrit();
    public event OnCrit onCrit; // Можно добавить крит, добавив скрипт с критом в инспекторе

    public AttackParameters attack;

    protected Rigidbody2D bulletRB;
    protected Transform shotPoint;

    [SerializeField] protected float _speed;
    public float speed { get =>_speed; set => _speed = value; }

    [SerializeField] protected LayerMask collisionLayer;
    [SerializeField] protected GameObject deathEffect;    
    [SerializeField] protected float offset = 1f;  // Регулировка размера эффекта взрыва


    public virtual void InstBullet(Transform shotPoint, Vector3 target, Transform targetTransform = null)
    {
        bulletRB = GetComponent<Rigidbody2D>();
        this.shotPoint = shotPoint;
        SetDirectionAndSpeed(target);
    }

    protected void SetDirectionAndSpeed(Vector3 target)
    {
        // Задание направления и скорости пули
        Vector2 direction = target - shotPoint.position;
        bulletRB.velocity = direction.normalized * speed;
        // Поворот пули в сторону цели
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
            Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg - 180);
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (Library.CompareLayer(other.gameObject.layer, collisionLayer)) Collision(other);
    }

    protected abstract void Collision(Collider2D other);

    protected void Crit()
    {
        onCrit?.Invoke();
    }

}
