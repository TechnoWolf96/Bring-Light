using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Equipment
{
    public int bonusHealth;

    protected override void Start()
    {
        base.Start();
        checkParameters.currentPlayer.maxHealth += bonusHealth;
        checkParameters.currentPlayer.health += bonusHealth;
        checkParameters.UpdateParameters();
    }
    private void OnDestroy()
    {
        checkParameters.currentPlayer.maxHealth -= bonusHealth;
        checkParameters.currentPlayer.health -= bonusHealth;
        if (checkParameters.currentPlayer.health <= 0) checkParameters.currentPlayer.health = 1;
        checkParameters.UpdateParameters();
    }
}
