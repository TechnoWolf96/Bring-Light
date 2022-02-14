using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    public GameObject weapon { get => _weapon; }
}
