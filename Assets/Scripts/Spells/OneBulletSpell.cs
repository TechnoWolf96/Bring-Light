using UnityEngine;

public class OneBulletSpell : NPCSpell
{
    [Header("One bullet spell:")]
    public int priorityIfTargetIsVisible;

    [SerializeField] protected Bullet_Parameters bulletParameters;
    [SerializeField] protected GameObject bullet;
    

    protected SmartRangedAttackPosition stalker;

    protected virtual void Start()
    {
        stalker = GetComponentInParent<SmartRangedAttackPosition>();
    }



    public override void Activate()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(bulletParameters, transform.parent, stalker.follow);
    }

    public override void CalculatePriority()
    {
        if (stalker.TargetIsVisible) priority = priorityIfTargetIsVisible;
        else priority = 0;
    }

    public override void BeginCast(){}

    public override void StopCast(){}
}
