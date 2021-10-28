using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    public GameObject bombBulletPrefab; // Префаб летящей бомбы
    public float speedBullet; // Скорость полета бомбы
    public float radiusExplosion; // Радиус взрыва

    private void Update()
    {
        if (Input.GetMouseButton(0) && IsRecharged()) Attack();
    }




    protected override void Attack()
    {
        rechargeTime = recharge;
        if (playerPos == null) print("Bad");
        GameObject inst = Instantiate(bombBulletPrefab, playerPos.transform.position, Quaternion.identity);
        inst.GetComponent<BombBullet>().InstBullet(attack, speedBullet, playerPos, layer, radiusExplosion);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusExplosion);
    }
}
