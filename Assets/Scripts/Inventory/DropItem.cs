using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int id;
    public int quantity;
    public bool isNeed = true; // Показывает, был ли выброшен предмет только что
                                // и его нельзя поднять, только не отойдя на некоторое расстояние

    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindWithTag("Script").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isNeed)
        {
            quantity = inventory.Take(id, quantity);
            if (quantity <= 0) Destroy(gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isNeed = true;
    }



}