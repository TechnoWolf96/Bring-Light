using System.Collections.Generic;
using UnityEngine;


public struct Creature_Parameters
{
    public float speed;                 // Скорость существа
    public int maxHealth;               // Максимальный запас здоровья
    public int health;                  // Текущий запас здоровья
    public ProtectParameters protect;   // Параметры защиты
    [Min(0f)] public float xPushMass;         // Множитель мощности  и времени отталкивания при получении урона
}
public struct Stalker_Parameters
{
    public float distanceDetection; // Дистанция обнаружения объекта для преследования
    public LayerMask detectionableLayer; // Слой, который отслеживает преследователь (Слой игроков)
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
    public float checkInterval;     // Временной интервал между запросами на смену типа существа
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
