using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEngine.UI;

public enum ItemType { MeleeWeapon, RangedWeapon, Usingable, Arrow, Artefact, Other, Backpack }



public class Icon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] protected int _id;
    public int id { get; }

    [SerializeField] protected ItemType _itemType; 
    public ItemType itemType { get => _itemType;  }

    [SerializeField] protected float _weight; 
    public float weight { get => _weight * quantity;}

    [SerializeField] protected int _quantity;
    public int quantity { 
        get => _quantity; 
        protected set 
        {
            float otherWeight = Weight.singleton.totalWeight - weight;
            _quantity = value;
            Weight.singleton.totalWeight = otherWeight + weight;
            if (quantityText != null)
                quantityText.text = _quantity.ToString();
        } 
    }

    [SerializeField] protected GameObject _droppedItem;
    public GameObject droppedItem { get => _droppedItem; }
    public bool questItem;

    [SerializeField] protected EventReference takeUpSound;
    [SerializeField] protected EventReference takeDownSound;
    public Transform beforePosition { get; protected set; }
    protected EquipmentItem equipmentEffect;
    protected UsingableItem usingEffect;
    protected Text quantityText;
    public bool equipped { get; protected set; }

    protected const float checkRadius = 40f;



    protected virtual void Start()
    {
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


}
