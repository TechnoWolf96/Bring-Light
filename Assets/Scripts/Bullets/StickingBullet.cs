using FMODUnity;
using UnityEngine;

public class StickingBullet : Bullet
{
    [SerializeField] protected float lifeTime;
    [SerializeField] protected Sprite thrustedBulletSprite;
    [SerializeField] protected EventReference thrustSound;
    protected SpriteRenderer sprite;
    protected float timeUntilDestroy;
    protected bool collided = false;

    protected virtual void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        timeUntilDestroy = lifeTime;
    }

    protected virtual void FixedUpdate()
    {
        timeUntilDestroy -= Time.deltaTime;
    }

    protected virtual void Update()
    {
        if (timeUntilDestroy < 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override void Collision(Collider2D other)
    {
        if (collided) return;
        if (attack.CalculateCrit()) Crit();
        if (other.TryGetComponent(out Creature check)) transform.SetParent(other.transform.Find("Body"));
        other.GetComponent<IDestructable>()?.GetDamage(attack, shotPoint.parent, transform);
        sprite.sprite = thrustedBulletSprite;
        bulletRB.velocity = Vector3.zero;
        Library.Play3DSound(thrustSound, transform);
        collided = true;
    }
}
