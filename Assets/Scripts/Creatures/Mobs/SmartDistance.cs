using UnityEngine;

public class SmartDistance : Stalker
{
    [Header("Smart Distance:")]
    [Min(0)] [SerializeField] protected float maxStopDistance;
    [Min(0)] [SerializeField] protected float minStopDistance;
    [Min(0)] [SerializeField] protected float retreatDistance;

    protected float randomStopDistance;
    protected bool positionReached;

    protected override void Start()
    {
        base.Start();
        randomStopDistance = Random.Range(minStopDistance, maxStopDistance);
        positionReached = false;
    }

    protected override void StateUpdate()
    {
        if (follow == null) { FindNewTarget(); return; }
        LookAt(follow.position);
        // Слишком далеко от цели? Да - позиция не достигнута
        if (Vector2.Distance(follow.position, transform.position) > maxStopDistance)
            positionReached = false;

        // Позиция достигнута? Нет - преследуем
        if (!positionReached)
        {
            Stalk();
            if (Vector2.Distance(follow.position, transform.position) < randomStopDistance)
                positionReached = true;
            return;
        }
        // Позиция достигнута? Да - останавливаемся
        if (Vector2.Distance(follow.position, transform.position) > retreatDistance)
        { StalkStop(); return; }

        // Враг слишком близко? Да - отступаем
        if (Vector2.Distance(follow.position, transform.position) < retreatDistance)
        { Retreat(); return; }
    }
    protected virtual void Retreat()
    {
        Vector2 newPosition = transform.position + (transform.position - follow.position).normalized*5;
        navAgent.isStopped = false;
        navAgent.SetDestination(newPosition);
        anim.SetBool("Walk", true);
    }
    protected virtual void StalkStop()
    {
        positionReached = true;
        navAgent.isStopped = true;
        anim.SetBool("Walk", false);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxStopDistance);
        Gizmos.DrawWireSphere(transform.position, minStopDistance);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }

   
}
