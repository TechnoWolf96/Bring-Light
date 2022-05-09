using UnityEngine;

public class HealPotion_UsingableItem : UsingableItem
{
    public int heal;
    public GameObject particles;

    public override void Use()
    {
        base.Use();
        Player.singleton.health += heal;
        Instantiate(particles, Player.singleton.transform);
    }
}
