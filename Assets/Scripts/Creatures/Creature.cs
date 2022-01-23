using UnityEngine;


// ������������� ����� - "��������", �������� ������� ������ ������ ������� �� �����
public abstract class Creature : MonoBehaviour, IDestructable
{
    [Header("Creature:")]
    public float speed;                 // �������� ��������
    public int maxHealth;               // ������������ ����� ��������
    public int health;                  // ������� ����� ��������
    public ProtectParameters protect;   // ��������� ������

    protected Animator anim;            // �������� ��������
    protected Rigidbody2D rb;           // ����� RigitBody ��������
    protected HealthBar healthBar;      // ������� �������� ��������
    protected Transform bodyCenter;    // ����� ���� �������� (���������� ��� ������� ������� ������������ ��� ��������� �����)
    [SerializeField] protected GameObject physicalSupport;       // ���������� ����� ��������

    public Transform GetBodyCenter() { return bodyCenter; }



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar ??= GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
        bodyCenter = gameObject.transform.Find("BodyCenter");
    }

    protected virtual void Update() {}



    // ������������ � ��������� ��� ��������� ����� �� ������� ���������� �������
    public void PushBack(float force, Transform pusher) 
    {
        Vector2 pushDirection = new Vector2(bodyCenter.position.x - pusher.position.x, bodyCenter.position.y - pusher.position.y).normalized;
        rb.AddForce(pushDirection * force);
        LookAt(pusher.position);
    }

    // ��������� ����� � ����� ������������ �� ������� ���������� � ����������, ���������� ��� �� ����
    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        health -= GetRealDamage(attack); // ������� ��������� �����
        if (bullet != null) PushBack(attack.pushForce, bullet); // ���� ���� �� ������� - ������ �� �������
        else PushBack(attack.pushForce, attacking);             // ���� ���������� ���� - ������ �� ����������
        

        if (health <= 0)   // �������� ���� ��� ����� 0 - �������� �������
        {
            health = 0;
            healthBar?.ShowBar();
            Death();
            return;
        }
        healthBar?.ShowBar();
        anim.SetTrigger("GetDamage");
        
    }


    public virtual void Death()
    {
        physicalSupport.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Corpses");
        anim.SetTrigger("Death");
        Destroy(this);
            
    }

    protected int GetRealDamage(AttackParameters attack) // ������ ��������� ��������� ����� ��������� � ������ ��� ������ � ����� �����
    {
        int result = 0;
        foreach (var item in attack.damages)
        {
            int preDamage = item.Damaged();
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

    public void LookAt(Vector2 target)
    {
        Vector2 directionMovement = VectorFunction.ToAxisAndNormalize(target - (Vector2)transform.position);
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