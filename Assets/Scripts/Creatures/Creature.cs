using UnityEngine;


// Универсальный класс - "Существо", является предком любого живого объекта на сцене
public abstract class Creature : MonoBehaviour, IDestructable
{
    [Header("Creature:")]
    public float speed;                 // Скорость существа
    public int maxHealth;               // Максимальный запас здоровья
    public int health;                  // Текущий запас здоровья
    public ProtectParameters protect;   // Параметры защиты

    protected Animator anim;            // Анимация существа
    protected Rigidbody2D rb;           // Агент RigitBody существа
    protected HealthBar healthBar;      // Полоска здоровья существа
    protected Transform bodyCenter;    // Центр тела существа (необходимо для верного расчета отталкивания при получении урона)
    [SerializeField] protected GameObject physicalSupport;       // Физическая опора существа

    public Transform GetBodyCenter() { return bodyCenter; }



    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar ??= GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
        bodyCenter = gameObject.transform.Find("BodyCenter");
    }

    protected virtual void Update() {}



    // Отталкивание и оглушение при получении урона от позиции толкающего объекта
    public void PushBack(float force, Transform pusher) 
    {
        Vector2 pushDirection = new Vector2(bodyCenter.position.x - pusher.position.x, bodyCenter.position.y - pusher.position.y).normalized;
        rb.AddForce(pushDirection * force);
        LookAt(pusher.position);
    }

    // Получение урона с силой отталкивания от позиции атакующего и оглушением, возвращает был ли крит
    public virtual void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        health -= GetRealDamage(attack); // Подсчет реального урона
        if (bullet != null) PushBack(attack.pushForce, bullet); // Если урон от снаряда - толчок от снаряда
        else PushBack(attack.pushForce, attacking);             // Если рукопашный урон - толчок от атакующего
        

        if (health <= 0)   // Здоровье ниже или равно 0 - существо умирает
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

    protected int GetRealDamage(AttackParameters attack) // Расчет получения реального урона существом с учетом его защиты и крита атаки
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


// Полезные функции для 2D векторов
public class VectorFunction
{
    // Перевести вектор в одно подходящее направление по оси
    public static Vector2 ToAxisAndNormalize(Vector2 vector)
    {
        vector.Normalize();
        // Первый квадрант
        if (vector.x > 0 && vector.y > 0)
        {
            if (vector.x >= vector.y) return Vector2.right;
            else return Vector2.up;
        }
        // Второй квадрант
        if (vector.x < 0 && vector.y > 0)
        {
            if (Mathf.Abs(vector.x) >= vector.y) return Vector2.left;
            else return Vector2.up;
        }
        // Третий квадрант
        if (vector.x < 0 && vector.y < 0)
        {
            if (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y)) return Vector2.left;
            else return Vector2.down;
        }
        // Четвертый квадрант
        if (vector.x > 0 && vector.y < 0)
        {
            if (vector.x >= Mathf.Abs(vector.y)) return Vector2.right;
            else return Vector2.down;
        }
        return Vector2.zero;
    }
}