using UnityEngine;
using UnityEngine.AI;

public class CloseAttackFSM : Creature
{
    [SerializeField] protected float distanceDetection;
    [SerializeField] protected LayerMask detectionableLayer;
    [SerializeField] protected float lossDistance;
    public Transform followBodyCenter { get; protected set; }
    public Transform follow
    {
        get => _follow;
        set 
        {
            if (value != null) { _follow = value; followBodyCenter = value.transform.Find("BodyCenter"); }
            else { _follow = null; followBodyCenter = null; }
        }
    }
    public override float speed { get => base.speed; set { _speed = value; navAgent.speed = _speed; } }

    public NavMeshAgent navAgent { get; protected set; }
    public Weapon currentWeapon { get; protected set; }
    protected Transform _follow;
    public Vector2 spawnPosition { get; protected set; }



    protected override void Start()
    {
        base.Start();
        if (currentWeapon != null)
        {
            currentWeapon = GetComponentInChildren<Weapon>();
            anim.runtimeAnimatorController = currentWeapon.animController;
        }
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        speed = speed;
        spawnPosition = transform.position;
    }


    protected override void UpdateState()
    {
        if (follow == null) FindNewTarget();
        if (follow != null && Vector2.Distance(transform.position, follow.position) < lossDistance)
        {
            anim.SetFloat("DistanceToTarget", Vector2.Distance(transform.position, follow.position));
            LookAt(follow.position);
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
            follow = newFollow.transform;
            anim.SetBool("SeeTarget", true);
        }

    }
    public void AttackMoment() => currentWeapon.Attack();

}
