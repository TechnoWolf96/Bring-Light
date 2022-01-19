using UnityEngine;
using FMODUnity;

public class Player : Creature, IAttackWithWeapon
{
    protected CheckParameters checkParameters;        // Система контроля параметров
    protected Inventory inventory;                    // Инвентарь
    protected Weapon weapon;

    [SerializeField]
    protected EventReference sound;
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
            Library.Play3DSound(sound, transform);
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
        /*
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 faceDirection = mousePosition - (Vector2)transform.position;
        anim.SetFloat("HorizontalMovement", faceDirection.x);
        anim.SetFloat("VerticalMovement", faceDirection.y);
        */
        rb.velocity = moveDirection.normalized * speed;
        anim.SetBool("Walk", true);
    }

    public override void Death()
    {
        base.Death();
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
