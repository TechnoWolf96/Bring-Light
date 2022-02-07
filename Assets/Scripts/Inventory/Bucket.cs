using UnityEngine;

public class Bucket : MonoBehaviour
{
    public void Drop(Icon item)
    {
        // Сделать рандом позиции
        Instantiate(item.droppedItem, Player.singleton.transform.position, Quaternion.identity)
            .GetComponent<DroppedItem>().quantity = item.quantity;
        Weight.singleton.totalWeight -= item.weight;
        if (item.equipped) item.PutOff();
        Destroy(item.gameObject);
    }
}
