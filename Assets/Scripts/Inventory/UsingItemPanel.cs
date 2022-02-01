using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItemPanel : MonoBehaviour
{
    public List<Transform> usingableSlots;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && usingableSlots[0].childCount != 0)
        {
            usingableSlots[0].GetChild(0).GetComponent<Icon>().TryUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && usingableSlots[1].childCount != 0)
        {
            usingableSlots[1].GetChild(0).GetComponent<Icon>().TryUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && usingableSlots[2].childCount != 0)
        {
            usingableSlots[2].GetChild(0).GetComponent<Icon>().TryUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && usingableSlots[3].childCount != 0)
        {
            usingableSlots[3].GetChild(0).GetComponent<Icon>().TryUse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && usingableSlots[4].childCount != 0)
        {
            usingableSlots[4].GetChild(0).GetComponent<Icon>().TryUse();
        }
    }


}
