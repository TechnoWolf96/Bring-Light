using UnityEngine;

public class Player : Creature, IAttackWithWeapon
{
    protected CheckParameters checkParameters;        // Система контроля параметров
    protected Inventory inventory;                    // Инвентарь
    protected Weapon weapon;

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (weapon != null) Destroy(weapon.gameObject);
        weapon = newWeapon;
        anim.runtimeAnimatorController = weapon.animController;
    }

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        checkParameters = GameObject.FindWithTag("Script").GetComponent<CheckParameters>();
        inventory = GameObject.FindWithTag("Script").GetComponent<Inventory>();
        ChangeWeapon(GetComponentInChildren<Weapon>());
    }

    protected override void Update()
    {
        if (!isStunned) Move();
        if (Input.GetMouseButtonDown(0) && !isStunned && weapon.IsRecharged())
        {
            weapon.RechargeAgain();
            anim.SetTrigger("Attack");
        }
    }

    protected override void FixedUpdate()
    {
        currentTimeStunning -= Time.deltaTime; //Отсчет оставшегося времени оглушения
        if (isStunned && currentTimeStunning < 0)
        {
            isStunned = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void Attack()
    {
        weapon.Attack();
    }

    private void Move()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        
        if (InputX == 0 && InputY == 0)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
            return;
        }
        anim.SetFloat("HorizontalMovement", InputX);
        anim.SetFloat("VerticalMovement", InputY);

        Vector2 moveDirection = new Vector2(InputX, InputY).normalized;
        rb.velocity = moveDirection * speed;
        anim.SetBool("Walk", true);
    }

    public override void Death()
    {
        base.Death();
        Weapon_notRelease weapon = GetComponentInChildren<Weapon_notRelease>();
        if (weapon != null) weapon.enabled = false;
        inventory.enabled = false;
        rb.velocity = Vector2.zero;
        enabled = false;

    }

    public override void GetDamage(AttackParameters attack, Transform attacking, Transform bullet = null)
    {
        base.GetDamage(attack, attacking, bullet);
        checkParameters.UpdateParameters();     // Обновляем параметры в UI при получении урона
    }





}
