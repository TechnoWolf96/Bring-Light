using System;
using System.Collections.Generic;
using UnityEngine;




public class DeathDropItem : MonoBehaviour
{
    [Serializable]
    private struct DropItem
    {
        public int id;
        public int minQuantity;
        public int maxQuantity;
        public int probability;
    }

    [SerializeField] private List<DropItem> dropItems;

    private void Start()
    {
        GetComponent<Creature>().onDeath += Drop;
    }


    private void Drop()
    {
        foreach (var item in dropItems)
        {
            if (item.probability >= UnityEngine.Random.Range(0, 101))
            {
                int quantity = UnityEngine.Random.Range(item.minQuantity, item.maxQuantity+1);
                Instantiate(IdSystem.singleton.icons[item.id].GetComponent<Icon>().droppedItem, transform.position, Quaternion.identity).
                    GetComponent<DroppedItem>().quantity = quantity;
            }
                
        }
    }




}
