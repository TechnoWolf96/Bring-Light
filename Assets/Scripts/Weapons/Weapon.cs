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
    [SerializeField] protected float rechargeTime;
    [SerializeField] protected RuntimeAnimatorController _animController;
    public RuntimeAnimatorController animController { get => _animController; }
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected EventReference attackSound;

    protected const float timeOriginalAttackAnimation = 2f;

    protected Creature owner;
    protected float currentRechargeTime;

    public abstract void Attack(); // �����

    public void BeginAttack()
    { 
        Library.Play3DSound(attackSound, owner.transform);
        RechargeAgain();
    }

    protected virtual void Start()
    {
        currentRechargeTime = 0f;
        owner = GetComponentInParent<Creature>();
    }

    protected virtual void FixedUpdate() // ���������� �������� ������� �� �����������
    {
        currentRechargeTime -= Time.deltaTime;
    }

    public bool IsRecharged()
    {
        return currentRechargeTime < 0;
    }

    public void RechargeAgain()
    {
        currentRechargeTime = rechargeTime;
    }

}
