using System.Collections.Generic;
using UnityEngine;

public class InventoryInfoSlot : MonoBehaviour
{
    private static InventoryInfoSlot _singleton;
    public static InventoryInfoSlot singleton { get => _singleton; }
    public delegate void OnUseItem(int id);
    public event OnUseItem onUseItem;

    [SerializeField] private List<Slot> _backpackSlots; public List<Slot> backpackSlots { get => _backpackSlots; }
    [SerializeField] private List<Slot> _consumableSlots; public List<Slot> consumableSlots { get => _consumableSlots; }
    [SerializeField] private List<Slot> _artefactSlot; public List<Slot> artefactSlots { get => _artefactSlot; }
    [SerializeField] private List<Slot> _arrowSlots; public List<Slot> arrowSlots { get => _arrowSlots; }
    [SerializeField] private Slot _meleeWeaponSlot; public Slot meleeWeaponSlot { get => _meleeWeaponSlot; }
    [SerializeField] private Slot _rangedWeaponSlot; public Slot rangedWeaponSlot { get => _rangedWeaponSlot; }
    [SerializeField] private Slot _backpackSlot; public Slot backpackSlot { get => _backpackSlot; }
    [SerializeField] private Slot _petSlot; public Slot petSlot { get => _petSlot; }

    private void Awake()
    {
        _singleton = this;
    }


    public float GetTotalWeight()
    {
        float result = 0f;
        
        foreach (var item in _backpackSlots)
        {
            if (item.transform.childCount != 0)
                result += item.transform.GetChild(0).GetComponent<Icon>().weight;
        }
        foreach (var item in _consumableSlots)
        {
            if (item.transform.childCount != 0)
                result += item.transform.GetChild(0).GetComponent<Icon>().weight;
        }
        foreach (var item in _artefactSlot)
        {
            if (item.transform.childCount != 0)
                result += item.transform.GetChild(0).GetComponent<Icon>().weight;
        }
        foreach (var item in _arrowSlots)
        {
            if (item.transform.childCount != 0)
                result += item.transform.GetChild(0).GetComponent<Icon>().weight;
        }
        
        if (_meleeWeaponSlot.transform.childCount != 0)
            result += _meleeWeaponSlot.transform.GetChild(0).GetComponent<Icon>().weight;
        if (_rangedWeaponSlot.transform.childCount != 0)
            result += _rangedWeaponSlot.transform.GetChild(0).GetComponent<Icon>().weight;
        if (_backpackSlot.transform.childCount != 0)
            result += _backpackSlot.transform.GetChild(0).GetComponent<Icon>().weight;
        /*
        if (_petSlot.transform.childCount != 0)
            result += _petSlot.transform.GetChild(0).GetComponent<Icon>().weight;
        */
        return result;
    }

    public Slot GetEmptySlot()
    {
        foreach (var item in _backpackSlots)
        {
            if (item.transform.childCount == 0)
                return item;
        }
        return null;
    }

    public bool SearchNumberedIcon(int id, out Icon resultIcon)
    {
        foreach (var item in _consumableSlots)
        {
            if (item.transform.childCount != 0)
            {
                Icon icon = item.transform.GetChild(0).GetComponent<Icon>();
                if (icon.id == id)
                {
                    resultIcon = icon;
                    return true;
                }
            }
        }
        foreach (var item in _backpackSlots)
        {
            if (item.transform.childCount != 0)
            {
                Icon icon = item.transform.GetChild(0).GetComponent<Icon>();
                if (icon.id == id && icon.numbered)
                {
                    resultIcon = icon;
                    return true;
                }
            }
        }
        foreach (var item in _arrowSlots)
        {
            if (item.transform.childCount != 0)
            {
                Icon icon = item.transform.GetChild(0).GetComponent<Icon>();
                if (icon.id == id && icon.numbered)
                {
                    resultIcon = icon;
                    return true;
                }
            }
        }
        resultIcon = null;
        return false;
    }

    public void RechargeItemsWithId(int id)
    {
        onUseItem.Invoke(id);
    }

}
