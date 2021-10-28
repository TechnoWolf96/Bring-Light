using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Equipment
{
    public int protection;

    protected override void Start()
    {
        base.Start();
        checkParameters.currentPlayer.physicalProtect += protection;
        checkParameters.UpdateParameters();
    }
    private void OnDestroy()
    {
        checkParameters.currentPlayer.physicalProtect -= protection;
        checkParameters.UpdateParameters();
    }
}
