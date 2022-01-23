using UnityEngine;
using FMODUnity;

public class OneTargetBullet_Spell : NPCSpell
{
    [Header("One bullet spell:")]
    public int priorityIfTargetIsVisible;

    [SerializeField] protected OneTargetBullet_Parameters bulletParameters;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected EventReference sound;


    protected SmartRangedAttackPosition stalker;

    protected override void Start()
    {
        base.Start();
        stalker = GetComponentInParent<SmartRangedAttackPosition>();
    }



    public override void Activate()
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
        if (stalker.follow == null) return;
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(bulletParameters, transform, stalker.follow.transform);
    }

    public override void CalculatePriority()
    {
        if (stalker.TargetIsVisible && stalker.follow != null) priority = priorityIfTargetIsVisible;
        else priority = 0;
    }

    public override void BeginCast(){}

    public override void StopCast(){}
}
