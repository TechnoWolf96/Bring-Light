

public class MaxHealth_EquipmentEffect : EquipmentEffect
{
    public int bonusMaxHealth;

    public override void PutOff()
    {
        Player.singleton.health -= bonusMaxHealth;
        if (Player.singleton.health <= 0) Player.singleton.health = 1;
        Player.singleton.maxHealth -= bonusMaxHealth;
        
    }

    public override void PutOn()
    {
        Player.singleton.maxHealth += bonusMaxHealth;
        Player.singleton.health += bonusMaxHealth;
    }
}
