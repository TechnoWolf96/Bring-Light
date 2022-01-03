using System.Collections.Generic;
using UnityEngine;


public struct Creature_Parameters
{
    public float speed;                 // �������� ��������
    public int maxHealth;               // ������������ ����� ��������
    public int health;                  // ������� ����� ��������
    public ProtectParameters protect;   // ��������� ������
    [Min(0f)] public float xPushMass;         // ��������� ��������  � ������� ������������ ��� ��������� �����
}
public struct Stalker_Parameters
{
    public float distanceDetection; // ��������� ����������� ������� ��� �������������
    public LayerMask detectionableLayer; // ����, ������� ����������� �������������� (���� �������)
}
public struct CloseAttack_Parameters
{
    public float radiusTriggerAttack;
}
public struct Spellcaster_Parameters
{
    public List<NPCSpell> spells;
}
public struct RangedAttack_Parameters
{

}



public class Multiclass : MonoBehaviour
{
    [Header("You need add scripts \"Close Attack\"")]
    public float checkInterval;     // ��������� �������� ����� ��������� �� ����� ���� ��������
    public bool canBeCloseAttack;
    public bool canBeSpellcaster;
    public bool canBeRangedAttack;


    protected CloseAttack closeAttack;
    protected Spellcaster spellcaster;
    protected RangedAttack rangedAttack;
    protected float currentCheckInterval;
    protected int priorityCloseAttack;
    protected int prioritySpellcaster;
    protected int priorityRangedAttack;


}
