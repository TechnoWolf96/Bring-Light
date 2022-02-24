using UnityEngine;

public abstract class EquipmentItem : MonoBehaviour, IEquipment
{
    public abstract void PutOff();

    public abstract void PutOn();


}
