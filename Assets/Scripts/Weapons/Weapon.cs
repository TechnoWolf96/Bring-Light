using UnityEngine;
using FMODUnity;

public interface IAttackWithWeapon
{
    public void AttackMoment();
}


// Объект со скриптом Weapon должен быть дочерним в иерархии к объекту Weapon
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
