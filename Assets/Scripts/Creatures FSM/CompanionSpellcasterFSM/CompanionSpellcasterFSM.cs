using System.Collections.Generic;
using UnityEngine;

public class CompanionSpellcasterFSM : CompanionCloseAttackFSM
{
    [SerializeField] protected List<SpellNPC> _spells;
    public List<SpellNPC> spells { get => _spells; }

    public SpellNPC currentSpell { get; set; }

    protected override void UpdateState()
    {
        base.UpdateState();
        bool canSpellcast = false;
        foreach (SpellNPC spell in spells)
        {
            spell.CalculatePriority();
            if (spell.priority != 0) { canSpellcast = true; break; }
        }
        anim.SetBool("CanSpellcast", canSpellcast);
    }

    public SpellNPC SelectRandomSpell(List<SpellNPC> spellList)
    {
        foreach (var spell in spellList)
            spell.CalculatePriority();

        List<int> spellNumbers = new List<int>();
        for (int i = 0; i < spellList.Count; i++)
        {
            for (int j = 0; j < spellList[i].priority; j++)
            {
                spellNumbers.Add(i);
            }
        }
        if (spellNumbers.Count == 0) return null;
        return spellList[spellNumbers[Random.Range(0, spellNumbers.Count)]];
    }

    public void ActivateSpell() => currentSpell.Activate();
    public void BeginCast()
    {
        currentSpell = SelectRandomSpell(spells);
        currentSpell.BeginCast();
    }

}
