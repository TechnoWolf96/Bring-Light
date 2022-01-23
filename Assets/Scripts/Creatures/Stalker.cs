using UnityEngine;
using UnityEngine.AI;


// Класс "Преследователь"
public class Stalker : Creature
{
    [Header("Stalker:")]
    public float distanceDetection; // Дистанция обнаружения объекта для преследования
    public LayerMask detectionableLayer; // Слой, который отслеживает преследователь (Слой игроков)

    [HideInInspector] public Creature follow; // Текущий объект для преследования
    [HideInInspector] public NavMeshAgent navAgent; // Агент NawMesh, закрепленный на данном объекте

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed; // Скорость в NawMesh равна скорости существа
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    protected override void Update()
    {
        base.Update();
        CheckStalk(); // Поиск объекта для преследования
        if (follow != null)
        {
            Stalk(); // Если объект не оглушен и есть за кем бежать, то начинает преследование
            LookAt(follow.transform.position);
        }
    }

    protected void CheckStalk() // Проверка, есть ли в зоне обнаружения объекты нужного слоя
    {
        if (follow == null) // Если не за кем бежать, то ищем объект для преследования
        {
            Creature newFollow = Physics2D.OverlapCircle(transform.position, distanceDetection, detectionableLayer)?.GetComponent<Creature>();
            if (newFollow != null) SetFollow(newFollow);
            else follow = null;
        }
            
    }
    protected void SetFollow(Creature newTarget)
    {
        follow = newTarget;
    }

    protected virtual void Stalk() // Объект получает точку назначения и начинает преследование
    {
        // Т.к скорость в NavAgent и в velocity накладываются друг на друга, то velocity нужно занулить
        //if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        anim.SetBool("Walk", true);
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.transform.position);
    }

    protected virtual void OnDrawGizmosSelected() // Рисует область обнаружения
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceDetection);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        if (attacking.position != transform.position)   // Если атакующий не он сам, то:
            SetFollow(attacking.GetComponent<Creature>()); // При получении урона преследователь бежит за нападающим
    }


    public override void Death()
    {
        navAgent.enabled = false;
        base.Death();
    }
}
