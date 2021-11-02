using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield_Inst : Equipment
{
    [SerializeField] private int bonusMagicProtect;
    [SerializeField] private float timeToDestroy;

    private void FixedUpdate()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy < 0) Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
       // checkParameters.currentPlayer.magicProtect += bonusMagicProtect;
       // checkParameters.UpdateParameters();
    }

    private void OnDestroy()
    {
        //checkParameters.currentPlayer.magicProtect -= bonusMagicProtect;
        //checkParameters.UpdateParameters();
    }


}
