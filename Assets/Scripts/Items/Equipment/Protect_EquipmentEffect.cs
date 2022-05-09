using UnityEngine;

public class Protect_EquipmentEffect : EquipmentEffect
{
    [SerializeField] private ProtectParameters bonusProtect;

    public override void PutOff()
    {
        Player.singleton.protect -= bonusProtect;
    }

    public override void PutOn()
    {
        Player.singleton.protect += bonusProtect;
    }
}
