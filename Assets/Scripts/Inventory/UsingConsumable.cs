using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingConsumable : MonoBehaviour
{
    public GameObject[] usingSlots;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            usingSlots[0].GetComponentInChildren<Usingable>()?.UseItem();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            usingSlots[1].GetComponentInChildren<Usingable>()?.UseItem();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            usingSlots[2].GetComponentInChildren<Usingable>()?.UseItem();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            usingSlots[3].GetComponentInChildren<Usingable>()?.UseItem();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            usingSlots[4].GetComponentInChildren<Usingable>()?.UseItem();
    }


}
