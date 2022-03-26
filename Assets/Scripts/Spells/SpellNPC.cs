using UnityEngine;

public abstract class SpellNPC : MonoBehaviour
{
    public int priority{ get; protected set; }
    [SerializeField] protected float speedCast;
    [SerializeField] protected float rechargeTime;
    protected float currentRechargeTime = 0;

    protected virtual void FixedUpdate()
    {
        currentRechargeTime -= Time.deltaTime;
    }

    // ���������� ��� ������ ������������ ����������
    public abstract void BeginCast();

    // �������� �������� ������ �� ���������� (������ ����� ���������� � �������� Spellcast)
    public abstract void Activate();

    // ������ �������, �� �������� ����������� ��������� ����������
    public abstract void CalculatePriority();

    public abstract void BreakCast();

}
