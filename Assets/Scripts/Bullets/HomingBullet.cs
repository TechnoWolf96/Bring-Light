using UnityEngine;
using UnityEngine.AI;

public abstract class HomingBullet : Bullet
{
    protected NavMeshAgent navAgent;
    protected Transform target;


    public override void InstBullet(Transform shotPoint, Vector3 target, Transform targetTransform = null)
    {
        navAgent = GetComponent<NavMeshAgent>();
        bulletRB = GetComponent<Rigidbody2D>();
        this.shotPoint = shotPoint;
        navAgent.speed = speed;
        this.target = targetTransform;
    }

    protected virtual void Update()
    {
        navAgent.SetDestination(target.position);
    }


}
