using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
    public int protection;
    public int speedBonus;
    protected override void Start()
    {
        base.Start();
        checkParameters.currentPlayer.physicalProtect += protection;
        checkParameters.currentPlayer.speed += speedBonus;
        checkParameters.UpdateParameters();
    }
    private void OnDestroy()
    {
        checkParameters.currentPlayer.physicalProtect -= protection;
        checkParameters.currentPlayer.speed -= speedBonus;
        checkParameters.UpdateParameters();
    }
}
