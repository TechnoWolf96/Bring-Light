using UnityEngine;

public class RangedAttackFSM : CloseAttackFSM
{
    [Min(0)] [SerializeField] protected float maxStopDistance;
    [Min(0)] [SerializeField] protected float minStopDistance;
    [Min(0)] [SerializeField] protected float tooCloseDistance;
    [SerializeField] protected LayerMask obstacleBulletLayer;

    public float randomStopDistance { get; protected set; }

    protected override void Start()
    {
        base.Start();
        randomStopDistance = Random.Range(minStopDistance, maxStopDistance);
    }

    protected override void UpdateState()
    {
        base.UpdateState();
        if (anim.GetFloat("DistanceToTarget") < randomStopDistance && CheckTargetIsVisible()) anim.SetBool("CanAttack", true);
        else anim.SetBool("CanAttack", false);

        if (anim.GetFloat("DistanceToTarget") < tooCloseDistance) anim.SetBool("TooClose", true);
        else anim.SetBool("TooClose", false);
    }




    protected bool CheckTargetIsVisible()
    {
        if (follow == null) return false;
        RaycastHit2D info = Physics2D.Raycast(bodyCenter.position, follow.bodyCenter.position - bodyCenter.position,
            Vector2.Distance(follow.bodyCenter.position, bodyCenter.position), obstacleBulletLayer);
        if (info.collider == null) return true;
        else return false;
    }


}
