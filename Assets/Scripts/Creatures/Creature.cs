using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeDamage { Physical, Magic }

[System.Serializable]

// ��������� �����
public struct AttackParameters 
{
    public int damage;                  // ����
    public TypeDamage typeDamage;       // ��� �����
    public float pushForce;             // �������� ������
    public float timeStunning;          // ����� ���������
}


// ������������� ����� - "��������", �������� ������� ������ ������ ������� �� �����
public abstract class Creature : MonoBehaviour
{
    [Header("Creature:")]
    public float speed;                 // �������� ��������
    public int maxHealth;               // ������������ ����� ��������
    public int health;                  // ������� ����� ��������
    public int physicalProtect;         // ���������� ������
    public int magicProtect;            // ���������� ������
    public float xDamageGain = 0;       // ��������� ����� �� ������
    public float xPushMass = 1;         // ��������� �������� ������������ ��� ��������� �����

    protected bool stunned = false;             // �������� �� �������� ����������
    protected bool death = false;               // �������� �� �������� �������
    protected float currentTimeStunning = 0f;   // ������� ����� ���������
    protected Rigidbody2D rb;                   // ����� RigitBody ��������
    protected Animator anim;                    // �������� ��������� ��������
    protected Collider2D collider;              // ��������� ��������



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
    protected virtual void Update() {}

    protected virtual void FixedUpdate() 
    {
        currentTimeStunning -= Time.deltaTime; //������ ����������� ������� ���������
        if (stunned && currentTimeStunning < 0)
        {
            stunned = false;
            rb.velocity = Vector2.zero;
        }
            
    }


    public void PushBack(float force, Transform pusher, float timeStunning) // ������������ � ��������� ��� ��������� ����� �� ������� ���������� �������
    {
        stunned = true;
        currentTimeStunning = timeStunning; 
        Vector2 pushDirection = new Vector2(transform.position.x - pusher.position.x, transform.position.y - pusher.position.y).normalized;
        rb.velocity = pushDirection * force * xPushMass;
    }

    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null) // ��������� ����� � ����� ������������ �� ������� ���������� � ����������
    {
        // ==== ������� ��������� ����� ====
        int realDamage = attack.damage; 
        if (attack.typeDamage == TypeDamage.Physical) realDamage -= physicalProtect;
        else realDamage -= magicProtect;
        if (realDamage < 0) realDamage = 0;
        // =================================
        health -= realDamage;

        if (bullet != null) PushBack(attack.pushForce, bullet, attack.timeStunning); // ���� ���� �� ������� - ������ �� �������
        else PushBack(attack.pushForce, attacking, attack.timeStunning);            // ���� ���������� ���� - ������ �� ����������

        if (health <= 0 && !death) // �������� ���� ��� ����� 0 - �������� �������
        {
            health = 0;
            Death();
        }
        if (!death) anim.SetTrigger("GetDamage"); // ���� �������� �� ������, �� ����������� �������� ��������� �����
    }
    public virtual void Death()
    {
        death = true;
        gameObject.layer = LayerMask.NameToLayer("Corpses");
        rb.bodyType = RigidbodyType2D.Static;
        collider.enabled = false;
        anim.SetTrigger("Death");
    }

}
