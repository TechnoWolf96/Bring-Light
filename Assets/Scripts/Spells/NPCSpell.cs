using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCSpell : MonoBehaviour
{
    [Header("NPC spell:")]
    public int priority;                // Шанс выполнения именно этого заклинания (рекомендуется приоритет от 1 до 10)
    public float speedCast;             // Скорость анимации исполнения заклинания
    public float timeUntilActivate;     // Время до получения полезного эффекта

    [SerializeField] protected float currentTimeUntilActivate;
    protected bool casting = false;

    protected virtual void Start()
    {
        currentTimeUntilActivate = timeUntilActivate; 
    }
    protected virtual void FixedUpdate()
    {
        if (casting) currentTimeUntilActivate -= Time.deltaTime;
    }

    protected virtual void Update()
    {
        if (currentTimeUntilActivate < 0) Cast();
    }

    public virtual void BeginCast()
    {
        currentTimeUntilActivate = timeUntilActivate;
        casting = true;
        ApplyVisualEffects();
    }

    protected virtual void ApplyVisualEffects() { }

    protected virtual void Cast()
    {
        casting = false;
        currentTimeUntilActivate = timeUntilActivate;
    }
}
