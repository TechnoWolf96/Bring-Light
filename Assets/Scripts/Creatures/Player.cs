using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{

    private bool right = true;                      // Смотрит ли игрок направо
    private CheckParameters checkParameters;        // Система контроля параметров
    private Inventory inventory;                    // Инвентарь

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        checkParameters = GameObject.FindWithTag("Script").GetComponent<CheckParameters>();
        inventory = GameObject.FindWithTag("Script").GetComponent<Inventory>();
    }

    protected override void Update()
    {
        if (!Move() && !stunned) rb.velocity = Vector2.zero; // Если игрок не двигается и не оглушен, то он останавливается
        CheckFace();
    }



    private bool Move()
    {
        if (currentTimeStunning > 0) return false;
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        if (InputX == 0 && InputY == 0) return false;
        Vector2 moveDirection = new Vector2(InputX, InputY).normalized;
        rb.velocity = moveDirection * speed;
        return true;
    }


    private void Flip() // Поворот игрока в другую сторону
    {
        right = !right;
        Vector2 scale = new Vector2(transform.localScale.x, transform.localScale.y);
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void CheckFace() // Проверка в какую сторону смотрит игрок
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < 0 && right) Flip();
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x > 0 && !right) Flip();
    }

    public override void Death()
    {
        base.Death();
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null) weapon.enabled = false;
        inventory.enabled = false;
        enabled = false;
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        // В классе-предке вызывается метод появления полоски с хп - поэтому переписан тот же код, но без вывода полоски с хп
        int realDamage = GetRealDamage(attack); // Подсчет реального урона
        health -= realDamage;

        if (bullet != null) PushBack(attack.pushForce, bullet, attack.timeStunning); // Если урон от снаряда - толчок от снаряда
        else PushBack(attack.pushForce, attacking, attack.timeStunning);            // Если рукопашный урон - толчок от атакующего

        if (health <= 0 && !death) // Здоровье ниже или равно 0 - существо умирает
        {
            health = 0;
            Death();
        }
        if (!death) anim.SetTrigger("GetDamage"); // Если сушество не мертво, то запускается анимация получения урона
        checkParameters.UpdateParameters();     // Обновляем параметры в UI при получении урона
    }

    
}
