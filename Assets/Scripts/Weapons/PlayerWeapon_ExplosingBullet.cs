
using UnityEngine;

public class PlayerWeapon_ExplosingBullet : Weapon
{
    [SerializeField] protected Explosing_Bullet_Parameters bulletParameters;
    [SerializeField] protected GameObject bullet;

    public override void Attack()
    {
        GameObject mousePosition = new GameObject();
        mousePosition.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(bulletParameters, transform.parent, mousePosition.transform);
        Destroy(mousePosition);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletParameters.radius);
    }

}
