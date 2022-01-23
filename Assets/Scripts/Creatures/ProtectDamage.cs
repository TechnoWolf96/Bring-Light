using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Всевозможные виды урона
public enum TypeDamage
{
    Physical, Holy, Fiery, Cold, Dark, Poison
}

[System.Serializable] // Параметры защиты
public class ProtectParameters
{
    [Header("Protection percentage:")]
    [Range(-1000, 100)] public int physical;    // От физического
    [Range(-1000, 100)] public int holy;        // От священного
    [Range(-1000, 100)] public int fiery;       // От огненного
    [Range(-1000, 100)] public int cold;        // От урона холодом
    [Range(-1000, 100)] public int dark;        // От урона силами тьмы
    [Range(-1000, 100)] public int poison;      // От урона ядом

    
    public static ProtectParameters operator+ (ProtectParameters th, ProtectParameters other)
    {
        ProtectParameters result = new ProtectParameters();
        result.physical = th.physical + other.physical;
        result.holy = th.holy + other.holy;
        result.fiery = th.fiery + other.fiery;
        result.cold = th.cold + other.cold;
        result.dark = th.dark + other.dark;
        result.poison = th.poison + other.poison;
        return result;
    }
    public static ProtectParameters operator -(ProtectParameters th, ProtectParameters other)
    {
        ProtectParameters result = new ProtectParameters();
        result.physical = th.physical - other.physical;
        result.holy = th.holy - other.holy;
        result.fiery = th.fiery - other.fiery;
        result.cold = th.cold - other.cold;
        result.dark = th.dark - other.dark;
        result.poison = th.poison - other.poison;
        return result;
    }
}

[System.Serializable] // Тип и величина урона
public struct Damage
{
    public TypeDamage typeDamage;                   // Тип урона
    [SerializeField] private int minDamage;         // Минимальный урон
    [SerializeField] private int maxDamage;         // Максимальный урон
    public int Damaged()     // Возвращает был ли крит и присваивает урон
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}


[System.Serializable] // Параметры атаки
public struct AttackParameters
{
    public Damage[] damages;            // Типы урона
    public float pushForce;             // Мощность толчка
    [Range(0, 100)] public int critChance;           // Вероятность в процентах нанесения критического урона
    private bool isCrit;            // Есть ли крит у текущей атаки


    public bool GetCrit() { return isCrit; }

    // Возвращает и устанавливает на текущую атаку был ли крит. Важно всегда вызывать метод перед атакой, чтобы обновить поле isCrit
    public bool CalculateCrit()      
    {
        if (critChance >= Random.Range(1, 101))
        {
            isCrit = true;
            return true;
        }
        isCrit = false;
        return false;
    }
}



