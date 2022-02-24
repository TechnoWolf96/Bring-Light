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
        if (backpack.activeInHierarchy) Player.singleton.controled = false;
         
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
        Player.singleton.controled = false;
        backpack.SetActive(true);
        equipment.SetActive(true);
    }

    private void Close()
    {
        Player.singleton.controled = true;
        backpack.SetActive(false);
        equipment.SetActive(false);
    }



}
