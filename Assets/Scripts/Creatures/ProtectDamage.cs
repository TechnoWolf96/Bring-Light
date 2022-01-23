using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������������ ���� �����
public enum TypeDamage
{
    Physical, Holy, Fiery, Cold, Dark, Poison
}

[System.Serializable] // ��������� ������
public class ProtectParameters
{
    [Header("Protection percentage:")]
    [Range(-1000, 100)] public int physical;    // �� �����������
    [Range(-1000, 100)] public int holy;        // �� ����������
    [Range(-1000, 100)] public int fiery;       // �� ���������
    [Range(-1000, 100)] public int cold;        // �� ����� �������
    [Range(-1000, 100)] public int dark;        // �� ����� ������ ����
    [Range(-1000, 100)] public int poison;      // �� ����� ����

    
    public static ProtectParameters operator+ (ProtectParameters th, ProtectParameters other)
    {
        ProtectParameters result = new ProtectParameters();
        result.physical = th.physical + other.physical;
        result.holy = th.holy + other.holy;
        result.fiery = th.fiery + other.fiery;
        result.cold = th.cold + other.cold;
        result.dark = th.dark + other.dark;
        result.poison = th.poison + other.poison;
        return result;
    }
    public static ProtectParameters operator -(ProtectParameters th, ProtectParameters other)
    {
        ProtectParameters result = new ProtectParameters();
        result.physical = th.physical - other.physical;
        result.holy = th.holy - other.holy;
        result.fiery = th.fiery - other.fiery;
        result.cold = th.cold - other.cold;
        result.dark = th.dark - other.dark;
        result.poison = th.poison - other.poison;
        return result;
    }
}

[System.Serializable] // ��� � �������� �����
public struct Damage
{
    public TypeDamage typeDamage;                   // ��� �����
    [SerializeField] private int minDamage;         // ����������� ����
    [SerializeField] private int maxDamage;         // ������������ ����
    public int Damaged()     // ���������� ��� �� ���� � ����������� ����
    {
        return Random.Range(minDamage, maxDamage + 1);
    }
}


[System.Serializable] // ��������� �����
public struct AttackParameters
{
    public Damage[] damages;            // ���� �����
    public float pushForce;             // �������� ������
    [Range(0, 100)] public int critChance;           // ����������� � ��������� ��������� ������������ �����
    private bool isCrit;            // ���� �� ���� � ������� �����


    public bool GetCrit() { return isCrit; }

    // ���������� � ������������� �� ������� ����� ��� �� ����. ����� ������ �������� ����� ����� ������, ����� �������� ���� isCrit
    public bool CalculateCrit()      
    {
        if (critChance >= Random.Range(1, 101))
        {
            isCrit = true;
            return true;
        }
        isCrit = false;
        return false;
    }
}



