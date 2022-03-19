using UnityEngine;

public abstract class SpellNPC : MonoBehaviour
{
    public int priority; //{ get; protected set; }
    [SerializeField] protected float speedCast;

    // ���������� ��� ������ ������������ ����������
    public abstract void BeginCast();

    // �������� �������� ������ �� ���������� (������ ����� ���������� � �������� Spellcast)
    public abstract void Activate();

    // ������ �������, �� �������� ����������� ��������� ����������
    public abstract void CalculatePriority();

    public abstract void BreakCast();

}
