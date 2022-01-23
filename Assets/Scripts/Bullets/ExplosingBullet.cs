using UnityEngine;

[System.Serializable]
public struct Explosing_Bullet_Parameters : IBullet_Parameters
{
    public float speed;                         // Скорость пули
    public AttackParameters attack;             // Параметры атаки пули
    public float radius;                        // Радиус взрыва
    public LayerMask DamagedExplosionLayers;    // Слои, которые будут получать урон от взрыва
}



public class ExplosingBullet : Bullet
{
    protected Explosing_Bullet_Parameters bulletParameters;

    public override void InstBullet(IBullet_Parameters bulletParameters, Transform shotPoint, Transform target)
    {
        base.InstBullet(bulletParameters, shotPoint, target);
        this.bulletParameters = (Explosing_Bullet_Parameters)bulletParameters;
        SetDirectionAndSpeed(target, this.bulletParameters.speed);
    }


    protected override void Collision(Collider2D other)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bulletParameters.radius, bulletParameters.DamagedExplosionLayers);
        bool crit = bulletParameters.attack.CalculateCrit();
        foreach (var item in colliders)
        {
            item.GetComponent<IDestructable>()?.GetDamage(bulletParameters.attack, shotPoint.parent, transform);
        }
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= bulletParameters.radius * offset;
        else Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= bulletParameters.radius * offset;

        Destroy(gameObject);
    }


}
