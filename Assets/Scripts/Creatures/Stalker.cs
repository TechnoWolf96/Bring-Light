using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// Класс "Преследователь"
public abstract class Stalker : Creature
{
    [Header("Stalker:")]
    public float distance; // Дистанция обнаружения объекта для преследования
    public LayerMask layer; // Слой, который отслеживает преследователь (Слой игроков)

    protected Transform follow; // Текущий объект для преследования
    protected NavMeshAgent navAgent; // Агент NawMesh, закрепленный на данном объекте
    protected bool right = true;

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    protected override void Update()
    {
        base.Update();
        navAgent.speed = speed; // Скорость в NawMesh равна скорости существа
        if (death) return;
        CheckStalk(); // Поиск объекта для преследования
        if (!stunned && follow != null) Stalk(); // Если объект не оглушен и есть за кем бежать, то начинает преследование
    }

    protected void CheckStalk() // Проверка, есть ли в зоне обнаружения объекты нужного слоя
    {
        if (follow == null) // Если не за кем бежать, то ищем объект для преследования
            follow = Physics2D.OverlapCircle(transform.position, distance, layer)?.GetComponent<Transform>();
    }

    protected void Stalk() // Объект получает точку назначения
    {
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        if (follow.position.x < transform.position.x && right) Flip();
        if (follow.position.x > transform.position.x && !right) Flip();
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        anim.SetTrigger("Walk");
    }

    protected virtual void OnDrawGizmosSelected() // Рисует область обнаружения
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        navAgent.isStopped = true; // При оглушении преследователь не может бежать за нападающим
        follow = attacking; // При получении урона преследователь бежит за нападающим
    }

    private void Flip() // Поворот преследователя в другую сторону
    {
        right = !right;
        Vector2 scale = new Vector2(transform.localScale.x, transform.localScale.y);
        scale.x *= -1;
        transform.localScale = scale;
    }

    public override void Death()
    {
        base.Death();
        navAgent.isStopped = true;
    }


}
