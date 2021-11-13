using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [Header("Sword:")]
    public AttackParameters attack; // Параметры атаки
    public LayerMask layer;         // Слой объектов, по которому будет проходить атака
    public float offset;            // Смещение меча для регулировки направления
    public float radius;            // Радиус поражения меча
    public Transform point;         // Точка атаки

    private Animator anim;


    protected override void Start()
    {
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }


    private void Turn()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    private void Update()
    {
        Turn();
        if (Input.GetMouseButton(0) && rechargeTime < 0) Attack();
    }

    


    protected override void Attack()
    {
        rechargeTime = recharge;
        anim.SetTrigger("Attack");
        Collider2D[] enemy_col = Physics2D.OverlapCircleAll(point.position, radius, layer);
        bool crit = attack.SetCrit();
        foreach (var item in enemy_col)
        {
            item.GetComponent<Creature>().GetDamage(attack, transform);
        }
        if (crit) print("Crit!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point.position, radius);
    }

    


    
}
