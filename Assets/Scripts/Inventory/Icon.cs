using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum ItemType { Weapon, Consumable, Ring, Armor, Helmet, Pendant, Backpack, Other, Rechargeable}

public class Icon : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Icon parameters:")]
    public int id;
    public GameObject equipItem;
    public ItemType itemType;
    public int count;
    [HideInInspector] public bool isEquipped = false;
    [Header("Description:")]
    [SerializeField] private GameObject descriptionPanel;

    private Text countText;
    private Transform canvas;
    private Inventory inventory;
    private Transform beforePosition;
    private const float checkSlotRadius = 50f; // ������ ��������� � ���� ��������
    private const float checkBucketRadius = 75f;
    private const float waitDescription = 0.7f;
    private bool wait = false; // ���� �� ����� ��������� ��������
    private bool draging = false; // ����� �� ������� � ������ ������

    private void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        inventory = GameObject.FindWithTag("Script").GetComponent<Inventory>();
    }


    public void OnCreate() // ������� ��������� �� ������� ������� ��� �������� ������ � ������
    {
        countText = GetComponentInChildren<Text>();
        UpdateCount();
    }


    public void UpdateCount()
    {
        countText.text = count.ToString();
    }

    public void ShowDescription()
    {
        descriptionPanel.SetActive(true);
        descriptionPanel.transform.SetParent(canvas);
    }

    public void HideDescription()
    {
        descriptionPanel.SetActive(false);
        descriptionPanel.transform.SetParent(transform);

    }

    private void OnMouseDown()
    {
        print("GetMouse");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return; // ����� ������ ��� ������� ���
        if (itemType != ItemType.Backpack) // ������� ������������� ������
        {
            HideDescription();
            beforePosition = transform.parent;
            transform.SetParent(canvas);
        }
        wait = false; // ������ �� ������ ��������, ����� ����� �������

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        draging = true;
        if (itemType != ItemType.Backpack) 
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (itemType != ItemType.Backpack) SearchSlot();
        draging = false;
        wait = true; // ���� ��������� ������ � �� ��� ��� �� �� ��� ��������, �� ���������� ��������
        StartCoroutine(WaitToShowDescription()); 
    }


    private void SearchSlot() // ����� ���������� ����� � ������������ � ����� �������� (��� ������ ��������)
    {
        for (int i = 0; i < inventory.maxSlot; i++) // ����� � ������� ��� ������ ��������
        {
            if (Vector2.Distance(inventory.slotsInBackpack[i].transform.position, transform.position) < checkSlotRadius)
            {
                if (itemType == ItemType.Consumable || itemType == ItemType.Other)
                    StandSlotNumbered(inventory.slotsInBackpack[i].transform);
                else
                    StandSlotNotNumbered(inventory.slotsInBackpack[i].transform);
                return;
            }
        }
                
        switch (itemType) // ����� ������������������� �����
        {
            case ItemType.Weapon:
                for (int i = 0; i < inventory.slotsWeapon.Count; i++)
                {
                    if (Vector2.Distance(inventory.slotsWeapon[i].transform.position, transform.position) < checkSlotRadius)
                    {
                        if (inventory.activeWeapon == i) StandSlotNotNumbered(inventory.slotsWeapon[i].transform, slotForEquipment: true);
                        else StandSlotNotNumbered(inventory.slotsWeapon[i].transform);
                        return;
                    }
                }
                break;
            case ItemType.Consumable:
                foreach (var item in inventory.slotsConsumable)
                    if (Vector2.Distance(item.transform.position, transform.position) < checkSlotRadius)
                    {
                        StandSlotNumbered(item.transform);
                        return;
                    }
                break;
            case ItemType.Rechargeable:
                foreach (var item in inventory.slotsConsumable)
                    if (Vector2.Distance(item.transform.position, transform.position) < checkSlotRadius)
                    {
                        StandSlotNotNumbered(item.transform);
                        return;
                    }
                break;
            case ItemType.Ring:
                if (Vector2.Distance(inventory.slotRing.transform.position, transform.position) < checkSlotRadius)
                {
                    StandSlotNotNumbered(inventory.slotRing.transform, slotForEquipment: true);
                    return;
                }
                break;
            case ItemType.Armor:
                if (Vector2.Distance(inventory.slotArmor.transform.position, transform.position) < checkSlotRadius)
                {
                    StandSlotNotNumbered(inventory.slotArmor.transform, slotForEquipment: true);
                    return;
                }
                break;
            case ItemType.Helmet:
                if (Vector2.Distance(inventory.slotHelmet.transform.position, transform.position) < checkSlotRadius)
                {
                    StandSlotNotNumbered(inventory.slotHelmet.transform, slotForEquipment: true);
                    return;
                }
                break;
            case ItemType.Pendant:
                if (Vector2.Distance(inventory.slotPendant.transform.position, transform.position) < checkSlotRadius)
                {
                    StandSlotNotNumbered(inventory.slotPendant.transform, slotForEquipment: true);
                    return;
                }
                break;
            case ItemType.Backpack:
                if (Vector2.Distance(inventory.slotBackpack.transform.position, transform.position) < checkSlotRadius)
                {
                    StandSlotNotNumbered(inventory.slotBackpack.transform, slotForEquipment: true);
                    return;
                }
                break;
        }
        if (!inventory.ScanSlots(transform, checkSlotRadius)) // ���� �� ������ �� �� ������ �����
        {
            inventory.Drop(id, count);
            if (isEquipped) inventory.DeleteEquipped(itemType);
            Destroy(gameObject);
        }
        else
        {
            transform.position = beforePosition.position; // ���� �� ������� �����, �� ���������� �� ������ �������
            transform.SetParent(beforePosition);
            if (isEquipped) inventory.ChangeEquipment(this); // ���������� �����, ���� ����������
        }
    }


    private void StandSlotNotNumbered(Transform newPosition, bool slotForEquipment = false) // ������ ���� �� ������ �������
    {
        if (newPosition.childCount != 0) // ������ ������
        {
            Icon alreadyIcon = newPosition.GetComponentInChildren<Icon>();
            if ((beforePosition.position == inventory.slotsWeapon[0].transform.position ||
                beforePosition.position == inventory.slotsWeapon[1].transform.position) &&
                alreadyIcon.itemType != itemType) // ���� ����� �� ����� ������ �� ����� � ����� �������� �� ������
            {
                transform.position = beforePosition.position; // ���������� ������ �� ������ �������
                transform.parent = beforePosition;
                return;
            }
            if (isEquipped && alreadyIcon.itemType != itemType) // ���� ����� �� ������ �� ����� � ������ ����� ��������
            {
                transform.position = beforePosition.position; // ���������� ������ �� ������ �������
                transform.parent = beforePosition;
                return;
            }

            if (alreadyIcon.isEquipped) // �� ����������� � ��������
            {
                inventory.ChangeEquipment(this, alreadyIcon);
                goto skipNextCondition;
            }
            if (isEquipped) // �� ��������� � ����������
                inventory.ChangeEquipment(alreadyIcon, this);

            skipNextCondition:
            
            alreadyIcon.transform.position = beforePosition.position; // ������ �������� �������
            alreadyIcon.transform.SetParent(beforePosition);
        }
        else // ������ �� ������
        {
            if (isEquipped && !slotForEquipment) // �� ��������� � ����������
            {
                inventory.DeleteEquipped(itemType); 
                isEquipped = false; 
            }
            if (!isEquipped && slotForEquipment) inventory.ChangeEquipment(this); // �� ����������� � ��������
        }
        transform.position = newPosition.position;
        transform.SetParent(newPosition);
    }

    private void StandSlotNumbered(Transform newPosition)
    {
        if (newPosition.childCount != 0) // ������ ������
        {
            Icon alreadyIcon = newPosition.GetComponentInChildren<Icon>();
            if (alreadyIcon.id == id) // ���� �������� ����� �������
            {
                AddInStack(alreadyIcon);
                transform.position = beforePosition.position;
                transform.SetParent(beforePosition);
            }
            else
            {
                alreadyIcon.transform.position = beforePosition.position; // ������ �������� �������
                alreadyIcon.transform.SetParent(beforePosition);
                transform.position = newPosition.position;
                transform.SetParent(newPosition);
            }

        }
        else // ������ �� ������
        {
            transform.position = newPosition.position;
            transform.SetParent(newPosition);
        }
        
    }

    private void AddInStack(Icon toIcon) 
    {
        toIcon.count += count;
        if (toIcon.count > inventory.stack)
        {
            count = toIcon.count - inventory.stack;
            UpdateCount();
            toIcon.count = inventory.stack;
            toIcon.UpdateCount();
            
        }
        else
        {
            toIcon.UpdateCount();
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkSlotRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkBucketRadius);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!draging) 
        {
            print("Enter point");
            wait = true;
            StartCoroutine(WaitToShowDescription());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        wait = false;
        HideDescription();
    }

    IEnumerator WaitToShowDescription()
    {
        yield return new WaitForSeconds(waitDescription);
        if (wait) ShowDescription();
    }
}

