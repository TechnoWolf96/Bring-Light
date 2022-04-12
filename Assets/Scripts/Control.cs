using UnityEngine;

public class Control : MonoBehaviour
{
    public static Control singleton { get; protected set; }

    public bool playerControlActive = false;
    public bool playerInterfaceActive = false;

    public KeyCode Consumable1;
    public KeyCode Consumable2;
    public KeyCode Consumable3;
    public KeyCode Consumable4;
    public KeyCode Consumable5;

    public KeyCode RangedAttack;
    public KeyCode CloseAttack;

    public KeyCode ChangeArrows;
    public KeyCode OpenInventory;
    public KeyCode TakeItem;
    public KeyCode SaveGame;
    public KeyCode LoadGame;



    private void Awake()
    {
        if (singleton == null) singleton = this;
    }

    private void Start()
    {
        if (this != singleton) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyDown(ChangeArrows))
            PlayerWeaponChanger.singleton.SelectArrows((PlayerWeaponChanger.singleton.activeArrow + 1)%2);

        if (Input.GetKeyDown(OpenInventory) && playerInterfaceActive) 
            InventoryPanel.singleton.OpenOrClose();

        if (Input.GetKeyDown(TakeItem) && playerControlActive)
            Player.singleton.TakeItem();



        if (Input.GetKeyDown(SaveGame))
            SaveLoadManager.singleton.Save();

        if (Input.GetKeyDown(LoadGame))
            SaveLoadManager.singleton.Load();

        if (Input.GetKey(CloseAttack) && playerControlActive)
            Player.singleton.CloseAttack();

        if (Input.GetKey(RangedAttack) && playerControlActive)
            Player.singleton.RangedAttack();

        if (Input.GetKeyDown(KeyCode.Escape) && GameMenuPanel.singleton != null)
        {
            if (GameMenuPanel.singleton.pause) GameMenuPanel.singleton.Continue();
            else GameMenuPanel.singleton.Pause();
        }
            

    }
}
