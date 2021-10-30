using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeDamage { Physical, Magic }

[System.Serializable]

// Параметры атаки
public struct AttackParameters 
{
    public int damage;                  // Урон
    public TypeDamage typeDamage;       // Тип урона
    public float pushForce;             // Мощность толчка
    public float timeStunning;          // Время оглушения
}


// Универсальный класс - "Существо", является предком любого живого объекта на сцене
public abstract class Creature : MonoBehaviour
{
    [Header("Creature:")]
    public float speed;                 // Скорость существа
    public int maxHealth;               // Максимальный запас здоровья
    public int health;                  // Текущий запас здоровья
    public int physicalProtect;         // Физическая защита
    public int magicProtect;            // Магическая защита
    public float xDamageGain = 0;       // Множитель урона от оружия
    public float xPushMass = 1;         // Множитель мощности отталкивания при получении урона

    protected bool stunned = false;             // Является ли существо оглушенным
    protected bool death = false;               // Является ли существо мертвым
    protected float currentTimeStunning = 0f;   // Текущее время оглушения
    protected Rigidbody2D rb;                   // Агент RigitBody существа
    protected Animator anim;                    // Анимация поведения существа
    protected Collider2D collider;              // Коллайдер существа



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
    protected virtual void Update() {}

    protected virtual void FixedUpdate() 
    {
        currentTimeStunning -= Time.deltaTime; //Отсчет оставшегося времени оглушения
        if (stunned && currentTimeStunning < 0)
        {
            stunned = false;
            rb.velocity = Vector2.zero;
        }
            
    }


    public void PushBack(float force, Transform pusher, float timeStunning) // Отталкивание и оглушение при получении урона от позиции толкающего объекта
    {
        stunned = true;
        currentTimeStunning = timeStunning; 
        Vector2 pushDirection = new Vector2(transform.position.x - pusher.position.x, transform.position.y - pusher.position.y).normalized;
        rb.velocity = pushDirection * force * xPushMass;
    }

    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null) // Получение урона с силой отталкивания от позиции атакующего и оглушением
    {
        // ==== Подсчет реального урона ====
        int realDamage = attack.damage; 
        if (attack.typeDamage == TypeDamage.Physical) realDamage -= physicalProtect;
        else realDamage -= magicProtect;
        if (realDamage < 0) realDamage = 0;
        // =================================
        health -= realDamage;

        if (bullet != null) PushBack(attack.pushForce, bullet, attack.timeStunning); // Если урон от снаряда - толчок от снаряда
        else PushBack(attack.pushForce, attacking, attack.timeStunning);            // Если рукопашный урон - толчок от атакующего

        if (health <= 0 && !death) // Здоровье ниже или равно 0 - существо умирает
        {
            health = 0;
            Death();
        }
        if (!death) anim.SetTrigger("GetDamage"); // Если сушество не мертво, то запускается анимация получения урона
    }
    public virtual void Death()
    {
        death = true;
        gameObject.layer = LayerMask.NameToLayer("Corpses");
        rb.bodyType = RigidbodyType2D.Static;
        collider.enabled = false;
        anim.SetTrigger("Death");
    }

}
