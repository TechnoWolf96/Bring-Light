using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _quantity;
    [SerializeField] private bool _numbered;
    public int quantity { get => _quantity; set => _quantity = value; }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
            Take();
    }


    private void Take()
    {
        if (_numbered && InventoryInfoSlot.singleton.SearchNumberedIcon(_id, out Icon icon))
            icon.quantity += quantity;
        else
        {
            //Weight.singleton.totalWeight += IdSystem.singleton.icons[_id].GetComponent<Icon>().weight;
            Instantiate(IdSystem.singleton.icons[_id], InventoryInfoSlot.singleton.GetEmptySlot().transform).
                GetComponent<Icon>().quantity = _quantity;
        }
            
        Destroy(gameObject);

    }


}
