using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* ”мное соблюдение рассто€ни€ между целью преследовани€ и самим объектом.
 * ѕредполагаетс€ дл€ использовани€ у юнитов с дальней атакой, когда не нужно подходить слишком близко или находитьс€ слишком далеко.
     * —ущество останавливаетс€ на случайном рассто€нии от объетка follow, выбранном в интервале
     * между minStopDistance и maxStopDistance.
     * ≈сли цель отошла от существа на рассто€ние, большее maxStopDistance,
     * то существо начинает снова двигатьс€ в сторону цели, снова наход€ случайное рассто€ние.
     * ≈сли рассто€ние до объетка follow меньше чем runFromDistance, то существо пытаетс€ отдалитьс€
     * на ранее случайно определенную дистанцию.
     */
public class SmartDistance : Stalker
{
    [Header("Smart Distance:")]
    [Min(0)] public float maxStopDistance;           // ћаксимальное рассто€ние остановки до цели
    [Min(0)] public float minStopDistance;           // ћинимальное рассто€ние остановки до цели
    [Min(0)] public float runFromDistance;           // –ассто€ние начала отдалени€

    private float randomStopDistance = 0;    // ѕеременна€ под хранение сгенерировавшейс€ случайной дистанции остановки
    private bool distanceDefined;            // ѕоказывает, определена ли уже случайна€ дистанци€


    protected override void Start()
    {
        base.Start();
        SetRandomDistance();
    }
    protected void SetRandomDistance()
    {
        randomStopDistance = Random.Range(minStopDistance, maxStopDistance);
        distanceDefined = true;
        print(randomStopDistance);
    }
    protected void RunFromFollow()
    {
        Vector2 direction = (transform.position - follow.position).normalized;
        direction *= runFromDistance - Vector2.Distance(follow.position, transform.position);
        Vector2 newPosition = transform.position;
        newPosition += direction;
        navAgent.isStopped = false;
        navAgent.SetDestination(newPosition);
        anim.SetTrigger("Walk");
    }
    protected void RunToFollow()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
        anim.SetTrigger("Walk");
    }
    protected void RunStop()
    {
        // “.к цель достигнута, при выходе за runToDistance случайна€ дистанци€ определитс€ заново
        distanceDefined = false;
        print("distance Defined = false");
        navAgent.isStopped = true;
        anim.SetTrigger("Stop");
    }

    protected override void Stalk()
    {
        FlipAndNullVelocity();
        // ≈сли не достигли нужной дистанции - преследуем
        if (distanceDefined && Vector2.Distance(follow.position, transform.position) > randomStopDistance)
            RunToFollow();
        // ≈сли достигли случайно установленной дистанции, но не оказались ближе runFromDistance - останавливаемс€
        if (Vector2.Distance(follow.position, transform.position) < randomStopDistance &&
            Vector2.Distance(follow.position, transform.position) > runFromDistance)
            RunStop();
        // ≈сли существо оказалось слишком близко к цели, то оно должно отдалитьс€ на рассто€ние minStopDistance
        if (Vector2.Distance(follow.position, transform.position) < runFromDistance)
            RunFromFollow();
        //  ак только покинули максимально допустимую дистанцию - должны обновить случайную дистанцию
        if (!distanceDefined && Vector2.Distance(follow.position, transform.position) > maxStopDistance)
            SetRandomDistance();
        

    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxStopDistance);
        Gizmos.DrawWireSphere(transform.position, minStopDistance);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, runFromDistance);
    }

    

}
