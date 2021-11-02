using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������������� ����� - "��������", �������� ������� ������ ������ ������� �� �����
public abstract class Creature : MonoBehaviour
{
    [Header("Creature:")]
    public float speed;                 // �������� ��������
    public int maxHealth;               // ������������ ����� ��������
    public int health;                  // ������� ����� ��������
    public ProtectParameters protect;   // ��������� ������
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
        int realDamage = GetRealDamage(attack); // ������� ��������� �����
        if (realDamage < 0) realDamage = 0;
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

    private int GetRealDamage(AttackParameters attack)
    {
        int result = 0;
        foreach (var item in attack.damages)
        {
            switch (item.typeDamage)
            {
                case TypeDamage.Physical:
                    result += item.damage - (item.damage * protect.physical / 100);
                    break;
                case TypeDamage.Holy:
                    result += item.damage - (item.damage * protect.holy / 100);
                    break;
                case TypeDamage.Fiery:
                    result += item.damage - (item.damage * protect.fiery / 100);
                    break;
                case TypeDamage.Cold:
                    result += item.damage - (item.damage * protect.cold / 100);
                    break;
                case TypeDamage.Dark:
                    result += item.damage - (item.damage * protect.dark / 100);
                    break;
                default:
                    break;
            }
        }
        return result;
    }

}
