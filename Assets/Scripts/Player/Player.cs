using UnityEngine;

public class Player : Creature, IAttackWithWeapon
{
    public event Event onPlayerDeath;
    private static Player _singleton;
    public static Player singleton { get => _singleton; }
    [HideInInspector] public bool controled = true;

    public Weapon currentWeapon { get; set; }
    public Transform weaponSlot { get; protected set; }

    private const float coolDownTime = 0.1f;
    private float currentCoolDownTime;


    private void Awake()
    {
        _singleton = this;
        anim = GetComponent<Animator>();
        weaponSlot = gameObject.transform.Find("Weapon");
    }

    private void FixedUpdate()
    {
        currentCoolDownTime -= Time.deltaTime;
    }

    protected override void Start()
    {
        base.Start();
        
        currentCoolDownTime = coolDownTime;
    }

    protected void Update()
    {
        if (!controled) return;
        Move();
        AttackInput();
        
    }

    public void AttackMoment()
    {
       
       
        if (PlayerWeaponChanger.singleton.currentWeaponType == CurrentWeaponType.Ranged)
        {
            RangedWeaponPlayer rangedWeapon = (RangedWeaponPlayer)currentWeapon;
            rangedWeapon.bullet = PlayerWeaponChanger.singleton.GetAndUseArrow();
        }
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
        if (currentCoolDownTime > 0) return;
        else currentCoolDownTime = coolDownTime;
        // ЛКМ - Выстрел
        if (Input.GetMouseButton(0))
        {
            if (InventoryInfoSlot.singleton.rangedWeaponSlot.transform.childCount == 0) return; // Нет в слоте оружия - ничего не делаем
            if (PlayerWeaponChanger.singleton.currentWeaponType != CurrentWeaponType.Ranged)
            {
                PlayerWeaponChanger.singleton.SelectWeapon(CurrentWeaponType.Ranged);
            }
            if (currentWeapon.IsRecharged())
            {
                
                if (ArrowCounter.singleton.count == 0) return;

                anim.SetTrigger("Attack");
                anim.SetFloat("SpeedAttack", 2f);
                currentWeapon.BeginAttack();
            }
            return;
        }
        // ПКМ - Удар
        if (Input.GetMouseButton(1))
        {
            if (InventoryInfoSlot.singleton.meleeWeaponSlot.transform.childCount == 0) return; // Нет в слоте оружия - ничего не делаем
            if (PlayerWeaponChanger.singleton.currentWeaponType != CurrentWeaponType.Melee)
            {
                PlayerWeaponChanger.singleton.SelectWeapon(CurrentWeaponType.Melee);
            }
            if (currentWeapon.IsRecharged())
            {
                anim.SetTrigger("Attack");
                anim.SetFloat("SpeedAttack", 2f);
                currentWeapon.BeginAttack();
            }
            return;
        }
        
    }

    public override void Death()
    {
        base.Death();
        onPlayerDeath.Invoke();
    }





}
