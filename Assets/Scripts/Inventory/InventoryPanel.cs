using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel singleton { get; private set; }
    private GameObject backpack;
    private GameObject equipment;


    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        backpack = GameObject.Find("Canvas/Inventory/BackpackPanel");
        equipment = GameObject.Find("Canvas/Inventory/Equipment");
        if (backpack.activeInHierarchy) Control.singleton.playerControlActive = false;
         
    }


    public void OpenOrClose()
    {
        if (backpack.activeInHierarchy) Close();
        else Open();
    }

    private void Open()
    {
        Control.singleton.playerControlActive = false;
        backpack.SetActive(true);
        equipment.SetActive(true);
    }

    private void Close()
    {
        Control.singleton.playerControlActive = true;
        backpack.SetActive(false);
        equipment.SetActive(false);
    }



}
