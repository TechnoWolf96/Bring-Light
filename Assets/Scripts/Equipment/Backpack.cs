using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public int maxSlot;

    private void Start()
    {
        Inventory inv;
        inv = GameObject.FindWithTag("Script").GetComponent<Inventory>();
        inv.maxSlot = maxSlot;
        inv.ChangeColorSlots();

    }
}
