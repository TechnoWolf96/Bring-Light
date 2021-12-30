using System.Collections.Generic;
using UnityEngine;




public class Spellcaster : SmartRangedAttackPosition
{
    [Header("Spellcaster")]
    public List<NPCSpell> spells;
    protected NPCSpell selectedSpell;


    protected override void Update()
    {
        base.Update(); if (isDeath) return;
        if (!isStunned && !anim.GetBool("Walk"))
            TryCastSpell();
    }




    // Попытаться произнести заклинание
    protected virtual void TryCastSpell()
    {
        SelectRandomSpell();
        // Если у всех имеющихся заклинаний приоритет 0 - ничего не делаем
        if (selectedSpell == null) return;
        selectedSpell.BeginCast();
        anim.speed = selectedSpell.speedCast;
        anim.SetTrigger("Spellcast");
        
        isStunned = true;
        currentTimeStunning = 10000f;
    }

    public void EndSpellcasting()
    {
        isStunned = false;
        selectedSpell = null;
    }

    // Получить полезный эффект от заклинания (спустя время вызывается в анимации Spellcast)
    public void ActivateSpell()
    {
        selectedSpell.Activate();
    }

    // Выбрать случайное заклинание с учетом их приоритета
    protected void SelectRandomSpell()
    {
        foreach (var spell in spells)
            spell.CalculatePriority();

        List<int> spellNumbers = new List<int>();
        for (int i = 0; i < spells.Count; i++)
        {
            for (int j = 0; j < spells[i].priority; j++)
            {
                spellNumbers.Add(i);
            }
        }
        if (spellNumbers.Count == 0) return;
        selectedSpell = spells[spellNumbers[Random.Range(0, spellNumbers.Count)]];
    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        selectedSpell?.StopCast();
        base.GetDamage(attack, attacking, bullet);
    }

}
