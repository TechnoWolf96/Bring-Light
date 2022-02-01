using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_MaxHealth : EquipmentItem
{
    public int bonusMaxHealth;

    public override void PutOff()
    {
        player.maxHealth -= bonusMaxHealth;
    }

    public override void PutOn()
    {
        player.maxHealth += bonusMaxHealth;
    }
}
