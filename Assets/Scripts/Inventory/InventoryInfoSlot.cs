using System.Collections.Generic;
using UnityEngine;

public class InventoryInfoSlot : MonoBehaviour
{
    private static InventoryInfoSlot _singleton;
    public static InventoryInfoSlot singleton { get => _singleton; }
    public delegate void OnUseItem(int id);
    public event OnUseItem onUseItem;
    [SerializeField] private int _unlockedSlots;
    public int unlockedSlots
    {
        get => _unlockedSlots;
        set
        {
            _unlockedSlots = value;
            UpdateSlots();
        }
    }

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
    private void Start()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (var slot in backpackSlots)
        {
            slot.uncloked = false;
        }

        for (int i = 0; i < unlockedSlots; i++)
        {
            backpackSlots[i].uncloked = true;
        }

    }

    public Slot GetEmptySlot()
    {
        for (int i = 0; i < unlockedSlots; i++)
            if (_backpackSlots[i].transform.childCount == 0) return _backpackSlots[i];

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
