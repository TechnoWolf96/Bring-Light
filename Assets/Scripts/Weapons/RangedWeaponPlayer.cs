using UnityEngine;

public class RangedWeaponPlayer : Weapon
{
    [HideInInspector] public GameObject bullet;

    public override void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform.parent, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
