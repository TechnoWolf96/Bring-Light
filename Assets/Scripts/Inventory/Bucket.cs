using UnityEngine;

public class Bucket : MonoBehaviour
{
    public void Drop(Icon item)
    {
        // ������� ������ �������
        Instantiate(item.droppedItem, Player.singleton.transform.position, Quaternion.identity)
            .GetComponent<DroppedItem>().quantity = item.quantity;
        if (item.equipped) item.PutOff();
        Destroy(item.gameObject);
    }
}
