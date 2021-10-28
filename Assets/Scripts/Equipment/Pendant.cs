using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendant : Equipment
{
    public int bonusMagicProtect;


    protected override void Start()
    {
        base.Start();
        checkParameters.currentPlayer.magicProtect += bonusMagicProtect;
        checkParameters.UpdateParameters();
    }
    private void OnDestroy()
    {
        checkParameters.currentPlayer.magicProtect -= bonusMagicProtect;
        checkParameters.UpdateParameters();
    }
}
