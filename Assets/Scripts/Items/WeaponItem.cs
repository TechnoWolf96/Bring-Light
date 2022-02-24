using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponItem : MonoBehaviour, IBeginDragHandler
{
    private enum WeaponType { Ranged, Melee}

    [SerializeField] private GameObject _weapon;
    [SerializeField] private WeaponType weaponType;
    public GameObject weapon { get => _weapon; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Icon>().equipped && weaponType.ToString() == PlayerWeaponChanger.singleton.currentWeaponType.ToString())
            PlayerWeaponChanger.singleton.SelectWeapon(CurrentWeaponType.Without);
    }
}
