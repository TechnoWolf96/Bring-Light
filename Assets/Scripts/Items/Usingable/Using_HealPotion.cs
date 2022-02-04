using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Using_HealPotion : UsingableItem
{
    public int heal;
    public GameObject particles;

    public override void Use()
    {
        base.Use();
        Player.singleton.health += heal;
        Instantiate(particles, Player.singleton.transform);
    }
}
