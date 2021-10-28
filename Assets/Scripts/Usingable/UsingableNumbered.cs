using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsingableNumbered : Usingable
{
    private Icon currentIcon;

    protected override void Start()
    {
        base.Start();
        currentIcon = GetComponent<Icon>();
    }
    public override bool UseItem()
    {
        if (!base.UseItem()) return false;

        currentIcon.count--;
        if (currentIcon.count <= 0) Destroy(gameObject);
        currentIcon.UpdateCount();

        return true;
        
    }

}
