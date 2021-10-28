using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield_Item : Usingable
{
    [Header("Using:")]
    public GameObject magicShield;
    private Transform playerPosition;

    protected override void Start()
    {
        base.Start();
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public override bool UseItem()
    {
        if (!base.UseItem()) return false;
        Instantiate(magicShield, playerPosition);
        return true;
    }
}

