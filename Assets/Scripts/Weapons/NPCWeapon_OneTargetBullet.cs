using UnityEngine;

public class NPCWeapon_OneTargetBullet : Weapon
{
    [Header("(NPC) Ranged weapon:")]
    [SerializeField] protected OneTargetBullet_Parameters bulletParameters;
    [SerializeField] protected GameObject bullet;

    protected AbleToSeekRangedAttackPosition stalker;

    protected override void Start()
    {
        base.Start();
        stalker = GetComponentInParent<AbleToSeekRangedAttackPosition>();
    }

    public override void Attack()
    {
        if (stalker.follow != null)
            Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
                InstBullet(bulletParameters, transform.parent, stalker.followBodyCenter);

    }
}
