using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack_OneBullet : SmartRangedAttackPosition
{
    [Header("Ranged Attact: One Bullet")]
    public OneBullet_Parameters bulletParameters;
    public GameObject bulletPrefab;
    public float recharge;

    private float currentRecharge;

    protected override void Start()
    {
        base.Start();
        bulletParameters.carrier = transform;
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

    protected override void RangedAttack()
    {
        if (Recharged())
        {
            currentRecharge = recharge;
            bulletParameters.target = follow;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity).
                GetComponent<OneBullet>().InstBullet(bulletParameters);
        }
    }


}
