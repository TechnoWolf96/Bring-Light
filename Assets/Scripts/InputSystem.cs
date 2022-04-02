using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem singleton { get; protected set; }

    public KeyCode Up;
    public KeyCode ChangeArrows;
    public KeyCode SaveGame;
    public KeyCode LoadGame;



    private void Awake()
    {
        singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(ChangeArrows))
        {
            PlayerWeaponChanger.singleton.SelectArrows((PlayerWeaponChanger.singleton.activeArrow + 1)%2);
        }
        if (Input.GetKeyDown(SaveGame))
        {
            SaveLoadManager.singleton.Save();
        }
        if (Input.GetKeyDown(LoadGame))
        {
            SaveLoadManager.singleton.Load();
        }
    }
}
