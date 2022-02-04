using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public void Drop(Icon item)
    {
        Instantiate(item.droppedItem, Player.singleton.transform.position, Quaternion.identity);
        Weight.singleton.totalWeight -= item.weight;
        Destroy(item.gameObject);
    }
}
