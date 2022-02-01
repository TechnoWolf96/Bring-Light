using UnityEngine;
using FMODUnity;

public class Player : Creature, IAttackWithWeapon
{
    public PlayerStatus status { get; protected set; }        // Система контроля параметров
    protected Weapon weapon;
    protected Transform weaponSlot;    // Слот под текущее оружие в иерархии

    public override int health
    { 
        get => base.health;
        set
        {
            base.health = value;
            status.UpdateParameters();
        }
    }
    public override int maxHealth {
        get => base.maxHealth;
        set
        {
            base.maxHealth = value;
            status.UpdateParameters();
        }
    }

    [SerializeField]
    protected EventReference sound;
    public void ChangeWeapon(GameObject newWeapon)
    {
        if (weapon != null) Destroy(weapon.gameObject);
        weapon = Instantiate(newWeapon, weaponSlot).GetComponent<Weapon>();
        anim.runtimeAnimatorController = weapon.animController;
    }

    protected virtual void OnEnable()
    {
        status = GameObject.Find("Tools/PlayerScripts").GetComponent<PlayerStatus>();
    }


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        weaponSlot = gameObject.transform.Find("Weapon");
        weapon = GetComponentInChildren<Weapon>();
        anim.runtimeAnimatorController = weapon.animController;
    }

    protected override void Update()
    {
        Move();
        if (Input.GetMouseButton(0) && weapon.IsRecharged())
        {
            anim.SetTrigger("Attack");
            
            float calculateSpeed = weapon.GetTimeOriginalAttackAnimation() / weapon.rechargeTime;
            anim.SetFloat("SpeedAttack", calculateSpeed);
            weapon.BeginAttack();
        }
    }

    public void Attack()
    {
        weapon.Attack();
    }

    private void Move()
    {
        Vector2 moveDirection = new Vector2();
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        
        if (moveDirection.x == 0 && moveDirection.y == 0)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
            LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            return;
        }
        LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        rb.velocity = moveDirection.normalized * speed;
        anim.SetBool("Walk", true);
    }

    public override void Death()
    {
        rb.velocity = Vector2.zero;
        enabled = false;
        base.Death();
        
        

    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        status.UpdateParameters();     // Обновляем параметры в UI при получении урона
    }





}
