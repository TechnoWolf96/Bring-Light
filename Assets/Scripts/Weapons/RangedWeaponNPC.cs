using UnityEngine;

public class RangedWeaponNPC : Weapon
{
    [SerializeField] protected GameObject bullet;
    protected Stalker stalker;

    protected override void Start()
    {
        base.Start();
        stalker = GetComponentInParent<Stalker>();
    }

    public override void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform.parent, stalker.followBodyCenter.position);
    }
}
