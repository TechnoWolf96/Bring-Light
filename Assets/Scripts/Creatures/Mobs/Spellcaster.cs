using System.Collections.Generic;
using UnityEngine;




public class Spellcaster : SmartRangedAttackPosition
{
    [Header("Spellcaster")]
    public List<NPCSpell> spells;
    protected NPCSpell selectedSpell;
    protected bool canCastSpell = true;

    public void CanCastSpell()
    {
        canCastSpell = true;
    }

    protected override void Update()
    {
        base.Update();
        if (selectedSpell == null && canCastSpell)
            TryCastSpell();
    }




    // ���������� ���������� ����������
    protected virtual void TryCastSpell()
    {
        SelectRandomSpell();
        if (selectedSpell == null) return; // ���� � ���� ��������� ���������� ��������� 0 - ������ �� ������
        selectedSpell.BeginCast();
        anim.SetFloat("SpeedCast", selectedSpell.speedCast);
        anim.SetTrigger("Spellcast");
    }

    public void EndSpellcasting()
    {
        selectedSpell?.StopCast();
        selectedSpell = null;
    }

    // �������� �������� ������ �� ���������� (������ ����� ���������� � �������� Spellcast)
    public void ActivateSpell()
    {
        selectedSpell.Activate();
    }

    // ������� ��������� ���������� � ������ �� ����������
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
        EndSpellcasting();  // ���������� �����������
        canCastSpell = false;   // ��������������� �������� ��������� �����
        base.GetDamage(attack, attacking, bullet);
    }

}
