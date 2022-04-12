using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _quantity;
    [SerializeField] private bool _numbered;
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
            
        Destroy(gameObject);

    }


}
