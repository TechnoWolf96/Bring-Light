using UnityEngine;

public class Player : Creature
{
    private CheckParameters checkParameters;        // Система контроля параметров
    private Inventory inventory;                    // Инвентарь


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        checkParameters = GameObject.FindWithTag("Script").GetComponent<CheckParameters>();
        inventory = GameObject.FindWithTag("Script").GetComponent<Inventory>();
    }

    protected override void Update()
    {
        if (!isStunned) Move();
        if (Input.GetMouseButtonDown(0) && !isStunned)
        {
            anim.SetTrigger("Attack");
        }
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
        Weapon weapon = GetComponentInChildren<Weapon>();
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
