using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventory : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    private GameObject backpack;
    private GameObject equipment;

    private void Start()
    {
        backpack = GameObject.Find("Canvas/Inventory/BackpackPanel");
        equipment = GameObject.Find("Canvas/Inventory/Equipment");
         
    }


    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (backpack.activeInHierarchy) Close();
            else Open();
        }
    }

    private void Open()
    {
        backpack.SetActive(true);
        equipment.SetActive(true);
    }

    private void Close()
    {
        backpack.SetActive(false);
        equipment.SetActive(false);
    }



}
