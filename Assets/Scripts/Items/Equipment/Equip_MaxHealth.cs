

public class Equip_MaxHealth : EquipmentItem
{
    public int bonusMaxHealth;

    public override void PutOff()
    {
        Player.singleton.maxHealth -= bonusMaxHealth;
    }

    public override void PutOn()
    {
        Player.singleton.maxHealth += bonusMaxHealth;
    }
}
