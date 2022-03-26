using UnityEngine;

public class Player : Creature, IAttackWithWeapon
{
    public event Event onPlayerDeath;
    private static Player _singleton;
    public static Player singleton { get => _singleton; }
    [HideInInspector] public bool controled = true;
    public PlayerWeapon currentWeapon { get; set; }
    public Transform weaponSlot { get; protected set; }



    private void Awake()
    {
        _singleton = this;
        anim = GetComponent<Animator>();
        weaponSlot = gameObject.transform.Find("Weapon");
    }


    protected override void Update()
    {
        base.Update();
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
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0
            && controled) anim.SetBool("Move", true);
        else anim.SetBool("Move", false);
    }

    private void AttackInput()
    {
        // ЛКМ - Выстрел
        if (Input.GetMouseButton(0))
        {
            if (InventoryInfoSlot.singleton.rangedWeaponSlot.transform.childCount == 0) return; // Нет в слоте оружия - ничего не делаем
            if (PlayerWeaponChanger.singleton.currentWeaponType != CurrentWeaponType.Ranged)
            {
                PlayerWeaponChanger.singleton.SelectWeapon(CurrentWeaponType.Ranged);
            }
            if (ArrowCounter.singleton.count == 0) return;
            anim.SetTrigger("Attack");
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
            anim.SetTrigger("Attack");
            return;
        }
    }

    public void PlaySoundAttack() => currentWeapon.PlaySound();



}
