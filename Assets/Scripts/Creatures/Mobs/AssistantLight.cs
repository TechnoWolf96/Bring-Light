using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantLight : SmartDistance
{
    [Header("Assistant Light:")]
    public Transform positionLight;

    protected override void Start()
    {
        base.Start();
        //follow = positionLight;
    }
}
