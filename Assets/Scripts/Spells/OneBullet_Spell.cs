using UnityEngine;

public class OneBullet_Spell : SpellNPC
{
    [SerializeField] protected LayerMask obstacleBulletLayer;
    [SerializeField] protected int priorityIfTargetIsVisible;
    [SerializeField] protected GameObject bullet;



    public override void Activate()
    {
        if (spellcaster.follow == null) return;
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().
            InstBullet(transform, spellcaster.follow.bodyCenter.position);
    }

    public override void CalculatePriority()
    {
        if (currentRechargeTime > 0) { priority = 0; return; }
        if (CheckTargetIsVisible()) priority = priorityIfTargetIsVisible;
        else priority = 0;
    }


    protected bool CheckTargetIsVisible()
    {
        if (spellcaster.follow == null) return false;
        RaycastHit2D info = Physics2D.Raycast(spellcaster.bodyCenter.position, spellcaster.follow.bodyCenter.position - spellcaster.bodyCenter.position,
            Vector2.Distance(spellcaster.follow.bodyCenter.position, spellcaster.bodyCenter.position), obstacleBulletLayer);
        if (info.collider == null) return true;
        else return false;
    }
}
