using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCSpell : MonoBehaviour
{
    [Header("NPC spell:")]
    public int priority;                // ���� ���������� ������ ����� ���������� (������������� ��������� �� 1 �� 10)
    public float speedCast;             // �������� �������� ���������� ����������
    public float timeUntilActivate;     // ����� �� ��������� ��������� �������

    [SerializeField] protected float currentTimeUntilActivate;
    protected bool casting = false;

    protected virtual void Start()
    {
        currentTimeUntilActivate = timeUntilActivate; 
    }
    protected virtual void FixedUpdate()
    {
        if (casting) currentTimeUntilActivate -= Time.deltaTime;
    }

    protected virtual void Update()
    {
        if (currentTimeUntilActivate < 0) Cast();
    }

    public virtual void BeginCast()
    {
        currentTimeUntilActivate = timeUntilActivate;
        casting = true;
        ApplyVisualEffects();
    }

    protected virtual void ApplyVisualEffects() { }

    protected virtual void Cast()
    {
        casting = false;
        currentTimeUntilActivate = timeUntilActivate;
    }
}
