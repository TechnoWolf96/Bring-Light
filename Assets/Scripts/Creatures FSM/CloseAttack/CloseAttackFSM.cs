using UnityEngine;
using UnityEngine.AI;

public class CloseAttackFSM : Creature
{
    [SerializeField] protected float distanceDetection;
    [SerializeField] protected LayerMask detectionableLayer;
    [SerializeField] protected float lossDistance;

    public Creature follow { get; set; }
    public override float speed { get => base.speed; set { _speed = value; navAgent.speed = _speed; } }

    public NavMeshAgent navAgent { get; protected set; }
    public Weapon currentWeapon { get; protected set; }
    
    public Vector2 spawnPosition { get; protected set; }



    protected override void Start()
    {
        base.Start();
        currentWeapon ??= GetComponentInChildren<Weapon>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        navAgent.speed = speed;
        spawnPosition = transform.position;
    }


    protected override void UpdateState()
    {
        if (follow == null) FindNewTarget();


        else anim.SetBool("SeeTarget", true);
        ChangeTargetWhileProvocation();

        if (follow != null && Vector2.Distance(transform.position, follow.transform.position) < lossDistance)
        {
            anim.SetFloat("DistanceToTarget", Vector2.Distance(transform.position, follow.transform.position));
        }
        else
        {
            anim.SetFloat("DistanceToTarget", 10000f);
            follow = null;
            anim.SetBool("SeeTarget", false);
        }
        anim.SetFloat("DistanceToSpawn", Vector2.Distance(transform.position, spawnPosition));
    }


    protected void FindNewTarget()
    {
        Collider2D newFollow = Physics2D.OverlapCircle(transform.position, distanceDetection, detectionableLayer);

        if (newFollow != null)
        {
            follow = newFollow.transform.GetComponent<Creature>();
            anim.SetBool("SeeTarget", true);
        }

    }

    protected void ChangeTargetWhileProvocation()
    {
        if (follow == null) return;
        Vector2 direction = follow.bodyCenter.position - bodyCenter.position;
        RaycastHit2D hit = Physics2D.Raycast(bodyCenter.position, direction, direction.magnitude, detectionableLayer);

        if (hit.collider != null) follow = hit.transform.GetComponent<Creature>();
    }



    public void AttackMoment() => currentWeapon.Attack();


    public override void Death()
    {
        navAgent.isStopped = true;
        base.Death();
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        if (follow == null)
        {
            follow = attacking.GetComponent<Creature>();
            anim.SetBool("SeeTarget", true);
        }
        else if (Vector2.Distance(transform.position, attacking.position) <
            Vector2.Distance(transform.position, follow.transform.position))
        {
            follow = attacking.GetComponent<Creature>();
            anim.SetBool("SeeTarget", true);
        }
            
    }

}
