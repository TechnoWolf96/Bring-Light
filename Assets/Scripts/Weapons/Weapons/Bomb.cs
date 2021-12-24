using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon_notRelease
{
    public GameObject bombBulletPrefab; // ֿנופאב כועÿשוי במלבû
    public Bullet_Parameters bulletParameters;

    private void Update()
    {
        if (Input.GetMouseButton(0) && IsRecharged()) Attack();
    }
    protected override void Attack()
    {
        rechargeTime = recharge;
        GameObject inst = Instantiate(bombBulletPrefab, playerPos.transform.position, Quaternion.identity);

        Transform newTarget = new GameObject().transform;
        newTarget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        inst.GetComponent<ExplosingBullet>().InstBullet(bulletParameters, playerPos, newTarget);
    }
}
