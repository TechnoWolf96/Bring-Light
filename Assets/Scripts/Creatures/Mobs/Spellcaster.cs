using System.Collections.Generic;
using UnityEngine;




public class Spellcaster : AbleToSeekRangedAttackPosition
{
    [Header("Spellcaster")]
    [SerializeField] protected List<NPCSpell> spells;
    protected NPCSpell selectedSpell;
    public bool canCastSpell { get; protected set; }

    protected override void StateUpdate()
    {
        base.StateUpdate();
        if (selectedSpell == null && canCastSpell)
            TryCastSpell();
    }




    // Попытаться произнести заклинание
    protected virtual void TryCastSpell()
    {
        SelectRandomSpell();
        if (selectedSpell == null) return; // Если у всех имеющихся заклинаний приоритет 0 - ничего не делаем
        selectedSpell.BeginCast();
        anim.SetFloat("SpeedCast", selectedSpell.speedCast);
        anim.SetTrigger("Spellcast");
    }

    public void EndSpellcasting()
    {
        selectedSpell?.StopCast();
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
        EndSpellcasting();  // Заклинание прерывается
        canCastSpell = false;
        base.GetDamage(attack, attacking, bullet);
    }

}
