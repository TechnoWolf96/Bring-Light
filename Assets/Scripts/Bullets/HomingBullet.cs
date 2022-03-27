using UnityEngine;
using UnityEngine.AI;

public abstract class HomingBullet : Bullet
{
    protected NavMeshAgent agent;
    protected Transform target;


    public override void InstBullet(Transform shotPoint, Vector3 target, Transform targetTransform = null)
    {
        agent = GetComponent<NavMeshAgent>();

        bulletRB = GetComponent<Rigidbody2D>();
        this.shotPoint = shotPoint;
        agent.speed = speed;
        this.target = targetTransform;
    }

    protected virtual void Update()
    {
        agent.SetDestination(target.position);
    }


}
