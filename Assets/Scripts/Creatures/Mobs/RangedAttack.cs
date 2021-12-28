using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : SmartRangedAttackPosition_NotRelease
{
    [Header("Ranged Attact: One Bullet")]
    public Bullet_Parameters bulletParameters;
    public GameObject bulletPrefab;
    public float recharge;

    private float currentRecharge;

    protected override void Start()
    {
        base.Start();
        currentRecharge = recharge;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        currentRecharge -= Time.deltaTime;
    }
    protected bool Recharged()
    {
        return currentRecharge < 0;
    }

    protected override void Attack()
    {
        if (Recharged())
        {
            currentRecharge = recharge;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity).
                GetComponent<Bullet>().InstBullet(bulletParameters, transform, follow);
        }
    }


}
