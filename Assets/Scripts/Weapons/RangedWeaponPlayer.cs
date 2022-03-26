using UnityEngine;

public class RangedWeaponPlayer : PlayerWeapon
{
    [HideInInspector] public GameObject bullet;

    public override void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform.parent, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
