using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    public GameObject bombBulletPrefab; // ֿנופאב כועÿשוי במלבû
    public ExplosingBullet_Parameters bulletParameters;

    private void Update()
    {
        if (Input.GetMouseButton(0) && IsRecharged()) Attack();
    }
    protected override void Attack()
    {
        rechargeTime = recharge;
        GameObject inst = Instantiate(bombBulletPrefab, playerPos.transform.position, Quaternion.identity);
        UpdateBulletParameters();
        inst.GetComponent<ExplosingBullet>().InstBullet(bulletParameters);
    }
    private void UpdateBulletParameters()
    {
        bulletParameters.carrier = playerPos;
        bulletParameters.target = new GameObject().transform;
        bulletParameters.target.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
