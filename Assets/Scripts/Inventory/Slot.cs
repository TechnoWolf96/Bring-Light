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
            // ���� ���� ��� �����, �� �������� �� ������������� ���� ��� ����� ������
            if (CheckCompatibility(this, newIcon) && 
                CheckCompatibility(newIcon.beforePosition.GetComponent<Slot>(),transform.GetChild(0).GetComponent<Icon>()))
            {
                // �������� �������
                Library.SetSlotPosition(transform.GetChild(0).GetComponent<Icon>(),
                    newIcon.beforePosition, newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
                Library.SetSlotPosition(newIcon, transform, slotForEquipment);
            }
            else
            {
                // �������� ���������
                Library.SetSlotPosition(newIcon, newIcon.beforePosition,
                    newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
            }
            
        }
        // ����� ������ �������� �� ����� ����� �����, ��� �������, ��� ��� ����������
        else
        {
            if (CheckCompatibility(this, newIcon))
            {
                // ����������
                Library.SetSlotPosition(newIcon, transform, slotForEquipment);
            }
            else
            {
                // ������������
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
