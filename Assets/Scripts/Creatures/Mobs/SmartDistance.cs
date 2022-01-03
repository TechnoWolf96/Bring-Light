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

    //protected bool canGoBack;                  // ћожет ли существо отступать при приближении противника
    protected float randomStopDistance = 0;    // ѕеременна€ под хранение сгенерировавшейс€ случайной дистанции остановки
    protected bool distanceDefined;            // ѕоказывает, определена ли уже случайна€ дистанци€


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
    protected virtual void RunFromFollow()
    {
        Vector2 newPosition = transform.position + (transform.position - follow.transform.position).normalized;
        Collider2D collider = Physics2D.OverlapCircle(newPosition, 0.7f);
        if (collider != null)
        {
            print("Stop");
            RunStop();
            return;
        }
        navAgent.isStopped = false;
        navAgent.SetDestination(newPosition);
        anim.SetBool("Walk", true);
    }
    protected virtual void RunToFollow()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.transform.position);
        anim.SetBool("Walk", true);
    }
    protected virtual void RunStop()
    {
        // “.к цель достигнута, при выходе за runToDistance случайна€ дистанци€ определитс€ заново
        distanceDefined = false;
        navAgent.isStopped = true;
        anim.SetBool("Walk", false);
    }

    protected override void Stalk()
    {
        // “.к скорость в NavAgent и в velocity накладываютс€ друг на друга, то velocity нужно занулить
        if (rb.velocity != Vector2.zero) rb.velocity = Vector2.zero;
        //  ак только покинули максимально допустимую дистанцию - должны обновить случайную дистанцию
        if (!distanceDefined && Vector2.Distance(follow.transform.position, transform.position) > maxStopDistance)
            SetRandomDistance();

        // ≈сли не достигли нужной дистанции - преследуем
        if (distanceDefined && Vector2.Distance(follow.transform.position, transform.position) > randomStopDistance)
        {
            RunToFollow();
            return;
        }
        // ≈сли достигли случайно установленной дистанции, но не оказались ближе runFromDistance - останавливаемс€
        if (Vector2.Distance(follow.transform.position, transform.position) > runFromDistance-0.1f)
        {
            RunStop();
            return;
        } 
        // ≈сли существо оказалось слишком близко к цели, то оно должно отдалитьс€ на рассто€ние minStopDistance
        if (Vector2.Distance(follow.transform.position, transform.position) < runFromDistance)
        {
            RunFromFollow();
            return;
        }
            
        




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
