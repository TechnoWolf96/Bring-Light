using UnityEngine;

public interface IDestructable
{
    public void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null);
    public void Death();
}


// Универсальный класс - "Существо", является предком любого живого объекта на сцене
public abstract class Creature : MonoBehaviour, IDestructable
{
    public delegate void CreatureEvent();
    public event CreatureEvent onHealthChanged;
    public event CreatureEvent onDeath;

    [SerializeField] protected float _speed;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected ProtectParameters _protect;
    protected const float updateStateTimeCicle = 0.3f;
    protected float timeUntilUpdateState;
    public virtual float speed { get => _speed; set => _speed = value; }
    public virtual int maxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            if (health > _maxHealth) health = _maxHealth;
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
            onHealthChanged?.Invoke();
        }
    }
    public virtual ProtectParameters protect { get => _protect; set => _protect = value; }

    public Animator anim { get; set; }     
    public Rigidbody2D rb { get; protected set; }
    public GameObject physicalSupport { get; protected set; }
    public Transform bodyCenter { get; protected set; }

    protected virtual void Update() 
    {
        timeUntilUpdateState -= Time.deltaTime;
        if (timeUntilUpdateState < 0)
        {
            UpdateState();
            timeUntilUpdateState = updateStateTimeCicle;
        }
    }
    protected virtual void UpdateState() {}

    protected virtual void Start()
    {
        timeUntilUpdateState = updateStateTimeCicle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCenter = gameObject.transform.Find("BodyCenter");
        physicalSupport = gameObject.transform.Find("PhysicalSupport").gameObject;
    }



    // Отталкивание при получении урона от позиции толкающего объекта
    public void PushBack(float force, Transform pusher) 
    {
        Vector2 pushDirection;
        if (pusher.TryGetComponent(out Creature creature))
        {
            pushDirection = new Vector2(bodyCenter.position.x - creature.bodyCenter.position.x,
                bodyCenter.position.y - creature.bodyCenter.position.y).normalized;
        }
        else
        {
            pushDirection = new Vector2(bodyCenter.position.x - pusher.position.x,
                bodyCenter.position.y - pusher.position.y).normalized;
        }
        
        rb.AddForce(pushDirection * force);
        LookAt(pusher.position);
    }

    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        health -= CalculateRealDamage(attack);
        if (bullet != null) PushBack(attack.pushForce, bullet); // Если урон от снаряда - толчок от снаряда
        else PushBack(attack.pushForce, attacking);             // Если рукопашный урон - толчок от атакующего
        if (health <= 0) Death();
        else anim.SetTrigger("GetDamage");
    }


    public virtual void Death()
    {
        physicalSupport.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Corpses");
        anim.SetTrigger("Death");
        onDeath?.Invoke();
        Destroy(this);

    }

    public void LookAt(Vector2 target)
    {
        Vector2 directionMovement = Library.ToAxisAndNormalize(target - (Vector2)transform.position);
        anim.SetFloat("HorizontalMovement", directionMovement.x);
        anim.SetFloat("VerticalMovement", directionMovement.y);
    }


    // Расчет получения реального урона существом с учетом его защиты
    protected int CalculateRealDamage(AttackParameters attack)
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


}