using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCSpell : MonoBehaviour
{
    [Header("NPC spell:")]
    public int priority;  // Шанс выполнения именно этого заклинания (рекомендуется приоритет от 1 до 10)
    public float speedCast;                 // Скорость анимации исполнения заклинания

    protected Creature creature;

    protected virtual void Start()
    {
        creature = GetComponentInParent<Creature>();
    }


    // Вызывается при начале произношения заклинания
    public abstract void BeginCast();

    // Вызывается в случае остановки произношения заклинания (При получении урона)
    public abstract void StopCast();

    // Получить полезный эффект от заклинания (спустя время вызывается в анимации Spellcast)
    public abstract void Activate();

    // Задает правило, по которому вычисляется приоритет заклинания
    public abstract void CalculatePriority();

}
