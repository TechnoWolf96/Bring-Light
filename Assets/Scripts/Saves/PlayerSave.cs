using UnityEngine;
using System;

[Serializable]
public class PlayerSave
{
    
    float posX;
    float posY;
    int maxHealth;
    int health;
    int level;
    int currentExperience;
    int maxExperience;
    string _name;
    ItemQuantity[] backpackItems;
    [Serializable]
    private struct ItemQuantity
    {
        public int id;
        public int quantity;
    }

    public void Record()
    {
        backpackItems = new ItemQuantity[100];
        posX = Player.singleton.transform.position.x;
        posY = Player.singleton.transform.position.y;
        maxHealth = Player.singleton.maxHealth;
        health = Player.singleton.health;
        level = PlayerStatus.singleton.level;
        currentExperience = PlayerStatus.singleton.currentExperience;
        maxExperience = PlayerStatus.singleton.maxExperience;
        _name = PlayerStatus.singleton._name;
        for (int i = 0; i < InventoryInfoSlot.singleton.backpackSlots.Count; i++)
        {
            if (InventoryInfoSlot.singleton.backpackSlots[i].transform.childCount != 0)
            {
                Icon savingIcon = InventoryInfoSlot.singleton.backpackSlots[i].transform.GetChild(0).GetComponent<Icon>();
                backpackItems[i].id = savingIcon.id;
                backpackItems[i].quantity = savingIcon.quantity;
            }
            else
            {
                backpackItems[i].id = 0;
                backpackItems[i].quantity = 0;
            }
            // Сделать с другими
        
        }
    }

    public void Initial()
    {
        Vector3 position = new Vector3(posX, posY, 0f);
        Player.singleton.transform.position = position;
        Player.singleton.maxHealth = maxHealth;
        Player.singleton.health = health;
        PlayerStatus.singleton.level = level;
        PlayerStatus.singleton.currentExperience = currentExperience;
        PlayerStatus.singleton.maxExperience = maxExperience;
        PlayerStatus.singleton._name = _name;
        for (int i = 0; i < backpackItems.Length; i++)
        {
            if (backpackItems[i].id != 0)
            {
                Icon instIcon = GameObject.Instantiate
                    (IdSystem.singleton.icons[backpackItems[i].id], InventoryInfoSlot.singleton.backpackSlots[i].transform).GetComponent<Icon>();
                instIcon.quantity = backpackItems[i].quantity;
            }
            // Сделать с другими
        }
    }

    

}


