using UnityEngine;
using FMODUnity;

public class OneTargetBullet_Spell : NPCSpell
{
    [Header("One bullet spell:")]
    public int priorityIfTargetIsVisible;

    //[SerializeField] protected OneTargetBullet_Parameters bulletParameters;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected EventReference sound;


    protected AbleToSeekRangedAttackPosition stalker;

    protected override void Start()
    {
        base.Start();
        stalker = GetComponentInParent<AbleToSeekRangedAttackPosition>();
    }



    public override void Activate()
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
        if (stalker.follow == null) return;
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform, stalker.followBodyCenter.position);
    }

    public override void CalculatePriority()
    {
        if (stalker.targetIsVisible && stalker.follow != null) priority = priorityIfTargetIsVisible;
        else priority = 0;
    }

    public override void BeginCast(){}

    public override void StopCast(){}
}
