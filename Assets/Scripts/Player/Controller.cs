using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private KeyCode ChangeArrows;
    [SerializeField] private KeyCode SaveGame;
    [SerializeField] private KeyCode LoadGame;



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
