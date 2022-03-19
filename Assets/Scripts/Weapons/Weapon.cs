using UnityEngine;
using FMODUnity;

public interface IAttackWithWeapon
{
    public void AttackMoment();
}


// ������ �� �������� Weapon ������ ���� �������� � �������� � ������� Weapon
public abstract class Weapon : MonoBehaviour
{
    
    [Header("Weapon:")]
    [SerializeField] protected RuntimeAnimatorController _animController;
    [SerializeField] protected LayerMask layer;
    public RuntimeAnimatorController animController { get => _animController; }
    protected Creature owner;

    public abstract void Attack();


    protected virtual void Start()
    {
        owner = GetComponentInParent<Creature>();
    }


}
