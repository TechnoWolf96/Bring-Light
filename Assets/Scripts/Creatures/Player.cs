using UnityEngine;
using FMODUnity;

public class Player : Creature, IAttackWithWeapon
{
    private static Player _singleton;
    public static Player singleton { get => _singleton; }

    public delegate void OnUpdateHealth();
    public event OnUpdateHealth updateHealth;

    protected Weapon weapon;
    protected Transform weaponSlot;    // ���� ��� ������� ������ � ��������

    public override int health
    { 
        get => base.health;
        set
        {
            base.health = value;
            updateHealth.Invoke();
        }
    }
    public override int maxHealth {
        get => base.maxHealth;
        set
        {
            base.maxHealth = value;
            updateHealth.Invoke();
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
    private void Awake()
    {
        _singleton = this;
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





}
