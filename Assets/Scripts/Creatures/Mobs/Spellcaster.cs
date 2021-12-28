using System.Collections.Generic;
using UnityEngine;




public class Spellcaster : SmartRangedAttackPosition
{
    [Header("Spellcaster")]
    public List<NPCSpell> spells;
    protected bool isSpellcasting = false;



    protected override void Update()
    {
        base.Update();
        if (!isSpellcasting && !anim.GetBool("Walk"))
            TryCastSpell(SelectRandomSpell());
    }




    // ѕопытатьс€ произнести заклинание
    protected virtual void TryCastSpell(NPCSpell spell)
    {
        isSpellcasting = true;
        spell.BeginCast();
        anim.speed = spell.speedCast;
        anim.SetTrigger("Spellcast");
        isStunned = true;
    }

    public void EndSpellcasting()
    {
        isSpellcasting = false;
        isStunned = false;
    }





    // ¬ыбрать случайное заклинание с учетом их приоритета
    protected NPCSpell SelectRandomSpell()
    {
        List<int> spellNumbers = new List<int>();
        for (int i = 0; i < spells.Count; i++)
        {
            for (int j = 0; j < spells[i].priority; j++)
            {
                spellNumbers.Add(i);
            }
        }
        return spells[spellNumbers[Random.Range(0, spellNumbers.Count)]];
    }


}
