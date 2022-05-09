using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEngine.UI;

public enum ItemType { MeleeWeapon, RangedWeapon, Usingable, Arrow, Artefact, Other, Backpack }



public class Icon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected int _id;
    public int id { get => _id; }

    [SerializeField] protected ItemType _itemType; 
    public ItemType itemType { get => _itemType;  }

    [SerializeField] protected int _quantity;
    public int quantity { 
        get => _quantity; 
        set 
        {
            _quantity = value;
            if (numbered)
                quantityText.text = _quantity.ToString();
            if (_quantity == 0) Destroy(gameObject);
        } 
    }
    [SerializeField] protected int _price;
    public int price { get => _price*quantity; }

    [SerializeField] protected GameObject _droppedItem;
    public GameObject droppedItem { get => _droppedItem; }
    public bool questItem;

    [SerializeField] protected EventReference takeUpSound;
    [SerializeField] protected EventReference takeDownSound;
    [SerializeField] protected GameObject descriptionPanel;
    public Transform beforePosition { get; protected set; }
    protected EquipmentEffect equipmentEffect;
    protected UsingableItem usingEffect;
    protected Text quantityText;
    protected GameObject currentDescriptionPanel;
    public bool equipped { get; protected set; }
    public bool numbered { get => quantityText != null; }

    protected const float checkRadius = 40f;



    protected virtual void Start()
    {
        equipped = false;
        TryGetComponent(out equipmentEffect);
        TryGetComponent(out usingEffect);
        if (transform.parent.GetComponent<Slot>().slotForEquipment) PutOn();
        transform.Find("Text")?.TryGetComponent(out quantityText);
        if (quantityText != null) quantityText.text = _quantity.ToString();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        beforePosition = transform.parent;
        transform.SetParent(Canvas.singleton.transform);
        Library.Play2DSound(takeUpSound);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        PutToSlot();
        Library.Play2DSound(takeDownSound);
    }


    protected void PutToSlot()
    {
        Collider2D newSlot = Physics2D.OverlapCircle(transform.position, checkRadius);
        if (newSlot == null) Library.SetSlotPosition(this, beforePosition, beforePosition.GetComponent<Slot>().slotForEquipment);
        else
        {
            if (newSlot.TryGetComponent(out Bucket bucket))
            {
                if (!questItem) bucket.Drop(this);
                else Library.SetSlotPosition(this, beforePosition, beforePosition.GetComponent<Slot>().slotForEquipment);
                return;
            }
            newSlot.GetComponent<Slot>().Put(this);
        }
    }

    public void PutOn()
    {
        equipped = true;
        equipmentEffect?.PutOn();
    }

    public void PutOff()
    {
        equipped = false;
        equipmentEffect?.PutOff();
    }

    public void TryUse()
    {
        if (usingEffect.IsRecharged())
        {
            usingEffect.Use();
            if (quantityText != null) quantity--;
        }
        if (quantity == 0) Destroy(gameObject);
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentDescriptionPanel = Instantiate(descriptionPanel, DescriptionPanel.singleton.transform); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(currentDescriptionPanel);
    }

    private void OnDestroy()
    {
        if (currentDescriptionPanel != null) Destroy(currentDescriptionPanel);
    }

}
