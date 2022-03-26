using UnityEngine;
using FMODUnity;
public interface IAttackWithWeapon
{
    public void AttackMoment();
}

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask layerAttack;
    protected Creature owner;
    public abstract void Attack();
    protected virtual void Start()
    {
        owner = GetComponentInParent<Creature>();
    }
}
// ������ �� �������� Weapon ������ ���� �������� � �������� � ������� Weapon
public abstract class PlayerWeapon : Weapon
{
    [SerializeField] protected RuntimeAnimatorController _animController;
    [SerializeField] protected EventReference soundAttack;
    public RuntimeAnimatorController animController { get => _animController; }

    public void PlaySound()
    {
        Library.Play3DSound(soundAttack, transform);
    }
}
