using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private KeyCode ChangeArrows;




    private void Update()
    {
        if (Input.GetKeyDown(ChangeArrows))
        {
            PlayerWeaponChanger.singleton.SelectArrows((PlayerWeaponChanger.singleton.activeArrow + 1)%2);
        }
    }
}
