using UnityEngine;
using UnityEngine.AI;


// Класс "Преследователь"
public abstract class Stalker : Creature
{
    [Header("Stalker:")]
    public float distanceDetection; // Дистанция обнаружения объекта для преследования
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
            follow = Physics2D.OverlapCircle(transform.position, distanceDetection, layer)?.GetComponent<Transform>();
    }

    protected virtual void Stalk() // Объект получает точку назначения и начинает преследование
    {
        FlipAndNullVelocity();
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        anim.SetTrigger("Walk");
    }

    protected virtual void OnDrawGizmosSelected() // Рисует область обнаружения
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceDetection);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        navAgent.isStopped = true; // При оглушении преследователь не может бежать за нападающим
        follow = attacking; // При получении урона преследователь бежит за нападающим
    }

    protected void Flip() // Поворот преследователя в другую сторону
    {
        right = !right;
        Vector2 scale = new Vector2(transform.localScale.x, transform.localScale.y);
        scale.x *= -1;
        transform.localScale = scale;
    }
    protected void FlipAndNullVelocity()    // Определение поворота и зануление velocity
    {
        // Т.к скорость в NavAgent и в velocity накладываются друг на друга, то velocity нужно занулить
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        if (follow.position.x < transform.position.x && right) Flip();
        if (follow.position.x > transform.position.x && !right) Flip();
    }


    public override void Death()
    {
        base.Death();
        navAgent.enabled = false;
    }


}
