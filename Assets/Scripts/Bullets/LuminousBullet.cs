using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class LuminousBullet : Bullet
{
    [SerializeField] protected float lifeTime;
    [SerializeField] protected List<Sprite> thrustedBulletSprites;
    [SerializeField] EventReference thrustSound;
    protected SpriteRenderer sprite;
    protected float timeUntilDestroy;
    protected bool collided = false;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        timeUntilDestroy = lifeTime;
    }

    private void FixedUpdate()
    {
        timeUntilDestroy -= Time.deltaTime;
    }

    private void Update()
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
        if (other.TryGetComponent<Creature>(out Creature check)) transform.SetParent(other.transform.Find("Body"));
        other.GetComponent<IDestructable>()?.GetDamage(attack, shotPoint.parent, transform);
        sprite.sprite = thrustedBulletSprites[Random.Range(0, thrustedBulletSprites.Count)];
        bulletRB.velocity = Vector3.zero;
        Library.Play3DSound(thrustSound, transform);
        collided = true;
    }
}
