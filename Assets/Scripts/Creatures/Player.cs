using UnityEngine;

public class Player : Creature, IAttackWithWeapon
{
    public event Event onPlayerDeath;
    private static Player _singleton;
    public static Player singleton { get => _singleton; }
    [HideInInspector] public bool controled = true;

    protected Weapon currentWeapon;
    protected Transform weaponSlot;
    protected bool activeMeleeWeapon = true;
    
    private void Awake()
    {
        _singleton = this;
    }
    public void ChangeWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null) Destroy(currentWeapon.gameObject);
        currentWeapon = Instantiate(newWeapon, weaponSlot).GetComponent<Weapon>();
        anim.runtimeAnimatorController = currentWeapon.animController;
    }

    protected override void Start()
    {
        base.Start();
        weaponSlot = gameObject.transform.Find("Weapon");
        currentWeapon = GetComponentInChildren<Weapon>();
        anim.runtimeAnimatorController = currentWeapon.animController;
    }

    protected void Update()
    {
        if (!controled) return;
        Move();
        AttackInput();
        
    }

    public void AttackMoment()
    {
        currentWeapon.Attack();
    }

    private void Move()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (moveDirection.x == 0 && moveDirection.y == 0)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = moveDirection.normalized * _speed;
            anim.SetBool("Walk", true);
        }
        LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void AttackInput()
    {
        // TODO: Разобраться с длительностью анимации у оружия
        // ЛКМ - Выстрел
        if (Input.GetMouseButton(0) && currentWeapon.IsRecharged())
        {
            if (activeMeleeWeapon && InventoryInfoSlot.singleton.rangedWeaponSlots.transform.childCount != 0)
            {
                ChangeWeapon(InventoryInfoSlot.singleton.rangedWeaponSlots.transform.GetChild(0).GetComponent<WeaponItem>().weapon);
                activeMeleeWeapon = false;
            }
            anim.SetTrigger("Attack");
            anim.SetFloat("SpeedAttack", 2f);
            currentWeapon.BeginAttack();
        }
        // ПКМ - Удар
        if (Input.GetMouseButton(1) && currentWeapon.IsRecharged())
        {
            if (!activeMeleeWeapon && InventoryInfoSlot.singleton.meleeWeaponSlots.transform.childCount != 0)
            {
                ChangeWeapon(InventoryInfoSlot.singleton.meleeWeaponSlots.transform.GetChild(0).GetComponent<WeaponItem>().weapon);
                activeMeleeWeapon = true;
            }
            anim.SetTrigger("Attack");
            anim.SetFloat("SpeedAttack", 2f);
            currentWeapon.BeginAttack();
        }
    }

    public override void Death()
    {
        base.Death();
        onPlayerDeath.Invoke();
    }





}
