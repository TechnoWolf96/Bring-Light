using UnityEngine;
using FMODUnity;

public interface IAttackWithWeapon
{
    public void Attack();
}


// ������ �� �������� Weapon ������ ���� �������� � �������� � ������� Weapon
public abstract class Weapon : MonoBehaviour
{
    
    [Header("Weapon:")]
    public float rechargeTime;              // �����������
    public RuntimeAnimatorController animController;
    public LayerMask layer;         // ���� ��������, �� �������� ����� ��������� �����
    [SerializeField] protected EventReference attackSound;
    [SerializeField] protected float timeOriginalAttackAnimation;   // ����� ������������ �������� ����� (������������ �������)

    public float GetTimeOriginalAttackAnimation() {return timeOriginalAttackAnimation;}

    protected Creature creature;            // ������ �������� ������, ��������� ������
    protected float currentRechargeTime;           // ������� ����� �� �����������

    public abstract void Attack(); // �����

    public void BeginAttack()
    { 
        Library.Play3DSound(attackSound, creature.transform);
        RechargeAgain();
    }

    protected virtual void Start()
    {
        currentRechargeTime = rechargeTime;
        creature = GetComponentInParent<Creature>();
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
