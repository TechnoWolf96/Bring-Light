using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    [SerializeField] private ItemType _itemType;
    [SerializeField] private bool _slotForEquipment;

    private Image image;
    public ItemType itemType { get { return _itemType; } }
    public bool slotForEquipment { get { return _slotForEquipment; } }

    [SerializeField] private bool _unlocked;
    public bool uncloked
    {
        get => _unlocked;
        set
        {
            _unlocked = value; 
            if (_unlocked) image.color = new Color(0.73f, 0.69f, 0.6f, 0.75f);
            else image.color = new Color(0.3f, 0.28f, 0.24f, 0.75f);

        }
    }

    private void Awake()
    {
        image = GetComponentInParent<Image>();
    }


    public void Put(Icon newIcon)
    {
        // Если слот заблокирован - проверка провалена
        if (!uncloked)
        {
            Library.SetSlotPosition(newIcon, newIcon.beforePosition,
                     newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
            return;
        }

        if (transform.childCount != 0)
        {
            // Если слот уже занят, то проверка на совместимость идет для обоих иконок
            if (CheckCompatibility(this, newIcon) && 
                CheckCompatibility(newIcon.beforePosition.GetComponent<Slot>(),transform.GetChild(0).GetComponent<Icon>()))
            {
                // Проверка успешна
                Library.SetSlotPosition(newIcon, transform, slotForEquipment);
                Library.SetSlotPosition(transform.GetChild(0).GetComponent<Icon>(),
                    newIcon.beforePosition, newIcon.beforePosition.GetComponent<Slot>().slotForEquipment);
                
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
