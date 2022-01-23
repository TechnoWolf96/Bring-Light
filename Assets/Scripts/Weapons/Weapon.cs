using UnityEngine;
using FMODUnity;

public interface IAttackWithWeapon
{
    public void Attack();
}


// Объект со скриптом Weapon должен быть дочерним в иерархии к объекту Weapon
public abstract class Weapon : MonoBehaviour
{
    
    [Header("Weapon:")]
    public float rechargeTime;              // Перезарядка
    public RuntimeAnimatorController animController;
    public LayerMask layer;         // Слой объектов, по которому будет проходить атака
    [SerializeField] protected EventReference attackSound;
    [SerializeField] protected float timeOriginalAttackAnimation;   // Время оригинальной анимации атаки (Выставляется вручную)

    public float GetTimeOriginalAttackAnimation() {return timeOriginalAttackAnimation;}

    protected Creature creature;            // Скрипт существа игрока, держащего оружие
    protected float currentRechargeTime;           // Текущее время до перезарядки

    public abstract void Attack(); // Атака

    public void BeginAttack()
    { 
        Library.Play3DSound(attackSound, creature.transform);
        RechargeAgain();
    }

    protected virtual void Start()
    {
        currentRechargeTime = rechargeTime;
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
