using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inventory : MonoBehaviour
{
    public InventorySriptableObject save;
    public int maxSlot;

    [Header("Slot space:")]
    public List<GameObject> slotsInBackpack;
    public List<GameObject> slotsConsumable;
    public List<GameObject> slotsWeapon;
    public GameObject slotHelmet;
    public GameObject slotArmor;
    public GameObject slotRing;
    public GameObject slotPendant;
    public GameObject slotBackpack;

    private IdSystem id;
    private Transform playerPosition;
    private Animator anim; // Анимация открытия/закрытия инвентаря
    private bool isOpen = false; // Открыт ли инвентарь
    public int activeWeapon; // Номер активной ячейки для оружия (0 - левая, 1 - правая)

    public int stack = 9; // Количество предметов в стаке


    private void Start()
    {
        id = GetComponent<IdSystem>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        anim = GameObject.Find("Canvas/Inventory").GetComponent<Animator>();
        LoadInventory();
        ChangeColorSlots();
    }

    

    public void SaveInventory()
    {
        int index = 0;
        save.activeWeapon = activeWeapon;
        for (int i = 0; i < slotsWeapon.Count; i++)
        {
            Icon icon = slotsWeapon[i].GetComponentInChildren<Icon>() ?? new Icon();
            save.idEquipment[index++] = icon.id;
        }
        for (int i = 0; i < slotsConsumable.Count; i++)
        {
            Icon icon = slotsConsumable[i].GetComponentInChildren<Icon>() ?? new Icon();
            save.idEquipment[index] = icon.id;
            save.countEquipment[index++] = icon.count;
        }
        save.idEquipment[index++] = slotHelmet.GetComponentInChildren<Icon>()?.id ?? 0;
        save.idEquipment[index++] = slotArmor.GetComponentInChildren<Icon>()?.id ?? 0;
        save.idEquipment[index++] = slotRing.GetComponentInChildren<Icon>()?.id ?? 0;
        save.idEquipment[index++] = slotPendant.GetComponentInChildren<Icon>()?.id ?? 0;
        save.idEquipment[index++] = slotBackpack.GetComponentInChildren<Icon>()?.id ?? 0;
        for (int i = 0; i < slotsInBackpack.Count; i++)
        {
            Icon icon = slotsInBackpack[i].GetComponentInChildren<Icon>() ?? new Icon();
            save.idInBackpack[i] = icon.id;
            save.countInBackpack[i] = icon.count;
        }


    }

    public void ChangeColorSlots() // Изменение цвета слотов в инвентаре
    {
        for (int i = 0; i < maxSlot; i++)
            slotsInBackpack[i].GetComponent<Image>().color = Color.white;
        for (int i = maxSlot; i < slotsInBackpack.Count; i++)
            slotsInBackpack[i].GetComponent<Image>().color = Color.gray;
    }

    public void LoadInventory() // Создание иконок на пустом инвентаре и создание эквипа на герое
    {
        for (int i = 0; i < save.idEquipment.Length; i++)
        {
            if (save.idEquipment[i] == 0) continue; // Если в сохраненном инвентаре 0, то ячейку оставляем пустой
            GameObject icon_obj = id.icons[save.idEquipment[i]];
            Icon icon = icon_obj.GetComponent<Icon>();
            switch (icon.itemType)
            {
                case ItemType.Helmet:
                    Instantiate(icon_obj, slotHelmet.transform);
                    ChangeEquipment(icon);
                    break;
                case ItemType.Armor: 
                    Instantiate(icon_obj, slotArmor.transform);
                    ChangeEquipment(icon);
                    break;
                case ItemType.Pendant: 
                    Instantiate(icon_obj, slotPendant.transform);
                    ChangeEquipment(icon);
                    break;
                case ItemType.Ring: 
                    Instantiate(icon_obj, slotRing.transform);
                    ChangeEquipment(icon);
                    break;
                case ItemType.Backpack: 
                    Instantiate(icon_obj, slotBackpack.transform);
                    ChangeEquipment(icon);
                    break;
                case ItemType.Weapon:
                        for (int j = 0; j < slotsWeapon.Count; j++)
                        {
                            if (slotsWeapon[j].transform.childCount == 0)
                            {
                                Instantiate(icon_obj, slotsWeapon[j].transform);
                                if (j == activeWeapon)
                                {
                                    slotsWeapon[i].GetComponent<Image>().color = Color.white; // Белая ячейка с активным оружием 
                                    ChangeEquipment(icon);
                                }
                                break;
                            }
                        }
                        break;
                case ItemType.Consumable:
                        for (int j = 0; j < slotsConsumable.Count; j++)
                        {
                            if (slotsConsumable[j].transform.childCount == 0)
                            {
                                Icon inst = Instantiate(icon_obj, slotsConsumable[j].transform).GetComponent<Icon>();
                                inst.count = save.countEquipment[i];
                                inst.OnCreate();
                                break;
                            }
                        }
                        break;
            }
        }
        for (int i = 0; i < save.idInBackpack.Length; i++)
        {
            if (save.idInBackpack[i] == 0) continue;
            Icon inst = Instantiate(id.icons[save.idInBackpack[i]], slotsInBackpack[i].transform).GetComponent<Icon>();
            if (inst.itemType == ItemType.Consumable || inst.itemType == ItemType.Other)
            {
                inst.count = save.countInBackpack[i];
                inst.OnCreate();
            }
            
        }
    }

    public int Take(int idItem, int quantity) // Возвращает число предметов, не поместившихся в инвентаре
    {
        Icon icon = id.icons[idItem].GetComponent<Icon>();
        if (icon.itemType == ItemType.Consumable || icon.itemType == ItemType.Other)
            return SearchNumbered(idItem, quantity);
        if (icon.itemType == ItemType.Backpack)
        {
            if (icon.equipItem.GetComponent<Backpack>().maxSlot > maxSlot)
            {
                ChangeBackpackIcon(idItem);
                ChangeEquipment(icon);
                return 0;
            }
            return 1;
        }
            
        return SearchNotNumbered(idItem);
    }
    public void Drop(int idItem, int quantity = 0) // Выброс предмета
    {
        DropItem drop = Instantiate(id.dropItems[idItem], playerPosition.position, Quaternion.identity).GetComponent<DropItem>();
        drop.isNeed = false;
        drop.quantity = quantity;
    }

    private void ChangeBackpackIcon(int idBackpack) // Взятие рюкзака
    {
        try
        {
            Destroy(slotBackpack.GetComponentInChildren<GameObject>());
        } 
        catch { }
        Instantiate(id.icons[idBackpack], slotBackpack.transform);
    }

    private int SearchNumbered(int idItem, int quantity)
    {
        // Для иконок с цифрой перебираем инвентарь до тех пор, пока не найдем идентичную иконку:
        // 1) Иконку с цифрой меньше константы stack - увеличиваем число в иконке на quantity
        // 2) Если эта иконка переполнена, то циклом с того же места ищем новую иконку - она должна быть справа, или соединена через другие предметы справа
        // 3) Если снова стак переполнен, то возвращаемся к 2
        // 4) Если идентичной иконки нет, то ищем заново пустой слот и добавляем все туда
        for (int i = 0; i < maxSlot; i++)
        {
            Icon slot = slotsInBackpack[i].GetComponentInChildren<Icon>() ?? new Icon();
            if (slot.id == idItem && slot.id != 0)
            {
                if ((slot.count + quantity) <= stack) // (1)
                {
                    slot.count += quantity;
                    slot.OnCreate();
                    return 0;
                }
                else // (2)
                {
                    quantity -= stack - slot.count;
                    slot.count = stack;
                    slot.OnCreate();
                    continue; // (3)
                }
            }
        }
        for (int i = 0; i < maxSlot; i++) // (4)
        {
            if (slotsInBackpack[i].transform.childCount == 0)
            {
                Icon inst = Instantiate(id.icons[idItem], slotsInBackpack[i].transform).GetComponent<Icon>();
                inst.count = quantity;
                inst.OnCreate();
                return 0;
            }
        }
        return quantity;
    }

    private int SearchNotNumbered(int idItem)
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (slotsInBackpack[i].transform.childCount == 0)
            {
                Instantiate(id.icons[idItem], slotsInBackpack[i].transform).GetComponent<Icon>().isEquipped = false;
                return 0;
            }
        }
        return 1;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SaveInventory();
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeWeapon();
        if (Input.GetKeyDown(KeyCode.E))
            OpenClose();
    }

    public void ChangeEquipment(Icon setActive, Icon setNoActive = null) // Смена экипировки или ее создание на персонаже (Если отстутсвует)
    {
        setActive.isEquipped = true;
        if (setNoActive != null) setNoActive.isEquipped = false;
        DeleteEquipped(setActive.itemType);
        Instantiate(setActive.equipItem, playerPosition);
    }

    public void ChangeWeapon()
    {
        Icon first = slotsWeapon[0].GetComponentInChildren<Icon>();
        Icon second = slotsWeapon[1].GetComponentInChildren<Icon>();
        if (activeWeapon == 0 && slotsWeapon[1].transform.childCount != 0)
        {
            ChangeEquipment(second, first);
            slotsWeapon[1].GetComponent<Image>().color = Color.white;
            slotsWeapon[0].GetComponent<Image>().color = Color.gray;
            activeWeapon = 1;
            return;
        }
        if (activeWeapon == 1 && slotsWeapon[0].transform.childCount != 0)
        {
            ChangeEquipment(first, second);
            slotsWeapon[0].GetComponent<Image>().color = Color.white;
            slotsWeapon[1].GetComponent<Image>().color = Color.gray;
            activeWeapon = 0;
            return;
        }
    }

    public void DeleteEquipped(ItemType itemType) // Удаление предмета определенного типа с игрока (Если существует)
    {
        GameObject equippedItem = null;
        switch (itemType)
        {
            case ItemType.Weapon:
                equippedItem = playerPosition.GetComponentInChildren<Weapon>()?.gameObject;
                break;
            case ItemType.Ring:
                equippedItem = playerPosition.GetComponentInChildren<Ring>()?.gameObject;
                break;
            case ItemType.Armor:
                equippedItem = playerPosition.GetComponentInChildren<Armor>()?.gameObject;
                break;
            case ItemType.Helmet:
                equippedItem = playerPosition.GetComponentInChildren<Helmet>()?.gameObject;
                break;
            case ItemType.Pendant:
                equippedItem = playerPosition.GetComponentInChildren<Pendant>()?.gameObject;
                break;
            case ItemType.Backpack:
                equippedItem = playerPosition.GetComponentInChildren<Backpack>()?.gameObject;
                break;
        }
        if (equippedItem != null)
            Destroy(equippedItem);
    }

    public bool ScanSlots(Transform position, float radius) // Проверка на нахождение вокруг заданной точки слотов
    {
        bool result = false;
        foreach (var item in slotsInBackpack)
            if (Vector2.Distance(item.transform.position, position.position) < radius)
                result = true;
        foreach (var item in slotsWeapon)
            if (Vector2.Distance(item.transform.position, position.position) < radius)
                result = true;
        foreach (var item in slotsConsumable)
            if (Vector2.Distance(item.transform.position, position.position) < radius)
                result = true;
        if (Vector2.Distance(slotHelmet.transform.position, position.position) < radius)
            result = true;
        if (Vector2.Distance(slotArmor.transform.position, position.position) < radius)
            result = true;
        if (Vector2.Distance(slotRing.transform.position, position.position) < radius)
            result = true;
        if (Vector2.Distance(slotPendant.transform.position, position.position) < radius)
            result = true;
        if (Vector2.Distance(slotBackpack.transform.position, position.position) < radius)
            result = true;

        return result;
    }

    private void OpenClose()
    {
        if (isOpen) anim.SetBool("Open", false);
        else anim.SetBool("Open", true);
        isOpen = !isOpen;

    }


}