using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnable : MonoBehaviour
{
    void Start()
    {
        Control.singleton.playerControlActive = true;
        Control.singleton.playerInterfaceActive = true;
    }
}
