using UnityEngine;

public class ArrowItem : EquipmentEffect
{
    [SerializeField] private GameObject _arrow;
    public GameObject arrow { get => _arrow; }
    [SerializeField] private Sprite _arrowImage;
    public Sprite arrowImage { get => _arrowImage;}

    public override void PutOff()
    {
        PlayerWeaponChanger.singleton.SelectArrows(PlayerWeaponChanger.singleton.activeArrow);
    }

    public override void PutOn()
    {
        PlayerWeaponChanger.singleton.SelectArrows(PlayerWeaponChanger.singleton.activeArrow);
    }

    private void OnDestroy()
    {
        ArrowCounter.singleton.RemoveArrows();
    }
}
