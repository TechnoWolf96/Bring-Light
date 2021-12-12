using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon:")]
    public float recharge;              // Перезарядка
    //public AttackParameters attack;   // Параметры атаки
    //public LayerMask layer;             // Слой объектов, по которому будет проходить атака

    protected Transform playerPos;      // Позиция игрока, держащего оружие
    protected Creature_NotRelease creature;        // Скрипт существа игрока, держащего оружие
    protected float rechargeTime;       // Текущее время до перезарядки

    protected abstract void Attack(); // Атака

    protected virtual void Start()
    {
        rechargeTime = recharge;
        playerPos = transform.parent;
        creature = GetComponentInParent<Creature_NotRelease>();
    }

    protected virtual void FixedUpdate() // Уменьшение текущего времени до перезарядки
    {
        rechargeTime -= Time.deltaTime;
    }

    protected bool IsRecharged()
    {
        return rechargeTime < 0;
    }

}
