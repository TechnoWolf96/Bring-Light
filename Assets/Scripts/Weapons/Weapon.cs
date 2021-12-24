using UnityEngine;


public interface IAttackWithWeapon
{
    public void ChangeWeapon(Weapon newWeapon);
    public void Attack();
}



public abstract class Weapon : MonoBehaviour
{
    
    [Header("Weapon:")]
    public float rechargeTime;              // �����������
    public RuntimeAnimatorController animController;
    public LayerMask layer;         // ���� ��������, �� �������� ����� ��������� �����

    protected Transform creaturePos;          // ������� ��������, ��������� ������
    protected Creature creature;            // ������ �������� ������, ��������� ������
    protected float currentRechargeTime;           // ������� ����� �� �����������

    public abstract void Attack(); // �����

    protected virtual void Start()
    {
        currentRechargeTime = rechargeTime;
        creaturePos = transform.parent;
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
