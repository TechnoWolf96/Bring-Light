using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItem : MonoBehaviour, IEquipment
{
    protected Player player;

    public abstract void PutOff();

    public abstract void PutOn();


    
    protected virtual void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

}
