using UnityEngine;


public enum CurrentWeaponType { Ranged, Melee, Without }

public class PlayerWeaponChanger : MonoBehaviour
{
    private int _activeArrow = 0;
    public int activeArrow 
    { 
        get => _activeArrow; 
        set 
        { 
            _activeArrow = value;
            if (_activeArrow == 0) {selectedArrow1.SetActive(true); selectedArrow2.SetActive(false);}
            else { selectedArrow1.SetActive(false); selectedArrow2.SetActive(true);}
        }
    }

    public static PlayerWeaponChanger singleton { get; protected set; }
    public CurrentWeaponType currentWeaponType { get; protected set; }

    [SerializeField] private RuntimeAnimatorController _withoutWeaponAnimController;
    public RuntimeAnimatorController withoutWeaponAnimController { get => _withoutWeaponAnimController; }

    [SerializeField] private GameObject selectedArrow1;
    [SerializeField] private GameObject selectedArrow2;
    private ArrowItem currentArrowItem;
    private Icon currentArrowIcon;



    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        if (InventoryInfoSlot.singleton.rangedWeaponSlot.transform.childCount != 0)
            SelectWeapon(CurrentWeaponType.Ranged);
        else if (InventoryInfoSlot.singleton.meleeWeaponSlot.transform.childCount != 0)
            SelectWeapon(CurrentWeaponType.Melee);
        else SelectWeapon(CurrentWeaponType.Without);
        SelectArrows(activeArrow = 0);

    }


    public void SelectWeapon(CurrentWeaponType newWeaponType)
    {
        switch (newWeaponType)
        {
            case CurrentWeaponType.Ranged:
                ChangeWeapon
                    (InventoryInfoSlot.singleton.rangedWeaponSlot.transform.GetChild(0).GetComponent<WeaponItem>().weapon);
                currentWeaponType = CurrentWeaponType.Ranged;
                break;
            case CurrentWeaponType.Melee:
                ChangeWeapon
                    (InventoryInfoSlot.singleton.meleeWeaponSlot.transform.GetChild(0).GetComponent<WeaponItem>().weapon);
                currentWeaponType = CurrentWeaponType.Melee;
                break;
            case CurrentWeaponType.Without:
                ChangeWeapon();
                currentWeaponType = CurrentWeaponType.Without;
                break;

        }
    }

    private void ChangeWeapon(GameObject newWeapon = null)
    {
        if (Player.singleton.currentWeapon != null) Destroy(Player.singleton.currentWeapon.gameObject);
        // При null игрок становится безоружным
        if (newWeapon == null)
        {
            Player.singleton.anim.runtimeAnimatorController = withoutWeaponAnimController;
            return;
        }
        Player.singleton.currentWeapon = Instantiate(newWeapon, Player.singleton.weaponSlot).GetComponent<PlayerWeapon>();
        Player.singleton.anim.runtimeAnimatorController = Player.singleton.currentWeapon.animController;
    }

    public GameObject GetAndUseArrow()
    {
        ArrowCounter.singleton.count = --currentArrowIcon.quantity;
        return currentArrowItem.arrow;
    }

    public void SelectArrows(int slotNumber)
    {
        activeArrow = slotNumber;
        if (InventoryInfoSlot.singleton.arrowSlots[activeArrow].transform.childCount != 0)
        {
            currentArrowItem = InventoryInfoSlot.singleton.arrowSlots[activeArrow].transform.GetChild(0).GetComponent<ArrowItem>();
            currentArrowIcon = InventoryInfoSlot.singleton.arrowSlots[activeArrow].transform.GetChild(0).GetComponent<Icon>();
            ArrowCounter.singleton.count = currentArrowIcon.quantity;
            ArrowCounter.singleton.sprite = currentArrowItem.arrowImage;
        }
        else ArrowCounter.singleton.RemoveArrows();
    }

}
