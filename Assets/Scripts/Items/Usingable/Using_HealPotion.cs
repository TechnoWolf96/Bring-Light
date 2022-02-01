using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Using_HealPotion : UsingableItem
{
    public int heal;
    public GameObject healEffect;

    public override void Use()
    {
        base.Use();
        player.health += heal;
        Instantiate(healEffect, player.transform);
    }
}
