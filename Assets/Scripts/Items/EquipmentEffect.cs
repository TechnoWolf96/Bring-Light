using UnityEngine;

public abstract class EquipmentEffect : MonoBehaviour, IEquipment
{
    public abstract void PutOff();

    public abstract void PutOn();


}
