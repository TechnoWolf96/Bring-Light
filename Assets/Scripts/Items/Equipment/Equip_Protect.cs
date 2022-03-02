using UnityEngine;

public class Equip_Protect : EquipmentItem
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
