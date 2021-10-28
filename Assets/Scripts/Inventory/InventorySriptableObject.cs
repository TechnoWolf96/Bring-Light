using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class InventorySriptableObject : ScriptableObject
{
    public int activeWeapon;
    public int[] idEquipment;
    public int[] idInBackpack;
    public int[] countEquipment;
    public int[] countInBackpack;
}
