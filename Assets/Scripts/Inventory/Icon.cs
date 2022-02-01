using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEngine.UI;

public enum ItemType { MeleeWeapon, RangedWeapon, Usingable, Arrow, Artefact, Other, Backpack }



public class Icon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] protected ItemType _itemType;
    public ItemType itemType { get { return _itemType; } }
    [SerializeField] protected float _weight;
    public float weight { get { return _weight*quantity; } protected set { _weight = value; } }

    [SerializeField] protected int _quantity;
    public int quantity { get { return _quantity; } protected set { _quantity = value; quantityText.text = _quantity.ToString(); } }

    [SerializeField] protected EventReference takeUpSound;
    [SerializeField] protected EventReference takeDownSound;
    public Transform beforePosition { get; protected set; }
    protected Transform canvas;
    protected EquipmentItem equipmentEffect;
    protected UsingableItem usingEffect;
    protected Text quantityText;
    public bool equipped { get; protected set; }

    protected const float checkRadius = 40f;



    protected virtual void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        TryGetComponent(out equipmentEffect);
        TryGetComponent(out usingEffect);
        if (transform.parent.GetComponent<Slot>().slotForEquipment) PutOn();
        quantityText = transform.Find("Text").GetComponent<Text>();
        quantityText.text = quantity.ToString();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        beforePosition = transform.parent;
        transform.SetParent(canvas);
        Library.Play2DSound(takeUpSound);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        PutSlot();
        Library.Play2DSound(takeDownSound);
    }


    protected void PutSlot()
    {
        Collider2D newSlot = Physics2D.OverlapCircle(transform.position, checkRadius);
        if (newSlot == null) Library.SetSlotPosition(this, beforePosition, beforePosition.GetComponent<Slot>().slotForEquipment);
        else newSlot.GetComponent<Slot>().Put(this);
        
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
            quantity--;
        }
        if (quantity == 0) Destroy(gameObject);
            
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }


}
