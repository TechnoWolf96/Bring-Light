using UnityEngine;


// ������������� ����� - "��������", �������� ������� ������ ������ ������� �� �����
public abstract class Creature : MonoBehaviour, IDestructable
{
    public delegate void Event();
    public event Event onHealthChanged;
    public event Event onDeath;

    [SerializeField] protected float _speed;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected ProtectParameters _protect;

    public virtual float speed { get; set; }
    public virtual int maxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            if (health > _maxHealth) health = _maxHealth;
            if (healthBar != null) healthBar.ShowBar();
            onHealthChanged?.Invoke();
        }
    }
    public virtual int health
    {
        get => _health;
        set 
        {
            if (0 <= value && value <= maxHealth) _health = value;
            if (value > maxHealth) _health = maxHealth;
            if (value < 0) _health = 0;
            if (healthBar != null) healthBar.ShowBar();
            onHealthChanged?.Invoke();
        }
    }
    public virtual ProtectParameters protect { get => _protect; set => _protect = value; }

    protected Animator anim;      
    protected Rigidbody2D rb;     
    protected GameObject physicalSupport;
    protected HealthBar healthBar;
    public Transform bodyCenter { get; protected set; }



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCenter = gameObject.transform.Find("BodyCenter");
        physicalSupport = gameObject.transform.Find("PhysicalSupport").gameObject;
        try { healthBar = gameObject.transform.Find("HealthBar").GetComponent<HealthBar>(); } catch {}
    }



    // ������������ ��� ��������� ����� �� ������� ���������� �������
    public void PushBack(float force, Transform pusher) 
    {
        Vector2 pushDirection = new Vector2(bodyCenter.position.x - pusher.position.x, bodyCenter.position.y - pusher.position.y).normalized;
        rb.AddForce(pushDirection * force);
        LookAt(pusher.position);
    }

    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        health -= CalculateRealDamage(attack);
        if (bullet != null) PushBack(attack.pushForce, bullet); // ���� ���� �� ������� - ������ �� �������
        else PushBack(attack.pushForce, attacking);             // ���� ���������� ���� - ������ �� ����������
        if (health <= 0) Death();
        else { anim.SetTrigger("GetDamage"); }
    }


    public virtual void Death()
    {
        physicalSupport.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Corpses");
        anim.SetTrigger("Death");
        //onDeath.Invoke();
        Destroy(this);
            
    }

    public void LookAt(Vector2 target)
    {
        Vector2 directionMovement = Library.ToAxisAndNormalize(target - (Vector2)transform.position);
        anim.SetFloat("HorizontalMovement", directionMovement.x);
        anim.SetFloat("VerticalMovement", directionMovement.y);
    }


    // ������ ��������� ��������� ����� ��������� � ������ ��� ������
    protected int CalculateRealDamage(AttackParameters attack)
    {
        int result = 0;
        if (protect == null) print("ouuu");
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


}