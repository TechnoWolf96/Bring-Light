using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableDecoration : MonoBehaviour, IDestructable
{
    public int health;
    public ProtectParameters protect;

    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        int realDamage = GetRealDamage(attack);
        if (realDamage == 0) return;
        health -= realDamage;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
        else
        {
            anim.SetTrigger("GetDamage");
        }
    }

    public virtual void Death()
    {
        anim.SetTrigger("Death");
        Destroy(this);
    }

    protected int GetRealDamage(AttackParameters attack) // –асчет получени€ реального урона существом с учетом его защиты и крита атаки
    {
        int result = 0;
        foreach (var item in attack.damages)
        {
            int preDamage = item.Damaged();
            switch (item.typeDamage)
            {
                case TypeDamage.Physical:
                    result += preDamage - (preDamage * protect.physical / 100);
                    break;
                case TypeDamage.Holy:
                    result += preDamage - (preDamage * protect.holy / 100);
                    break;
                case TypeDamage.Fiery:
                    result += preDamage - (preDamage * protect.fiery / 100);
                    break;
                case TypeDamage.Cold:
                    result += preDamage - (preDamage * protect.cold / 100);
                    break;
                case TypeDamage.Dark:
                    result += preDamage - (preDamage * protect.dark / 100);
                    break;
                case TypeDamage.Poison:
                    result += preDamage - (preDamage * protect.poison / 100);
                    break;
            }
        }
        if (result < 0) result = 0;
        return result;
    }

}
