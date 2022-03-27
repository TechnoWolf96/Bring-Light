using UnityEngine;

public class RangedWeaponNPC : Weapon
{
    [SerializeField] protected GameObject bullet;
    protected RangedAttackFSM shooter;

    protected override void Start()
    {
        base.Start();
        shooter = GetComponentInParent<RangedAttackFSM>();
    }

    public override void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform.parent, shooter.follow.bodyCenter.position);
    }
}
