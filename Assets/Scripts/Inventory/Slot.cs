using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    [SerializeField] private ItemType _itemType;
    [SerializeField] private bool _slotForEquipment;
    public ItemType itemType { get { return _itemType; } }
    public bool slotForEquipment { get { return _slotForEquipment; } }


    public void Put(Icon newIcon)
    {
        if (transform.childCount != 0)
        {
            // Если слот уже занят, то проверка на совместимость идет для обоих иконок
            if (CheckCompatibility(this, newIcon) && 
                CheckCompatibility(newIcon.beforePosition.GetComponent<Slot>(),transform.GetChild(0).GetComponent<Icon>()))
            {
                // Проверка успешна
                Library.SetSlotPosition(transform.GetChild(0).GetComponent<Icon>(),
                    newIcon.beforePosition, newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
                Library.SetSlotPosition(newIcon, transform, slotForEquipment);
            }
            else
            {
                // Проверка провалена
                Library.SetSlotPosition(newIcon, newIcon.beforePosition,
                    newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
            }
            
        }
        // Новая иконка ставится на место этого слота, при условии, что она совместима
        else
        {
            if (CheckCompatibility(this, newIcon))
            {
                // Совместима
                Library.SetSlotPosition(newIcon, transform, slotForEquipment);
            }
            else
            {
                // Несовместима
                Library.SetSlotPosition(newIcon, newIcon.beforePosition,
                    newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
            }

        }
    }


    private bool CheckCompatibility(Slot slot, Icon icon)
    {
        if (slot.itemType == ItemType.Other) return true;
        if (slot.itemType == icon.itemType) return true;
        return false;


        
    }





}
