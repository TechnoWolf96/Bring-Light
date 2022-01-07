using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Ranged weapon:")]
    [SerializeField] protected Bullet_Parameters bulletParameters;
    [SerializeField] protected GameObject bullet;

    protected SmartRangedAttackPosition stalker;

    protected override void Start()
    {
        base.Start();
        stalker = GetComponentInParent<SmartRangedAttackPosition>();
    }

    public override void Attack()
    {
        if (stalker.follow != null)
            Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
                InstBullet(bulletParameters, transform.parent, stalker.follow.transform);

    }
}
