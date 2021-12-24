using UnityEngine;


public interface IAttackWithWeapon
{
    public void ChangeWeapon(Weapon newWeapon);
    public void Attack();
}



public abstract class Weapon : MonoBehaviour
{
    
    [Header("Weapon:")]
    public float rechargeTime;              // Перезарядка
    public RuntimeAnimatorController animController;
    public LayerMask layer;         // Слой объектов, по которому будет проходить атака

    protected Transform creaturePos;          // Позиция существа, держащего оружие
    protected Creature creature;            // Скрипт существа игрока, держащего оружие
    protected float currentRechargeTime;           // Текущее время до перезарядки

    public abstract void Attack(); // Атака

    protected virtual void Start()
    {
        currentRechargeTime = rechargeTime;
        creaturePos = transform.parent;
        creature = GetComponentInParent<Creature>();
    }

    protected virtual void FixedUpdate() // Уменьшение текущего времени до перезарядки
    {
        currentRechargeTime -= Time.deltaTime;
    }

    public bool IsRecharged()
    {
        return currentRechargeTime < 0;
    }

    public void RechargeAgain()
    {
        currentRechargeTime = rechargeTime;
    }

}
