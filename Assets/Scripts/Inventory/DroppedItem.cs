using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _quantity;
    [SerializeField] private bool _numbered;
    private bool nowDropped;

    public int quantity { get => _quantity; set => _quantity = value; }

    public void Take()
    {
        if (_numbered && InventoryInfoSlot.singleton.SearchNumberedIcon(_id, out Icon icon))
            icon.quantity += quantity;
        else
        {
            Slot emptySlot = InventoryInfoSlot.singleton.GetEmptySlot();
            if (emptySlot == null) return;
            Instantiate(IdSystem.singleton.icons[_id], emptySlot.transform).GetComponent<Icon>().quantity = _quantity;
        }

        Library.Play2DSound(CommonSounds.singleton.takeItem);
        Destroy(gameObject);

    }

    private void Start()
    {
        nowDropped = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var item in colliders)
        {
            if (item.CompareTag("PlayerPhysicalSupport")) nowDropped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPhysicalSupport") && !nowDropped) Take();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPhysicalSupport")) nowDropped = false;
    }


}
