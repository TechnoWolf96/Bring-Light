using UnityEngine;


// ������������� ����� - "��������", �������� ������� ������ ������ ������� �� �����
public abstract class Creature : MonoBehaviour
{
    [Header("Creature:")]
    public float speed;                 // �������� ��������
    public int maxHealth;               // ������������ ����� ��������
    public int health;                  // ������� ����� ��������
    public ProtectParameters protect;   // ��������� ������
    [Min(0f)] public float xPushMass = 1;         // ��������� �������� ������������ ��� ��������� �����

    protected Animator anim;          // �������� ��������
    protected bool isStunned = false;             // �������� �� �������� ����������
    protected bool isDeath = false;               // �������� �� �������� �������
    protected float currentTimeStunning = 0f;   // ������� ����� ���������
    protected Rigidbody2D rb;                   // ����� RigitBody ��������
    protected Collider2D collider;              // ��������� ��������
    protected HealthBar healthBar;              // ������� �������� ��������



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        healthBar ??= GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update() {}

    protected virtual void FixedUpdate()
    {
        currentTimeStunning -= Time.deltaTime; //������ ����������� ������� ���������
        if (isStunned && currentTimeStunning < 0)
        {
            isStunned = false;
            rb.velocity = Vector2.zero;
        }

    }

    // ������������ � ��������� ��� ��������� ����� �� ������� ���������� �������
    public void PushBack(float force, Transform pusher, float timeStunning) 
    {
        isStunned = true;
        currentTimeStunning = timeStunning;
        Vector2 pushDirection = new Vector2(transform.position.x - pusher.position.x, transform.position.y - pusher.position.y).normalized;
        rb.velocity = pushDirection * force * xPushMass;
        LookAt(pusher);
    }

    // ��������� ����� � ����� ������������ �� ������� ���������� � ����������, ���������� ��� �� ����
    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        int realDamage = GetRealDamage(attack);   // ������� ��������� �����
        health -= realDamage;
        print(realDamage);
        if (bullet != null) PushBack(attack.pushForce, bullet, attack.timeStunning); // ���� ���� �� ������� - ������ �� �������
        else PushBack(attack.pushForce, attacking, attack.timeStunning);             // ���� ���������� ���� - ������ �� ����������

        if (health <= 0 && !isDeath)   // �������� ���� ��� ����� 0 - �������� �������
        {
            health = 0;
            Death();
        }
        if (!isDeath) anim.SetTrigger("GetDamage");
        healthBar?.ShowBar();     // ��� ��������� ����� ������������ ������� ��������
    }


    public virtual void Death()
    {
        isDeath = true;
        gameObject.layer = LayerMask.NameToLayer("Corpses");
        anim.SetTrigger("Death");
    }

    protected int GetRealDamage(AttackParameters attack) // ������ ��������� ��������� ����� ��������� � ������ ��� ������ � ����� �����
    {
        int result = 0;
        foreach (var item in attack.damages)
        {
            int preDamage = item.Damaged(attack.GetCrit(), attack.critGainPercentage);
            switch (item.typeDamage)
            {
                case TypeDamage.Physical:
                    result += preDamage - (preDamage * protect.physical / 100);
                    break;
                case TypeDamage.Holy:
                    result += preDamage - (preDamage * protect.holy / 100);
                    break;
                case TypeDamage.Fiery:
                    result += preDamage - (preDamage * protect.fiery / 100);
                    break;
                case TypeDamage.Cold:
                    result += preDamage - (preDamage * protect.cold / 100);
                    break;
                case TypeDamage.Dark:
                    result += preDamage - (preDamage * protect.dark / 100);
                    break;
                case TypeDamage.Poison:
                    result += preDamage - (preDamage * protect.poison / 100);
                    break;
            }
        }
        if (result < 0) result = 0;
        return result;
    }

    public void LookAt(Transform target)
    {
        Vector2 directionMovement = VectorFunction.ToAxisAndNormalize(target.position - transform.position);
        anim.SetFloat("HorizontalMovement", directionMovement.x);
        anim.SetFloat("VerticalMovement", directionMovement.y);
    }

    public void LookAway(Transform target)
    {
        Vector2 directionMovement = VectorFunction.ToAxisAndNormalize(target.position - transform.position);
        anim.SetFloat("HorizontalMovement", -directionMovement.x);
        anim.SetFloat("VerticalMovement", -directionMovement.y);
    }
    

}


// �������� ������� ��� 2D ��������
public class VectorFunction
{
    // ��������� ������ � ���� ���������� ����������� �� ���
    public static Vector2 ToAxisAndNormalize(Vector2 vector)
    {
        vector.Normalize();
        // ������ ��������
        if (vector.x > 0 && vector.y > 0)
        {
            if (vector.x >= vector.y) return Vector2.right;
            else return Vector2.up;
        }
        // ������ ��������
        if (vector.x < 0 && vector.y > 0)
        {
            if (Mathf.Abs(vector.x) >= vector.y) return Vector2.left;
            else return Vector2.up;
        }
        // ������ ��������
        if (vector.x < 0 && vector.y < 0)
        {
            if (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y)) return Vector2.left;
            else return Vector2.down;
        }
        // ��������� ��������
        if (vector.x > 0 && vector.y < 0)
        {
            if (vector.x >= Mathf.Abs(vector.y)) return Vector2.right;
            else return Vector2.down;
        }
        return Vector2.zero;
    }
}