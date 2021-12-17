using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion_Item : UsingableNumbered
{
    public int heal;

    private CheckParameters_NotRelease checkParameters;

    protected override void Start()
    {
        base.Start();
        checkParameters = GameObject.FindWithTag("Script").GetComponent<CheckParameters_NotRelease>();
    }

    public override bool UseItem()
    {
        if (!base.UseItem()) return false;

        Player_NotRelease player = checkParameters.currentPlayer;
        checkParameters.currentPlayer.health += heal;
        if (player.health > player.maxHealth)
            player.health = player.maxHealth;
        checkParameters.UpdateParameters();

        return true;
    }
}
