using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    protected CheckParameters checkParameters;

    protected virtual void Start()
    {
        checkParameters = GameObject.FindWithTag("Script").GetComponent<CheckParameters>();
    }
}
