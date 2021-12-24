using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_notRelease : MonoBehaviour
{
    [Header("Weapon:")]
    public float recharge;              // Перезарядка


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
