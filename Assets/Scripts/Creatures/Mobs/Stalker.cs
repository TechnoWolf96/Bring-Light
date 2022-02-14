using UnityEngine;
using UnityEngine.AI;


// Класс "Преследователь"
public class Stalker : Creature
{
    [Header("Stalker")]
    [SerializeField] protected float distanceDetection;
    [SerializeField] protected LayerMask detectionableLayer;

    protected const float updateTimeCicle = 0.3f;
    protected float timeUntilUpdate;
    public Transform followBodyCenter { get; protected set; }

    public Transform follow { get; private set; }
    public NavMeshAgent navAgent { get; protected set; }
    public override float speed 
    { 
        get => base.speed;
        set
        {
            base.speed = value;
            navAgent.speed = value;
        }
    }
    public void SetFollow(Transform target)
    {
        follow = target;
        followBodyCenter = target.GetComponent<Creature>().bodyCenter;
    }

    protected override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        timeUntilUpdate = updateTimeCicle;
    }

    protected virtual void FixedUpdate() => timeUntilUpdate -= Time.deltaTime;
    private void Update()
    {
        if (timeUntilUpdate < 0)
        {
            StateUpdate();
            timeUntilUpdate = updateTimeCicle;
        }
    }

    protected virtual void StateUpdate()
    {
        if (follow == null) FindNewTarget();
        else
        {
            Stalk();
            LookAt(follow.position);
        }
    }

    protected void FindNewTarget()
    {
        Collider2D newFollow = Physics2D.OverlapCircle(transform.position, distanceDetection, detectionableLayer);
        if (newFollow != null) SetFollow(newFollow.transform);
            
    }

    protected virtual void Stalk()
    {
        anim.SetBool("Walk", true);
        navAgent.isStopped = false;
        navAgent.SetDestination(follow.position);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceDetection);
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        SetFollow(attacking); // При получении урона преследователь бежит за нападающим
    }


    public override void Death()
    {
        navAgent.enabled = false;
        base.Death();
    }
}
