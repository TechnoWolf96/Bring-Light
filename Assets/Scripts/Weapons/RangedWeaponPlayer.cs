using UnityEngine;

public class RangedWeaponPlayer : Weapon
{
    [SerializeField] protected GameObject _bullet;
    public GameObject bullet { get; set; }
    protected Stalker stalker;

    protected override void Start()
    {
        base.Start();
        stalker = GetComponentInParent<Stalker>();
    }

    public override void Attack()
    {
        Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
                InstBullet(transform.parent, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
