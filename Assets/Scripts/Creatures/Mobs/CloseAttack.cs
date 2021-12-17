using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : Stalker
{

    [Header("Close Attack:")]
    public AttackParameters attack;
    public float recharge;
    public float radiusAttack;
    public Transform attackPosition;

    protected float currentRecharge;

    protected override void Start()
    {
        base.Start();
        currentRecharge = recharge;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        currentRecharge -= Time.deltaTime;
    }

    protected override void Update()
    {
        base.Update();
        if (isDeath) return;
        if (CheckAttack() && Recharged()) Attack();
    }



    protected virtual bool CheckAttack()
    {
        if (follow == null) return false;
        if (Vector2.Distance(follow.position, attackPosition.position) < radiusAttack)
            return true;
        return false;
    }

    protected virtual void Attack()
    {
        currentRecharge = recharge;
        Collider2D damaged = Physics2D.OverlapCircle(attackPosition.position, radiusAttack, detectionableLayer);
        bool crit = attack.SetCrit();
        damaged.GetComponent<Creature>().GetDamage(attack, transform);
        if (crit) anim.SetTrigger("Crit");
        else anim.SetTrigger("Attack");
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, radiusAttack);
    }

    protected bool Recharged()
    {
        return currentRecharge < 0;
    }

}
