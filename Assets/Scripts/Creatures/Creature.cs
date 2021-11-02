using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Универсальный класс - "Существо", является предком любого живого объекта на сцене
public abstract class Creature : MonoBehaviour
{
    [Header("Creature:")]
    public float speed;                 // Скорость существа
    public int maxHealth;               // Максимальный запас здоровья
    public int health;                  // Текущий запас здоровья
    public ProtectParameters protect;   // Параметры защиты
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
        int realDamage = GetRealDamage(attack); // Подсчет реального урона
        if (realDamage < 0) realDamage = 0;
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

    private int GetRealDamage(AttackParameters attack)
    {
        int result = 0;
        foreach (var item in attack.damages)
        {
            switch (item.typeDamage)
            {
                case TypeDamage.Physical:
                    result += item.damage - (item.damage * protect.physical / 100);
                    break;
                case TypeDamage.Holy:
                    result += item.damage - (item.damage * protect.holy / 100);
                    break;
                case TypeDamage.Fiery:
                    result += item.damage - (item.damage * protect.fiery / 100);
                    break;
                case TypeDamage.Cold:
                    result += item.damage - (item.damage * protect.cold / 100);
                    break;
                case TypeDamage.Dark:
                    result += item.damage - (item.damage * protect.dark / 100);
                    break;
                default:
                    break;
            }
        }
        return result;
    }

}
