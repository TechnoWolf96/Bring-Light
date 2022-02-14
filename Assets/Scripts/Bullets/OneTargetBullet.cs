using UnityEngine;

[System.Serializable]
public struct OneTargetBullet_Parameters : IBullet_Parameters
{

    public float speed;                         // Скорость пули
    public AttackParameters attack;             // Параметры атаки пули
}

// Пуля, поражающая одну цель
public class OneTargetBullet : Bullet
{
    protected OneTargetBullet_Parameters bulletParameters;

    public override void InstBullet(IBullet_Parameters bulletParameters, Transform shotPoint, Transform target)
    {
        base.InstBullet(bulletParameters, shotPoint, target);
        this.bulletParameters = (OneTargetBullet_Parameters)bulletParameters;
        SetDirectionAndSpeed(target, this.bulletParameters.speed);
    }



    protected override void Collision(Collider2D other)
    {
        bool crit = bulletParameters.attack.CalculateCrit();
        other.GetComponent<IDestructable>()?.GetDamage(bulletParameters.attack, shotPoint.parent, transform);
        if (!crit) Instantiate(deathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
        else CritEffect();
        Destroy(gameObject);
    }

    protected override void CritEffect()
    {
        Instantiate(critDeathEffect, transform.position, Quaternion.identity).transform.localScale *= offset;
    }
}
