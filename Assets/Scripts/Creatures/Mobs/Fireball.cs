using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Stalker_NotRelease
{
    [Header("Fireball:")]
    public GameObject explotionPrefab;          // Префаб взрыва
    public float radiusTriggerExplotion;        // Дистанция от игрока, на которой шар взрывается
    public float radiusExplotion;               // Радиус взрыва
    public AttackParameters attack;             // Параметры атаки

    private float offset = 1.5f; // Регулировка размера спрайта взрыва под радиус


    protected override void Update()
    {
        base.Update();
        if (death) return;
        if (CheckExplosion()) Attack();
    }

    private bool CheckExplosion() // Проверка, нет ли в радиусе триггера игроков
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, radiusTriggerExplotion, layer);
        if (coll != null) return true;
        return false;
    }

    public void Attack()
    {
        death = true;
        Collider2D[] all = Physics2D.OverlapCircleAll(transform.position, radiusExplotion, layer);
        foreach (var item in all)
        {
            item.GetComponent<Creature_NotRelease>().GetDamage(attack, transform);
        }
        GameObject inst = Instantiate(explotionPrefab, transform.position, Quaternion.identity);
        inst.transform.localScale = new Vector2(radiusExplotion, radiusExplotion) * offset;
        Death();
    }
    


    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusTriggerExplotion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusExplotion);
    }

}
